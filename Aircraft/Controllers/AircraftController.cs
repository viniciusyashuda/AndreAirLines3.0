using System.Collections.Generic;
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

            if(aircraft == null)
            {
                return NotFound();
            }

            return aircraft;

        }

        [HttpPost]
        public IActionResult Create(Aircraft aircraft)
        {




            if (_aircraft.Create(aircraft) == null)
            {

                return BadRequest("Aircraft already exist!");

            }

            return CreatedAtRoute("GetAircraft", new {id = aircraft.Id.ToString() }, aircraft);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Aircraft aircraft_updated)
        {

            var aircraft = _aircraft.Get(id);

            if(aircraft == null)
            {

                return NotFound();

            }

            _aircraft.Update(id, aircraft_updated);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {

            var aircraft = _aircraft.Get(id);

            if(aircraft == null)
            {

                return NotFound();

            }

            _aircraft.Remove(id);
            return NoContent();
        }



    }
}
