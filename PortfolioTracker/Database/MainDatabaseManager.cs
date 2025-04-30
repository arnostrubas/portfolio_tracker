using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public static class MainDatabaseManager
    {
        public static readonly PortfolioManagerDbContext manager = new();

        /// <summary>
        /// Adds a portfolio to manager database (expects that the portfolio with the same name isnt in the database
        /// Used only in constructor
        /// </summary>
        public static async void AddPortfolio(Portfolio portfolio)
        {
            manager.Portfolios.Add(portfolio);
            await manager.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a portfolio from the database.
        /// Dispose manager after
        /// </summary>
        public static async void RemovePortfolio(Portfolio portfolio)
        {
            manager.Portfolios.Remove(portfolio);
            await manager.SaveChangesAsync();
        }

        public static async void UpdatePortfolio(Portfolio portfolio)
        {
            var CurrentPortfolio = manager.Portfolios.FirstOrDefault(p => p.Name == portfolio.Name);
            if (CurrentPortfolio != null) {
                CurrentPortfolio.CurrentValue = portfolio.CurrentValue;
                CurrentPortfolio.NumberOfOrders = portfolio.NumberOfOrders;
                CurrentPortfolio.Profit = portfolio.Profit;
                CurrentPortfolio.CurrentValue = portfolio.CurrentValue;
                CurrentPortfolio.Invested = portfolio.Invested;
                await manager.SaveChangesAsync();
            }
        }
    }
}
