using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class MainDatabaseDbContext : DbContext
    {
        private readonly string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog = PortfolioManager; Integrated Security = true";
        public DbSet<Portfolio> Portfolios { get; set; }
        public ObservableCollection<string> NamesOfPortfolios()
        {
            var collection = new ObservableCollection<string>();
            foreach (var portfolio in Portfolios) collection.Add(portfolio.Name);
            return collection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
