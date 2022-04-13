
using System;
using TicketMicroService.Config;
using TicketMicroService.Services;
using Xunit;

namespace TicketTest
{
    public class TicketTest
    {
        public TicketService InitializeDataBase()
        {
            var settings = new TicketDatabaseSettings();
            TicketService ticket_service = new(settings);
            return ticket_service;
        }

        [Fact]
        public void Get()
        {

            var ticket_service = InitializeDataBase();
            var ticket = ticket_service.Get();
            var status = false;

            if (ticket.Count > 0)
                status = true;

            Assert.Equal(true, status);    
            
        }
    }
}
