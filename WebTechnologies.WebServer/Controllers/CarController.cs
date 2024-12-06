using Microsoft.AspNetCore.Mvc;
using WebTechnologies.Core;
using WebTechnologies.Data;

namespace WebTechnologies.WebServer.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Car> GetCars(
            [FromQuery(Name = "firm")] string? firm = null,
            [FromQuery(Name = "model")] string? model = null)
        {
            return _repository.GetCars().Where(car =>
                (string.IsNullOrWhiteSpace(firm) || car.Firm.ToLowerInvariant() == firm.ToLowerInvariant())
                && (string.IsNullOrWhiteSpace(model) || car.Model.ToLowerInvariant() == model.ToLowerInvariant()));
        }

        [HttpGet, Route("{id:int}")]
        public Car? GetCar(int id)
        {
            return _repository.GetCar(id);
        }

        [HttpPut, Route("{id:int}")]
        public IActionResult UpdateCar(int id, [FromBody] Car car)
        {
            var foundCar = _repository.GetCar(id);
            if (foundCar == null)
                return NotFound();

            foundCar.Firm = car.Firm;
            foundCar.Model = car.Model;
            foundCar.Year = car.Year;
            foundCar.Power = car.Power;
            foundCar.Color = car.Color;
            foundCar.Price = car.Price;
            _repository.Update(foundCar);
            _repository.Save();
            return Ok(foundCar);
        }

        [HttpPost]
        public Car? CreateCar([FromBody] Car car)
        {
            _repository.Create(car);
            _repository.Save();
            return car;
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeleteCar(int id)
        {
            var foundDealer = _repository.GetCar(id);
            if (foundDealer == null)
                return NotFound();
            _repository.Delete(id);
            _repository.Save();
            return NoContent();
        }
    }
}
