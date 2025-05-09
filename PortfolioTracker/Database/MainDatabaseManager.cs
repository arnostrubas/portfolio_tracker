using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public static class MainDatabaseManager
    {
        public static readonly MainDatabaseDbContext manager = new();

        private static readonly ObservableCollection<string> portfolios = manager.PortfolioNames();
        public static ObservableCollection<string> GetPortfolios() => portfolios;

        /// <summary>
        /// Creates a PortfolioDbContext and ensures database for the portfolio is created
        /// </summary>
        public static async Task<PortfolioManagerDbContext> CreateDb(Portfolio portfolio)
        {
            var portfolioDatabase = new PortfolioManagerDbContext(portfolio.ConnectionString);
            await portfolioDatabase.Database.EnsureCreatedAsync();
            await AddPortfolio(portfolio);
            return portfolioDatabase;
        }

        /// <summary>
        /// Adds a portfolio to manager database (expects that the portfolio with the same name isnt in the database
        /// Used only in constructor
        /// </summary>
        private static async Task AddPortfolio(Portfolio portfolio)
        {
            portfolios.Add(portfolio.Name);
            manager.Portfolios.Add(portfolio);
            await manager.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a portfolio from the database.
        /// Dispose PortfolioManager after
        /// </summary>
        public static async void RemovePortfolio(Portfolio portfolio)
        {
            manager.Portfolios.Remove(portfolio);
            portfolios.Remove(portfolio.Name);
            await manager.SaveChangesAsync();
        }

        public static async void UpdatePortfolio(Portfolio portfolio)
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    var CurrentPortfolio = manager.Portfolios.FirstOrDefault(p => p.Name == portfolio.Name);
                    if (CurrentPortfolio != null)
                    {
                        CurrentPortfolio.CurrentValue = portfolio.CurrentValue;
                        CurrentPortfolio.NumberOfOrders = portfolio.NumberOfOrders;
                        CurrentPortfolio.Profit = portfolio.Profit;
                        CurrentPortfolio.Invested = portfolio.Invested;
                        await manager.SaveChangesAsync();
                    }
                    success = true;
                }
                catch { }
            }
        }
    }
}
