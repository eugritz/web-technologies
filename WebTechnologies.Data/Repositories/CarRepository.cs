using Microsoft.EntityFrameworkCore;
using WebTechnologies.Core;

namespace WebTechnologies.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Car car)
        {
            _context.Cars.Add(car);
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
                _context.Cars.Remove(car);
        }

        public Car? GetCar(int id)
        {
            return _context.Cars.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Car> GetCars()
        {
            return _context.Cars;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
        }
    }
}
