using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class PortfolioManagerDbContext(string connectionString) : DbContext
    {
        private readonly string connectionString = connectionString;

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
