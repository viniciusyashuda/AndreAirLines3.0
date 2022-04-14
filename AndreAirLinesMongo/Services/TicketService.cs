using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver;
using Newtonsoft.Json;
using TicketMicroService.Config;

namespace TicketMicroService.Services
{
    public class TicketService
    {

        private readonly IMongoCollection<Ticket> _ticket;

        public TicketService(ITicketDatabaseSettings settings)
        {

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _ticket = database.GetCollection<Ticket>(settings.TicketCollectionName);

        }

        public List<Ticket> Get() =>
            _ticket.Find(ticket => true).ToList();

        public Ticket Get(string id) =>
            _ticket.Find(ticket => ticket.Id == id).FirstOrDefault();

        public async Task<Ticket> Create(Ticket ticket)
        {

            if (ticket.UserLogin == null)
            {

                return null;

            }

            var user = await SearchUser.FindUserAsync(ticket.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var passenger = await SearchPassenger.FindPassengerAsync(ticket.Passenger.Id);

            if (passenger == null)
            {

                return null;

            }

            var flight = await SearchFlight.FindFlightAsync(ticket.Flight.Id);

            if (flight == null)
            {

                return null;

            }

            var base_price = await SearchBasePrice.FindBasePriceAsync(ticket.BasePrice.Id);

            if (base_price == null)
            {

                return null;

            }


            ticket.Flight = flight;
            ticket.BasePrice = base_price;
            ticket.Passenger = passenger;
            ticket.TotalValue = (base_price.Value + ticket.Class.Class_Value) * (1 - ticket.SalePercentage);

            _ticket.InsertOne(ticket);

            Log log = new();
            log.User = user;
            log.EntityBefore = "";
            log.EntityAfter = JsonConvert.SerializeObject(ticket);
            log.Operation = "create";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _ticket.DeleteOne(ticketIn => ticketIn.Id == ticket.Id);
                return null;

            }

            return ticket;

        }

        public async Task<Ticket> Update(string id, Ticket ticket_updated)
        {

            if (ticket_updated.UserLogin == null)
            {

                return null;

            }
            var user = await SearchUser.FindUserAsync(ticket_updated.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var passenger = await SearchPassenger.FindPassengerAsync(ticket_updated.Passenger.Id);

            if (passenger == null)
            {

                return null;

            }

            var flight = await SearchFlight.FindFlightAsync(ticket_updated.Flight.Id);

            if (flight == null)
            {

                return null;

            }

            var base_price = await SearchBasePrice.FindBasePriceAsync(ticket_updated.BasePrice.Id);

            if (base_price == null)
            {

                return null;

            }

            var ticket = Get(id);

            _ticket.ReplaceOne(ticketIn => ticketIn.Id == id, ticket_updated);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(ticket);
            log.EntityAfter = JsonConvert.SerializeObject(ticket_updated);
            log.Operation = "update";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _ticket.ReplaceOne(ticketIn => ticketIn.Id == ticket_updated.Id, ticket);
                return null;

            }

            return ticket_updated;

        }

        public void Remove(Ticket ticketToRemove) =>
            _ticket.DeleteOne(ticket => ticket.Id == ticketToRemove.Id);

        public async Task<User> Remove(string id, User user)
        {

            if (user.UserLogin == null)
            {

                return null;

            }

            user = await SearchUser.FindUserAsync(user.UserLogin);

            if (user == null)
            {

                return null;

            }
            if (user.Role != "Admin" && user.Role != "User")
            {

                return null;

            }

            var ticket = Get(id);

            _ticket.DeleteOne(ticketIn => ticketIn.Id == id);

            Log log = new();
            log.User = user;
            log.EntityBefore = JsonConvert.SerializeObject(ticket);
            log.EntityAfter = "";
            log.Operation = "delete";
            log.Date = DateTime.Now.Date;

            var check = await InsertLog.InsertLogAsync(log);

            if (check != "Ok")
            {

                _ticket.InsertOne(ticket);
                return null;

            }

            return user;

        }

    }
}
