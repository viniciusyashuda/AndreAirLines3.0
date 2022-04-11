using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using UserMicroService.Services;

namespace UserMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserService _user;

        public UsersController(UserService service)
        {
            _user = service;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _user.Get();

        [HttpGet("{login}", Name = "GetUser")]

        public ActionResult<User> Get(string login)
        {

            var user = _user.Get(login);

            if (user == null)
            {

                NotFound();

            }

            return user;

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {

            var address_viacep = await ViaCep.GetAddressViaCep(user.Address.PostalCode);

            if (address_viacep != null)
            {

                user.Address = new Address(address_viacep.PostalCode, address_viacep.Street, user.Address.Number, address_viacep.District, address_viacep.City, user.Address.Country, user.Address.Continent, address_viacep.Federative_Unit, user.Address.Complement);

            }

            if (!ValidateCPF.CpfValidator(user.Cpf))
            {
                return BadRequest("This CPF is invalid!");
            }
            else if (user.Cpf == "00000000000" || user.Cpf == "11111111111" || user.Cpf == "22222222222" || user.Cpf == "33333333333" || user.Cpf == "44444444444" || user.Cpf == "55555555555" || user.Cpf == "66666666666" || user.Cpf == "77777777777" || user.Cpf == "88888888888" || user.Cpf == "99999999999")
            {
                return BadRequest("This CPF is invalid!");
            }

            if (_user.Create(user) == null)
            {

                return BadRequest();

            }

            return CreatedAtRoute("GetUser", new { login = user.Login.ToString() }, user);

        }


        [HttpPut(("{login}"))]
        public IActionResult Update(string login, User user_updated)
        {

            var user = _user.Get(login);

            if (user == null)
            {

                NotFound();

            }

            _user.Update(login, user_updated);
            return NoContent();

        }

        [HttpDelete("{login}")]
        public IActionResult Remove(string login)
        {

            var user = _user.Get(login);

            if (user == null)
            {

                NotFound();

            }

            _user.Remove(login);
            return NoContent();

        }
    }
}
