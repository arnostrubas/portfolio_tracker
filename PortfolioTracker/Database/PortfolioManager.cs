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
    /// <summary>
    /// Class that manages a single portfolio from PortfolioManager Database
    /// </summary>
    public class PortfolioManager
    {
        private readonly PortfolioManagerDbContext manager = new ();
        public Portfolio portfolio;
        public PortfolioDbContext portfolioDatabase = null!;

        /// <summary>
        /// Constructor for PortfolioManager
        /// Manages a portfolio. If the portfolio doesnt exist, it creates a new empty one.
        /// </summary>
        /// <param name="portfolioName"> Name of the portfolio to be managed. </param>
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

        /// <summary>
        /// Creates a PortfolioDbContext and ensures database for the portfolio is created
        /// </summary>
        private async void CreateDb()
        {
            this.portfolioDatabase = new PortfolioDbContext(this.portfolio.ConnectionString);
            await portfolioDatabase.Database.EnsureCreatedAsync();
        }

        /// <summary>
        /// Adds a portfolio to manager database (expects that the portfolio with the same name isnt in the database
        /// Used only in constructor
        /// </summary>
        private async void AddPortfolio()
        {
            manager.Portfolios.Add(this.portfolio);
            await manager.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a portfolio from the database.
        /// Dispose manager after
        /// </summary>
        public async void RemovePortfolio()
        {
            manager.Portfolios.Remove(this.portfolio);
            await manager.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an order to the database
        /// </summary>
        /// <param name="order">Order to be added</param>
        /// <returns></returns>
        public async Task AddOrder(Order order)
        {
            portfolioDatabase.Orders.Add(order);
            await portfolioDatabase.SaveChangesAsync();
        }
        
        /// <summary>
        /// Removes a order from the database
        /// </summary>
        /// <param name="order">Order returned from GetOrder</param>
        /// <returns></returns>
        public async Task RemoveOrder(Order order)
        {
            if (order != null) {
                portfolioDatabase.Orders.Remove(order);
                await portfolioDatabase.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Returns order with orderID or null
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<Order?> GetOrder(int orderID)
        {
            return await portfolioDatabase.Orders.FirstOrDefaultAsync(p => p.Id == orderID);
        }

    }
}
