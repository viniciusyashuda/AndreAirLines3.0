using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using QuerySQLEntityFrameworkMicroService.Data;

namespace QuerySQLEntityFrameworkMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportSQLController : ControllerBase
    {
        private readonly QuerySQLEntityFrameworkMicroServiceContext _context;

        public AirportSQLController(QuerySQLEntityFrameworkMicroServiceContext context)
        {
            _context = context;
        }


        // GET: api/AirportSQL/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportSQL>> GetAirportSQLById(int id)
        {
            var airportSQL = await _context.Airport.FindAsync(id);

            if (airportSQL == null)
            {
                return BadRequest("\nNo data with this Id was found! Check if the Id inserted is correct and if there is any data inserted in your database!");
            }

            return airportSQL;
        }        

        // GET: api/AirportSQL/5/Code
        [HttpGet("code/{code}")]
        public async Task<ActionResult<AirportSQL>> GetAirportSQLByCode(string code)
        {
            var airportSQL = await _context.Airport.Where(airport => airport.Code == code).FirstOrDefaultAsync();

            if (airportSQL == null)
            {
                return BadRequest("\nNo data with this Code was found! Check if the Id inserted is correct and if there is any data inserted in your database!");
            }

            return airportSQL;
        }

    }
}
