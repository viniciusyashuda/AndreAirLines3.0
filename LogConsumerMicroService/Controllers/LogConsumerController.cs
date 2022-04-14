using System.Threading.Tasks;
using LogConsumerMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LogConsumerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogConsumerController : ControllerBase
    {

        private readonly LogConsumerService _logconsumer;

        public LogConsumerController(LogConsumerService service)
        {
            _logconsumer = service;
        }


        [HttpGet("{id:length(24)}", Name = "GetLog")]
        public async Task<ActionResult<Log>> Get(string id)
        {

            var log = await _logconsumer.Get(id);

            if (log == null)
            {
                return NotFound("Log not found!");
            }

            return log;

        }


        [HttpPost]
        public async Task<IActionResult> Create(Log log)
        {

            var log_returned = _logconsumer.Create(log);
           
            return CreatedAtRoute("GetLog", new { id = log.Id }, log);

        }

    }
}
