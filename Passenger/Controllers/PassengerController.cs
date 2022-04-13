using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {

            // Recupera o usuário
            var user = await SearchUser.FindUserAsync(model.Login);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "User or password invalid!" });
            else if (user.Password != model.Password)
                return NotFound(new { message = "User or password invalid!" });



            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
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
                return NotFound("Passenger not found!");
            }

            return passenger;

        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create(Passenger passenger)
        {

            var address_viacep = await ViaCep.GetAddressViaCep(passenger.Address.PostalCode);

            if (address_viacep != null)
            {

                passenger.Address = new Address(address_viacep.PostalCode, address_viacep.Street, passenger.Address.Number, address_viacep.District, address_viacep.City, passenger.Address.Country, passenger.Address.Continent, address_viacep.Federative_Unit, passenger.Address.Complement);

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

                return BadRequest("The user is not authorized, the log insertion went wrong or the passenger already exists!");

            }

            return CreatedAtRoute("GetPassenger", new { id = passenger.Id.ToString() }, passenger);

        }

        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Update(string id, Passenger passenger_updated)
        {

            var passenger = _passenger.Get(id);

            if(passenger == null)
            {

                return NotFound("Passenger not found!");

            }

            var address_viacep = await ViaCep.GetAddressViaCep(passenger.Address.PostalCode);

            if (address_viacep != null)
            {

                passenger.Address = new Address(address_viacep.PostalCode, address_viacep.Street, passenger.Address.Number, address_viacep.District, address_viacep.City, passenger.Address.Country, passenger.Address.Continent, address_viacep.Federative_Unit, passenger.Address.Complement);

            }

            if (!ValidateCPF.CpfValidator(passenger.Cpf))
            {

                return BadRequest("This CPF is invalid!");

            }

            else if (passenger.Cpf == "00000000000" || passenger.Cpf == "11111111111" || passenger.Cpf == "22222222222" || passenger.Cpf == "33333333333" || passenger.Cpf == "44444444444" || passenger.Cpf == "55555555555" || passenger.Cpf == "66666666666" || passenger.Cpf == "77777777777" || passenger.Cpf == "88888888888" || passenger.Cpf == "99999999999")
            {

                return BadRequest("This CPF is invalid!");

            }

            if (await _passenger.Update(id, passenger_updated) != null)
            {

                return Ok("Passenger successfully updated!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");


        }   

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(string id, User user)
        {

            var passenger = _passenger.Get(id);

            if(passenger == null)
            {

                return NotFound("Passenger not found!");

            }

            if (await _passenger.Remove(id, user) != null)
            {

                return Ok("Passenger successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }


    }
}
