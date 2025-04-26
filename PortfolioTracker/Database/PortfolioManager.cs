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
using System.Windows.Media.Animation;
using System.DirectoryServices;

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
            CreateDb();
        }

        private async void CreateDb()
        {
            this.db = new portfolioDbContext(this.portfolio.ConnectionString);
            await db.Database.EnsureCreatedAsync();
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
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
        
        public async Task RemoveOrder(Order order)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }

        public async Task<Order?> GetOrder(int orderID)
        {
            return await db.Orders.FirstOrDefaultAsync(p => p.Id == orderID);
        }

    }
}
