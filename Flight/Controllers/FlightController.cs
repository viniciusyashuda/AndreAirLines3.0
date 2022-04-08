using FlightMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        public readonly FlightService _flight;

        public FlightController(FlightService service)
        {
            _flight = service;
        }

        [HttpGet]
        public ActionResult<List<Flight>> Get() =>
            _flight.Get();


        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        public ActionResult<Flight> Get(string id)
        {

            var flight = _flight.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            

            if (await _flight.Create(flight) == null)
            {

                return BadRequest("It was not possible to insert because there is somethig wrong in the airports or aircraft data!");
                
            }

                return CreatedAtRoute("GetFlight", new { id = flight.Id.ToString() }, flight);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update (string id, Flight flight_updated)
        {

            var flight = _flight.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            _flight.Update(id, flight_updated);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]

        public IActionResult Remove(string id)
        {

            var flight = _flight.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            _flight.Remove(id);
            return NoContent();

        }



    }
}
