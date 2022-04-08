using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace QuerySQLDapperMicroService.Data
{
    public class QuerySQLDapperMicroServiceContext : DbContext
    {
        public QuerySQLDapperMicroServiceContext (DbContextOptions<QuerySQLDapperMicroServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Model.AirportSQL> AirportSQL { get; set; }
    }
}
