﻿using System.Collections.Generic;
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

                return NotFound("Ticket not found!");

            }

            return ticket;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {

            if (await _ticket.Create(ticket) == null)
            {

                return BadRequest("The user is not authorized, the log insertion went wrong or there is a problem with the base price, flight and/or passenger data!");

            }
            
            return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);

        }


        [HttpPut(("{id:length(24)}"))]
        public async Task<IActionResult> Update(string id, Ticket ticket_updated)
        {

            var ticket = _ticket.Get(id);

            if (ticket == null)
            {

                return NotFound("Ticket not found!");

            }


            if (await _ticket.Update(id, ticket_updated) != null)
            {

                return Ok("Ticket successfully updated!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");


        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id, User user)
        {

            var ticket = _ticket.Get(id);

            if (ticket == null)
            {

                return NotFound("Ticket not found!");

            }

            if (await _ticket.Remove(id, user) != null)
            {

                return Ok("Ticket successfully removed!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }


    }
}
