using Microsoft.AspNetCore.Mvc;
using WebTechnologies.Core;
using WebTechnologies.Data;

namespace WebTechnologies.WebServer.Controllers
{
    [ApiController]
    [Route("api/dealers")]
    public class DealerController : ControllerBase
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ICarRepository _carRepository;

        public DealerController(IDealerRepository dealerRepository, ICarRepository carRepository)
        {
            _dealerRepository = dealerRepository;
            _carRepository = carRepository;
        }

        [HttpGet]
        public IEnumerable<Dealer> GetDealers()
        {
            return _dealerRepository.GetDealers();
        }

        [HttpGet, Route("{id:int}")]
        public Dealer? GetDealer(int id)
        {
            return _dealerRepository.GetDealer(id);
        }

        [HttpPut, Route("{id:int}")]
        public IActionResult UpdateDealer(int id, [FromBody] Dealer dealer)
        {
            var foundDealer = _dealerRepository.GetDealer(id);
            if (foundDealer == null)
                return NotFound();

            foundDealer.Name = dealer.Name;
            foundDealer.City = dealer.City;
            foundDealer.Address = dealer.Address;
            foundDealer.Area = dealer.Area;
            foundDealer.Rating = dealer.Rating;
            _dealerRepository.Update(foundDealer);
            _dealerRepository.Save();
            return Ok(foundDealer);
        }

        [HttpPost, Route("{dealerId:int}/cars/{carId:int}")]
        public IActionResult AttachCarToDealer(int dealerId, int carId)
        {
            var foundDealer = _dealerRepository.GetDealer(dealerId);
            if (foundDealer == null)
                return NotFound($"Dealer with id {dealerId} doesn't exist");

            var foundCar = _carRepository.GetCar(carId);
            if (foundCar == null)
                return NotFound($"Car with id {carId} doesn't exist");

            if (!foundDealer.Cars.Contains(foundCar))
            {
                foundDealer.Cars.Add(foundCar);
                _dealerRepository.Save();
            }

            return Ok(foundDealer);
        }

        [HttpDelete, Route("{dealerId:int}/cars/{carId:int}")]
        public IActionResult DetachCarFromDealer(int dealerId, int carId)
        {
            var foundDealer = _dealerRepository.GetDealer(dealerId);
            if (foundDealer == null)
                return NotFound($"Dealer with id {dealerId} doesn't exist");

            var foundCar = _carRepository.GetCar(carId);
            if (foundCar == null)
                return NotFound($"Car with id {carId} doesn't exist");

            if (foundDealer.Cars.Contains(foundCar))
            {
                foundDealer.Cars.Remove(foundCar);
                _dealerRepository.Save();
            }

            return Ok(foundDealer);
        }

        [HttpPost]
        public Dealer CreateDealer([FromBody] Dealer dealer)
        {
            _dealerRepository.Create(dealer);
            _dealerRepository.Save();
            return dealer;
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeleteDealer(int id)
        {
            var foundDealer = _dealerRepository.GetDealer(id);
            if (foundDealer == null)
                return NotFound();
            _dealerRepository.Delete(id);
            _dealerRepository.Save();
            return NoContent();
        }
    }
}
