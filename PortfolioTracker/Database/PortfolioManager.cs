using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Models;
using PortfolioTracker;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.EntityFrameworkCore;

namespace PortfolioTracker.Database
{
    public class PortfolioManager
    {
        private PortfolioManagerDbContext manager = new PortfolioManagerDbContext();
        public Portfolio portfolio;
        public portfolioDbContext db;
        public PortfolioManager(string portfolioName)
        {
            var existing = manager.Portfolios.FirstOrDefault(p => p.Name == portfolioName);
            if (existing != null) { this.portfolio = existing; }
            else { 
                portfolio = new Portfolio(portfolioName);
                AddPortfolio(); 
            }
            this.db = new portfolioDbContext(this.portfolio.ConnectionString);
        }

        private async void AddPortfolio()
        {
            if (!manager.Portfolios.Any(portfolio => portfolio.Name == this.portfolio.Name))
            {
                manager.Portfolios.Add(this.portfolio);
                await manager.SaveChangesAsync();
            }
        }

        private async void RemovePortfolio()
        {
            manager.Portfolios.Remove(this.portfolio);
            await manager.SaveChangesAsync();
        }

        public async Task AddOrder(Order order)
        {
            await db.Database.EnsureCreatedAsync();
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrder(int orderID)
        {
            return await db.Orders.FirstOrDefaultAsync(p => p.Id == orderID);
        }
    }
}
