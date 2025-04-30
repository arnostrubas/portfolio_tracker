using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class MainDatabaseDbContext : DbContext
    {
        private readonly string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog = PortfolioManager; Integrated Security = true";
        
        public DbSet<Portfolio> Portfolios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
