using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using PassengerMicroService.Services;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassengerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {

        private readonly PassengerService _passenger;

        public PassengerController(PassengerService service)
        {

            _passenger = service;

        }

        [HttpGet]
        public ActionResult<List<Passenger>> Get() =>
            _passenger.Get();


        [HttpGet("{id:length(24)}", Name = "GetPassenger")]
        public ActionResult<Passenger> Get(string id)
        {

            var passenger = _passenger.Get(id);

            if(passenger == null)
            {
                return NotFound();
            }

            return passenger;

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Passenger passenger)
        {

            var address_viacep = await ViaCep.GetAddressViaCep(passenger.Address.PostalCode);

            if (address_viacep != null)
            {

                passenger.Address = new Address(address_viacep.PostalCode, address_viacep.Street, passenger.Address.Number, address_viacep.District, address_viacep.City, passenger.Address.Country, address_viacep.Federative_Unit, passenger.Address.Complement);

            }

            if (!ValidateCPF.CpfValidator(passenger.Cpf))
            {
                return BadRequest("This CPF is invalid!");
            }
            else if(passenger.Cpf == "00000000000" || passenger.Cpf == "11111111111" || passenger.Cpf == "22222222222" || passenger.Cpf == "33333333333" || passenger.Cpf == "44444444444" || passenger.Cpf == "55555555555" || passenger.Cpf == "66666666666" || passenger.Cpf == "77777777777" || passenger.Cpf == "88888888888" || passenger.Cpf == "99999999999")
            {
                return BadRequest("This CPF is invalid!");
            }


            if(_passenger.Create(passenger) == null)
            {

                return BadRequest("Passenger already exist!");

            }

            return CreatedAtRoute("GetPassenger", new { id = passenger.Id.ToString() }, passenger);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Passenger passenger_updated)
        {

            var passenger = _passenger.Get(id);

            if(passenger == null)
            {

                return NotFound();

            }

            _passenger.Update(id, passenger_updated);
            return NoContent();

        }   

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {

            var passenger = _passenger.Get(id);

            if(passenger == null)
            {

                return NotFound();

            }

            _passenger.Remove(passenger);
            return NoContent();
        }


    }
}
