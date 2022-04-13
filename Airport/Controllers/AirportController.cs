using System.Collections.Generic;
using System.Threading.Tasks;
using AirportMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace AirportMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {

        private readonly AirportService _airport;

        public AirportController(AirportService service)
        {
            _airport = service;
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
        public ActionResult<List<Airport>> Get() =>
            _airport.Get();

        [HttpGet("{id:length(24)}", Name = "GetAirport")]
        public ActionResult<Airport> Get(string id)
        {

            var airport = _airport.Get(id);

            if(airport == null)
            {

                return NotFound("Airport not found!");

            }

            return airport;

        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Airport airport)
        {

            var address_viacep = await ViaCep.GetAddressViaCep(airport.Address.PostalCode);

            if(address_viacep != null)
            {

                airport.Address = new Address(address_viacep.PostalCode, address_viacep.Street, airport.Address.Number, address_viacep.District, address_viacep.City, airport.Address.Country, airport.Address.Continent, address_viacep.Federative_Unit, airport.Address.Complement);

            }

            if(await _airport.Create(airport) == null)
            {
                return BadRequest("The user is not authorized, the log insertion went wrong or the airport already exists!");
            }

            return CreatedAtRoute("GetAirport", new { id = airport.Id.ToString()}, airport);
               
        }

        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, Airport airport_updated)
        {

            var airport = _airport.Get(id);

            if(airport == null)
            {

                return NotFound("Airport not found!");

            }

            if(await _airport.Update(id, airport_updated) != null)
            {

                return Ok("Airport successfully updated!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }


        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id, User user)
        {

            var airport = _airport.Get(id);


            if(airport == null)
            {

                return NotFound("Airport not found!");

            }

            if (await _airport.Remove(id, user) != null)
            {

                return Ok("Airport successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }


    }
}
