using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using WebTechnologies.Core;
using WebTechnologies.Data;

namespace WebTechnologies.Importer
{
    internal class DbInitializer
    {
        private readonly DataContext _context;
        private readonly ILogger? _logger;

        public DbInitializer(DataContext context, ILogger? logger = null)
        {
            _context = context;
            _logger = logger;
        }

        public void ImportJson()
        {
            List<Car> cars;

            if (_context.Cars.Count() == 0)
            {
                if (_logger != null)
                    _logger.LogDebug("Importing cars.json");

                var rawData = File.ReadAllText("cars.json", Encoding.UTF8);
                var data = JsonSerializer.Deserialize<CarsDto>(rawData);

                if (data == null)
                {
                    if (_logger != null)
                        _logger.LogError("Failed to deserialize cars.json. Terminating");
                    return;
                }

                if (data.Cars == null)
                {
                    if (_logger != null)
                        _logger.LogError("Expected to have field \"cars\". Terminating");
                    return;
                }

                foreach (var car in data.Cars)
                {
                    _context.Cars.Add(car);
                }

                cars = data.Cars;
            }
            else
            {
                cars = _context.Cars.ToList();
            }

            if (_context.Dealers.Count() == 0)
            {
                if (_logger != null)
                    _logger.LogDebug("Importing dealers.json");

                var rawData = File.ReadAllText("dealers.json", Encoding.UTF8);
                var data = JsonSerializer.Deserialize<List<Dealer>>(rawData);

                if (data == null)
                {
                    if (_logger != null)
                        _logger.LogError("Failed to deserialize dealers.json. Terminating");
                    return;
                }

                var random = new Random();
                
                foreach (var dealer in data)
                {
                    int carCount = random.Next(1, 4);
                    for (int i = 0; i < carCount && cars.Count > 0; i++)
                    {
                        int idx = random.Next(0, cars.Count);
                        dealer.Cars.Add(cars[idx]);
                        cars.RemoveAt(idx);
                    }

                    _context.Dealers.Add(dealer);
                }
            }

            _context.SaveChanges();
        }
    }
}
