
using System;
using System.Threading.Tasks;
using Model;
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

            Assert.True(status);    
            
        }

        [Fact]
        public void GetById()
        {

            var ticket_service = InitializeDataBase();
            var ticket = ticket_service.Get("62558d1c8b3ae8776c8e3d4e");
            if (ticket == null)
                ticket = new Model.Ticket();

            Assert.Equal("62558d1c8b3ae8776c8e3d4e", ticket.Id);

        }

        [Fact]
        public async void Create()
        {

            var ticket_service = InitializeDataBase();
            Model.Ticket ticket = new()
            {

                TotalValue = 560,
                UserLogin = "shuda"
                
            };

            var ticket_return = await ticket_service.Create(ticket);

            Assert.Equal("shuda", ticket_return.UserLogin);

        }

        [Fact]
        public async void Update()
        {

            var ticket_service = InitializeDataBase();
            Model.Ticket ticket = new()
            {

                Id = "62574ef7f1269964206f017a",
                TotalValue = 1000,
                UserLogin = "shuda"

            };

            var ticket_return = await ticket_service.Update("62574ef7f1269964206f017a", ticket);

            Assert.Equal(1000, ticket_return.TotalValue);

        }

        [Fact]

        public void Delete()
        {

            var ticket_service = InitializeDataBase();
            var ticketToRemove = ticket_service.Get("62574ef7f1269964206f017a");

            ticket_service.Remove(ticketToRemove);

            ticketToRemove = ticket_service.Get("62574ef7f1269964206f017a");

            Assert.Null(ticketToRemove);
        }



    }
}
