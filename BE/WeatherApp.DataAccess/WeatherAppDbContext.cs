using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess
{
    public class WeatherAppDbContext:DbContext
    {
        public WeatherAppDbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

        }

        }
    }
