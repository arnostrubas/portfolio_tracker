using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class portfolioDbContext : DbContext
    {
        private string connectionString = "";

        public DbSet<Order> Orders { get; set; }
        public portfolioDbContext(string connectionString) 
        { 
            this.connectionString = connectionString;
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
