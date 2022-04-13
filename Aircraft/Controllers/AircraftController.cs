using System.Collections.Generic;
using System.Threading.Tasks;
using AircraftMicroService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AircraftMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {

        private readonly AircraftService _aircraft;

        public AircraftController(AircraftService service)
        {

            _aircraft = service;

        }

        [HttpGet]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraft.Get();

        [HttpGet("{id:length(24)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> Get(string id)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {
                return NotFound("Aircraft not found!");
            }

            return aircraft;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Aircraft aircraft)
        {


            if (await _aircraft.Create(aircraft) == null)
            {

                return BadRequest("The user is not authorized, the log insertion went wrong or the aircraft already exists!");

            }

            return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);

        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Aircraft aircraft_updated)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {

                return NotFound("Aircraft not found!");

            }

            if(await _aircraft.Update(id, aircraft_updated) != null)
            {

                return Ok("Aircraft successfully updated!");

            }
            
            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, User user)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {

                return NotFound("Aircraft not found!");

            }
            if(await _aircraft.Remove(id, user) != null)
            {

                return Ok("Aircraft successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }



    }
}
