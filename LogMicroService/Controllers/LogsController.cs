using System.Collections.Generic;
using LogMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LogMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogService _log;

        public LogsController(LogService service)
        {

            _log = service;

        }

        [HttpGet]
        public ActionResult<List<Log>> Get() =>
            _log.Get();

        [HttpGet("{id:length(24)}", Name = "GetLog")]
        public ActionResult<Log> Get(string id)
        {

            var log = _log.Get(id);

            if (log == null)
            {
                return NotFound();
            }

            return log;

        }

        [HttpPost]
        public IActionResult Create(Log log)
        {

            if (_log.Create(log) == null)
            {

                return BadRequest("Aircraft already exist!");

            }

            return CreatedAtRoute("GetAircraft", new { id = log.Id.ToString() }, log);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Log log_updated)
        {

            var log = _log.Get(id);

            if (log == null)
            {

                return NotFound();

            }

            _log.Update(id, log_updated);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {

            var log = _log.Get(id);

            if (log == null)
            {

                return NotFound();

            }

            _log.Remove(id);
            return NoContent();
        }

    }
}
