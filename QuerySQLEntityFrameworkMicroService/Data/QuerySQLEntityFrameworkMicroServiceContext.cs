using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace QuerySQLEntityFrameworkMicroService.Data
{
    public class QuerySQLEntityFrameworkMicroServiceContext : DbContext
    {
        public QuerySQLEntityFrameworkMicroServiceContext (DbContextOptions<QuerySQLEntityFrameworkMicroServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Model.AirportSQL> Airport { get; set; }
    }
}
