using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using QuerySQLDapperMicroService.Data;
using QuerySQLDapperMicroService.Service;

namespace QuerySQLDapperMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportSQLController : ControllerBase
    {
        private readonly QuerySQLDapperMicroServiceContext _context;

        public AirportSQLController(QuerySQLDapperMicroServiceContext context)
        {
            _context = context;
        }

        // GET: api/AirportSQL/5
        [HttpGet("{id}")]
        public ActionResult<AirportSQL> GetAirportSQLById(int id)
        {
            var airportSQL = new AirportService().GetById(id);

            if (airportSQL == null)
            {
                return BadRequest("\nNo data with this Id was found! Check if the Id inserted is correct and if there is any data inserted in your database!");
            }

            return airportSQL;

        }

        // GET: api/AirportSQL/5/Code
        [HttpGet("code/{code}")]
        public ActionResult<AirportSQL> GetAirportSQLByCode(string code)
        {
            var airportSQL = new AirportService().GetByCode(code);

            if (airportSQL == null)
            {
                return BadRequest("\nNo data with this Code was found! Check if the Id inserted is correct and if there is any data inserted in your database!");
            }

            return airportSQL;
        }
    }
}
