﻿using Microsoft.EntityFrameworkCore;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Context
{
    public class VilaContext : DbContext
    {
        public VilaContext(DbContextOptions<VilaContext> options):base(options) 
        {
            
        }

        public DbSet<Vila> vilas { get; set; }
        public DbSet<Detail> details { get; set; }
        public DbSet<Customer> customers { get; set; }
    }
}
