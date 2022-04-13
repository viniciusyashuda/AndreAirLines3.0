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
                return NotFound("Flight not found!");
            }

            return flight;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            

            if (await _flight.Create(flight) == null)
            {

                return BadRequest("The user is not authorized, the log insertion went wrong or there is a problem with the airports and/or aircraft data!");
                
            }

                return CreatedAtRoute("GetFlight", new { id = flight.Id.ToString() }, flight);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update (string id, Flight flight_updated)
        {

            var flight = _flight.Get(id);

            if (flight == null)
            {
                return NotFound("Flight not found!");
            }

            if (await _flight.Update(id, flight_updated) != null)
            {

                return Ok("Flight successfully updated!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }

        [HttpDelete("{id:length(24)}")]

        public async Task<IActionResult> Remove(string id, User user)
        {

            var flight = _flight.Get(id);

            if (flight == null)
            {
                return NotFound("Flight not found!");
            }

            if (await _flight.Remove(id, user) != null)
            {

                return Ok("Flight successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");
        }



    }
}
