using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Models;
using PortfolioTracker;
using System.Windows.Controls;

namespace PortfolioTracker.Database
{
    public class PortfolioManager
    {
        private Portfolio portfolio;
        portfolioDbContext db;
        public PortfolioManager(string portfolioName)
        {
            this.portfolio = new Portfolio(portfolioName);
            db = new portfolioDbContext(this.portfolio.ConnectionString);
            AddDatabase();
        }

        private async void AddDatabase()
        {
            using var manager = new PortfolioManagerDbContext();
            await manager.Database.EnsureCreatedAsync();
            if (!manager.Portfolios.Any(portfolio => portfolio.Name == this.portfolio.Name))
            {
                manager.Portfolios.Add(this.portfolio);
                await manager.SaveChangesAsync();
            }
        }

        public async Task AddOrder(Order order)
        {
            await db.Database.EnsureCreatedAsync();
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
    }
}
