using System.Collections.Generic;
using System.Threading.Tasks;
using LogMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LogMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogService _log;

        public LogsController(LogService service)
        {

            _log = service;

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Log>>> GetAsync() =>
            await _log.GetAsync();

        [HttpGet("{id:length(24)}", Name = "GetLog")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Log>> GetAsync(string id)
        {

            var log = await _log.GetAsync(id);

            if (log == null)
            {
                return NotFound("Log not found!");
            }

            return log;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Log log)
        {

            if (await _log.Create(log) == null)
            {

                return BadRequest("Log already exist!");

            }

            return CreatedAtRoute("GetLog", new { id = log.Id.ToString() }, log);

        }


    }
}
