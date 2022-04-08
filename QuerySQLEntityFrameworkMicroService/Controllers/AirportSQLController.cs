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
        public async Task<ActionResult<AirportSQL>> GetAirportSQL(int id)
        {
            var airportSQL = await _context.Airport.FindAsync(id);

            if (airportSQL == null)
            {
                return NotFound();
            }

            return airportSQL;
        }

    }
}
