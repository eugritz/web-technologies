using WebTechnologies.Core;

namespace WebTechnologies.Data
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetCars();
        public Car? GetCar(int id);
        public void Create(Car car);
        public void Update(Car car);
        public void Delete(int id);
        public void Save();
    }
}
