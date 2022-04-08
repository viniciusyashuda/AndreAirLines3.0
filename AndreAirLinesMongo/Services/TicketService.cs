using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver;
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

            _ticket.InsertOne(ticket);
            return ticket;

        }

        public void Update(string id, Ticket ticket_updated) =>
            _ticket.ReplaceOne(ticket => ticket.Id == id, ticket_updated);

        public void Remove(Ticket ticketToRemove) =>
            _ticket.DeleteOne(ticket => ticket.Id == ticketToRemove.Id);

        public void Remove(string id) =>
            _ticket.DeleteOne(ticket => ticket.Id == id);

    }
}
