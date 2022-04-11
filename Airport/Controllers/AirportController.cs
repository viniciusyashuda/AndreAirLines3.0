using System.Collections.Generic;
using System.Threading.Tasks;
using AirportMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace AirportMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {

        private readonly AirportService _airport;

        public AirportController(AirportService service)
        {
            _airport = service;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get() =>
            _airport.Get();

        [HttpGet("{id:length(24)}", Name = "GetAirport")]

        public ActionResult<Airport> Get(string id)
        {

            var airport = _airport.Get(id);

            if(airport == null)
            {

                return NotFound();

            }

            return airport;

        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Airport airport)
        {

            var address_viacep = await ViaCep.GetAddressViaCep(airport.Address.PostalCode);

            if(address_viacep != null)
            {

                airport.Address = new Address(address_viacep.PostalCode, address_viacep.Street, airport.Address.Number, address_viacep.District, address_viacep.City, airport.Address.Country, airport.Address.Continent, address_viacep.Federative_Unit, airport.Address.Complement);

            }

            if(_airport.Create(airport) == null)
            {
                return BadRequest("Airport already exist!");
            }

            return CreatedAtRoute("GetAirport", new { id = airport.Id.ToString()}, airport);

               
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Airport airport_updated)
        {

            var airport = _airport.Get(id);

            if(airport == null)
            {

                return NotFound();

            }

            _airport.Update(id, airport_updated);
            return NoContent();

        }


        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete (string id)
        {

            var airport = _airport.Get(id);


            if(airport == null)
            {

                return NotFound();

            }

            _airport.Remove(id);
            return NoContent();

        }


    }
}
