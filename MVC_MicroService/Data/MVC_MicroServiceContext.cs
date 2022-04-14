using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace MVC_MicroService.Data
{
    public class MVC_MicroServiceContext : DbContext
    {
        public MVC_MicroServiceContext (DbContextOptions<MVC_MicroServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Model.AirportSQL> Airport { get; set; }

        //public DbSet<Model.Airport> Airport { get; set; }
    }
}
