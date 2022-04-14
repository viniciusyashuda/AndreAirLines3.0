using System.Collections.Generic;
using System.Threading.Tasks;
using AircraftMicroService.Service;
using AircraftMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AircraftMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {

        private readonly AircraftService _aircraft;

        public AircraftController(AircraftService service)
        {

            _aircraft = service;

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
            else if(user.Password != model.Password)
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
        public ActionResult<List<Aircraft>> Get() =>
            _aircraft.Get();

        [HttpGet("{id:length(24)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> Get(string id)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {
                return NotFound("Aircraft not found!");
            }

            return aircraft;

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Aircraft aircraft)
        {


            if (await _aircraft.Create(aircraft) == null)
            {

                return BadRequest("The user is not authorized, the log insertion went wrong or the aircraft already exists!");

            }

            return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);

        }

        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, Aircraft aircraft_updated)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {

                return NotFound("Aircraft not found!");

            }

            if(await _aircraft.Update(id, aircraft_updated) != null)
            {

                return Ok("Aircraft successfully updated!");

            }
            
            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id, User user)
        {

            var aircraft = _aircraft.Get(id);

            if (aircraft == null)
            {

                return NotFound("Aircraft not found!");

            }
            if(await _aircraft.Remove(id, user) != null)
            {

                return Ok("Aircraft successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }



    }
}
