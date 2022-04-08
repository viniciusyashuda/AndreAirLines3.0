using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using TicketMicroService.Services;

namespace TicketMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly TicketService _ticket;

        public TicketController(TicketService service)
        {
            _ticket = service;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() =>
            _ticket.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicket")]

        public ActionResult<Ticket> Get(string id)
        {

            var ticket = _ticket.Get(id);

            if (ticket == null)
            {

                NotFound();

            }

            return ticket;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {

            if (await _ticket.Create(ticket) == null)
            {

                return BadRequest("It was not possible to insert because base price, passenger or flight inserted do not exist!");

            }
            
            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);

        }


        [HttpPut(("{id:length(24)}"))]
        public IActionResult Update(string id, Ticket ticket_updated)
        {

            var ticket = _ticket.Get(id);

            if (ticket == null)
            {

                NotFound();

            }

            _ticket.Update(id, ticket_updated);
            return NoContent();

        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Remove(string id)
        {

            var ticket = _ticket.Get(id);

            if (ticket == null)
            {

                NotFound();

            }

            _ticket.Remove(id);
            return NoContent();

        }


    }
}
