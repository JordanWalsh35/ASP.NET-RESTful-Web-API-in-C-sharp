using CarAPI.Data;
using CarAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Controllers
{
    [Route("api/CarAPI")]
    [ApiController]
    public class CarAPIController : ControllerBase
    {
        private ApplicationDBContext _db;

        public CarAPIController(ApplicationDBContext db) 
        {
            _db = db;
        }


        // Get all available records
        [HttpGet]
        public ActionResult<IEnumerable<CarDTO>> GetCars()
        {
            return Ok(_db.Car.ToList());
        }


        // Get an individual car based on an Id
        [HttpGet("{id:int}", Name ="GetCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDTO> GetCar(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var car = _db.Car.FirstOrDefault(u => u.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }


        // Create a new Car entry
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDTO> CreateCar([FromBody]CarDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest(carDTO);
            }

            if (carDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Car model = new()
            {
                Id = carDTO.Id,
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                Year = carDTO.Year,
                EngineSize = carDTO.EngineSize,
                BHP = carDTO.BHP,
                TimeCreated = DateTime.Now
            };

            _db.Car.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetCar", new { id = carDTO.Id }, carDTO);
        }


        // Delete a Car
        [HttpDelete("{id:int}", Name = "DeleteCar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCar(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var car = _db.Car.FirstOrDefault(u => u.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            _db.Car.Remove(car);
            _db.SaveChanges();
            return NoContent();
        }


        // Update using Put (all properties)
        [HttpPut("{id:int}", Name = "UpdateCar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCar(int id, [FromBody]CarDTO carDTO)
        {
            if (carDTO == null || id != carDTO.Id)
            {
                return BadRequest();
            }

            Car model = new()
            {
                Id = carDTO.Id,
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                Year = carDTO.Year,
                EngineSize = carDTO.EngineSize,
                BHP = carDTO.BHP,
                TimeCreated = DateTime.Now
            };

            _db.Car.Update(model);
            _db.SaveChanges();

            return NoContent();
        }


        // Update using Patch (single property)
        [HttpPatch("{id:int}", Name = "UpdateCarPartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCarPartial(int id, JsonPatchDocument<CarDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var car = _db.Car.AsNoTracking().FirstOrDefault(u => u.Id == id);

            CarDTO carDTO = new()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                EngineSize = car.EngineSize,
                BHP = car.BHP,
            };

            if (car == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(carDTO, ModelState);

            Car model = new Car()
            {
                Id = carDTO.Id,
                Brand = carDTO.Brand,
                Model = carDTO.Model,
                Year = carDTO.Year,
                EngineSize = carDTO.EngineSize,
                BHP = carDTO.BHP,
                TimeCreated = DateTime.Now
            };

            _db.Car.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
