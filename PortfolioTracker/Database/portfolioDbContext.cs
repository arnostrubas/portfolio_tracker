using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class PortfolioDbContext : DbContext
    {
        private string connectionString = "";

        public DbSet<Order> Orders { get; set; }
        public PortfolioDbContext(string connectionString) 
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
