using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class RequestContext : DbContext
    {
        public RequestContext (DbContextOptions<RequestContext> options)
            : base(options)
        {
        }

        public DbSet<Request> Request { get; set; } 
    }
