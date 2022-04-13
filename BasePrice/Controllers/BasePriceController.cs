using System.Collections.Generic;
using System.Threading.Tasks;
using BasePriceMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace BasePriceMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasePriceController : ControllerBase
    {

        private readonly BasePriceService _baseprice;

        public BasePriceController(BasePriceService service)
        {
            _baseprice = service;
        }

        [HttpGet]
        public ActionResult<List<BasePrice>> Get() =>
            _baseprice.Get();

        [HttpGet("{id:length(24)}", Name = "GetBasePrice")]
        public ActionResult<BasePrice> Get(string id)
        {

            var base_price = _baseprice.Get(id);

            if(base_price == null)
            {

                return NotFound("Base price not found!");

            }

            return base_price;

        }

        [HttpPost]
        public async Task <IActionResult> Create(BasePrice base_price)
        {

            if(await _baseprice.Create(base_price) == null)
            {

                return BadRequest("The user is not authorized, the log insertion went wrong or there is a problem with the airports data!");

            }

            return CreatedAtRoute("GetBasePrice", new { id = base_price.Id.ToString() }, base_price);

        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, BasePrice base_price_updated)
        {

            var base_price = _baseprice.Get(id);

            if(base_price == null)
            {

                return NotFound("Base price not found!");

            }

            if (await _baseprice.Update(id, base_price_updated) != null)
            {

                return Ok("Base price successfully updated!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> RemoveAsync(string id, User user)
        {

            var base_price = _baseprice.Get(id);

            if(base_price == null)
            {

                return NotFound("Base price not found!");

            }

            if (await _baseprice.Remove(id, user) != null)
            {

                return Ok("Base price successfully deleted!");

            }

            return BadRequest("The user is not authorized or the log insertion went wrong!");

        }
    }
}
