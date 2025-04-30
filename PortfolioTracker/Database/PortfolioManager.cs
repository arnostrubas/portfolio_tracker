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
using System.IO;
using PortfolioTracker.Enums;
using System.Windows;

namespace PortfolioTracker.Database
{
    /// <summary>
    /// Class that manages a single portfolio from PortfolioManager Database
    /// </summary>
    public class PortfolioManager : IDisposable
    {
        public Portfolio portfolio {  get; private set; }
        public PortfolioManagerDbContext portfolioDatabase { get; private set; } = null!;

        private readonly CancellationTokenSource cancellationTokenSource = new();
        private CancellationToken cancellationToken = new();

        /// <summary>
        /// Constructor for PortfolioManager
        /// Manages a portfolio. If the portfolio doesnt exist, it creates a new empty one.
        /// </summary>
        /// <param name="portfolioName"> Name of the portfolio to be managed. </param>
        public PortfolioManager(string portfolioName)
        {
            var existing = MainDatabaseManager.manager.Portfolios.FirstOrDefault(p => p.Name == portfolioName);
            if (existing != null) { this.portfolio = existing; }
            else {
                this.portfolio = new Portfolio(portfolioName);
                MainDatabaseManager.AddPortfolio(this.portfolio);
            }
            CreateDb();
        }

        private async void CreateDb()
        {
            this.portfolioDatabase = await MainDatabaseManager.CreateDb(this.portfolio);
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
            await portfolioDatabase.Database.EnsureCreatedAsync();
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

        /// <summary>
        /// Updates prices in the database every minute
        /// </summary>
        /// <returns>Task performing the updates</returns>
        public Task UpdatePrices() => Task.Run(async () =>
        {
            int secondsBetweenUpdates = 60;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    secondsBetweenUpdates = 60;
                    foreach (Order order in portfolioDatabase.Orders.Where(o => o.OrderType != (int)OrderType.Sell))
                    {
                        try
                        {
                            order.CurrentPrice = await Test.Price(order.Ticker);
                        }
                        catch
                        {
                            secondsBetweenUpdates = 5;
                        }
                    }
                    await portfolioDatabase.SaveChangesAsync().ContinueWith(_ => UpdatePortfolioStats());
                    await Task.Delay(secondsBetweenUpdates * 1000, cancellationToken);
                }
                catch
                {
                }
            }
        });

        /// <summary>
        /// Update the portfolioStats every minute
        /// </summary>
        private async void UpdatePortfolioStats()
        {
            try
            {
                decimal invested = 0;
                decimal currentValue = 0;
                int numberOfOrders = 0;
                bool success = true;

                var companies = portfolioDatabase.Orders.GroupBy(o => o.Ticker);
                foreach (var company in companies)
                {
                   try
                   {
                       var result = await CompanyValue(company);
                       invested += result.invested;
                       currentValue += result.value;
                       numberOfOrders += company.Count();
                   }
                   catch
                   {
                       success = false;
                       break;
                   }
                }
                if (success)
                {
                    portfolio.Invested = invested;
                    portfolio.CurrentValue = currentValue;
                    portfolio.Profit = portfolio.CurrentValue - portfolio.Invested;
                    MainDatabaseManager.UpdatePortfolio(portfolio);
                }
            }
            catch
            {
            }
        }
        
        /// <summary>
        /// Calculates the value of a company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>How much is invested in the company and what is the current value</returns>
        public async static Task<(decimal invested, decimal value)> CompanyValue(IGrouping<string, Order> company)
        {
            var buyOrders = company
                .Where(o => o.OrderType == (int)OrderType.Buy);

            var sellOrders = company
                .Where(o => o.OrderType == (int)OrderType.Sell);

            decimal buyAmount = buyOrders.Sum(o => o.Amount);
            decimal sellAmount = sellOrders.Sum(o => o.Amount);
            var value = (buyAmount - sellAmount) * await Test.Price(company.Key);

            decimal invested = 0;
            foreach (var order in buyOrders) {
                invested += order.Amount * order.Price;
            }
            foreach (var order in sellOrders) { 
                invested -= order.Amount * order.Price;
            }
            
            return (invested, value);
        }


        public async void Dispose()
        {
            this.cancellationTokenSource.Cancel();
            await Task.Delay(1000); // to give time for the tasks to end
            this.cancellationTokenSource.Dispose();
            portfolioDatabase.Dispose();
        }
    }
}