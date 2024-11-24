using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using WebTechnologies.Core;

namespace WebTechnologies.Data.RabbitMQ
{
    public class CarRepository : ICarRepository
    {
        private readonly ICarRepository _repository;

        private readonly IConnection _connection;
        private readonly IModel _model;

        public CarRepository(Data.CarRepository repository, IConnection connection)
        {
            _repository = repository;
            _connection = connection;

            _model = _connection.CreateModel();
            _model.ExchangeDeclare("cars_events_exchange", ExchangeType.Direct, true);
            _model.QueueDeclare(queue: "cars_events_queue", exclusive: false, autoDelete: false);
            _model.QueueBind("cars_events_queue", "cars_events_exchange", "");
        }

        public void Create(Car car)
        {
            _repository.Create(car);

            _model.BasicPublish(
                "cars_events_exchange",
                "",
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    EventType = "CREATE",
                    Car = car
                })));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);

            _model.BasicPublish(
                "cars_events_exchange",
                "",
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    EventType = "DELETE",
                    CarId = id
                })));
        }

        public Car? GetCar(int id)
        {
            return _repository.GetCar(id);
        }

        public IEnumerable<Car> GetCars()
        {
            return _repository.GetCars();
        }

        public void Save()
        {
            _repository.Save();
        }

        public void Update(Car car)
        {
            _repository.Update(car);

            _model.BasicPublish(
                "cars_events_exchange",
                "",
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    EventType = "UPDATE",
                    Car = car
                })));
        }
    }
}
