using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class portfolioDbContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog = Portfolio; Integrated Security =true";

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
