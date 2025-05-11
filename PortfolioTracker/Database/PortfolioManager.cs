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
using System.CodeDom;
using PortfolioTracker.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PortfolioTracker.Database
{
    /// <summary>
    /// Class that manages a single portfolio from PortfolioManager Database
    /// </summary>
    public class PortfolioManager : IDisposable
    {
        public Portfolio Portfolio {  get; private set; }
        public PortfolioManagerDbContext PortfolioDatabase { get; private set; } = null!;

        private readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly CancellationToken cancellationToken = new();
        public List<Company> Companies { get; private set; } = new();
        public Events UpdateHandler { get; set; }

        /// <summary>
        /// Constructor for PortfolioManager
        /// Manages a portfolio. If the portfolio doesnt exist, it creates a new empty one.
        /// </summary>
        /// <param name="portfolioName"> Name of the portfolio to be managed. </param>
        public PortfolioManager(string portfolioName)
        {
            UpdateHandler = new Events();
            var exists = true;
            var existing = MainDatabaseManager.manager.Portfolios.FirstOrDefault(p => p.Name == portfolioName);
            if (existing != null) { 
                Portfolio = existing; 
                PortfolioDatabase = new PortfolioManagerDbContext(ConnectionString.GetConnectionString(portfolioName)); 
            }
            else {
                this.Portfolio = new Portfolio(portfolioName);
                exists = false;
            }
            Start(exists);
        }

        private async void Start(bool exists)
        {
            if (!exists) PortfolioDatabase = await MainDatabaseManager.CreateDb(this.Portfolio);
            await UpdateCompanies();
            UpdatePrices();
        }

        private async Task UpdateCompanies()
        {
            try
            {
                Companies = new List<Company>();
                foreach (var company in PortfolioDatabase.Orders.GroupBy(o => o.Ticker))
                {
                    var buyOrders = company.Where(o => o.OrderType == (int)OrderType.Buy);
                    var buyAmount = buyOrders.Select(o => o.Amount).Sum();
                    var sellAmount = company.Where(o => o.OrderType == (int)OrderType.Sell).Select(o => o.Amount).Sum();
                    decimal owned = buyAmount - sellAmount;
                    decimal invested = 0;
                    foreach (var order in buyOrders)
                    {
                        if (sellAmount <= 0) invested += order.Amount * order.Price;
                        else if (sellAmount - order.Amount < 0) invested += (order.Amount - sellAmount) * order.Price;
                        else if (sellAmount - order.Amount > 0) sellAmount -= order.Amount;
                    }
                    var price = await StockPrice.Price(company.Key);
                    var newCompany = new Company(company.Key, Math.Round(owned, 2), Math.Round(invested / owned, 2), price);
                    Companies.Add(newCompany);
                    UpdateHandler.OnUpdate();
                }
            }
            catch { }
        }

        /// <summary>
        /// Adds an order to the database (unless it already is in the database)
        /// </summary>
        /// <param name="order">Order to be added</param>
        /// <returns></returns>
        public async Task AddOrder(Order order)
        {
            try
            {
                await StockPrice.Price(order.Ticker);
                if (!PortfolioDatabase.Orders.Any(o => o.Ticker == order.Ticker && o.OrderType == order.OrderType 
                                                        && o.Amount == order.Amount && o.Price == order.Price))
                {
                    PortfolioDatabase.Orders.Add(order);
                    await PortfolioDatabase.SaveChangesAsync();
                    await UpdateCompanies();
                    UpdatePortfolioStats();
                }
            }
            catch { }
        }

        /// <summary>
        /// Removes a order from the database
        /// </summary>
        /// <param name="order">Order returned from GetOrder</param>
        /// <returns></returns>
        public async Task RemoveOrder(Order? order)
        {
            try
            {
                await PortfolioDatabase.Database.EnsureCreatedAsync();
                if (order != null)
                {
                    PortfolioDatabase.Orders.Remove(order);
                    await PortfolioDatabase.SaveChangesAsync();
                }
                await UpdateCompanies();
                UpdatePortfolioStats();
            }
            catch { }
        }

        /// <summary>
        /// Returns order with orderID or null
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<Order?> GetOrder(int orderID)
        {
            try
            {
                return await PortfolioDatabase.Orders.FirstOrDefaultAsync(p => p.Id == orderID);
            }
            catch { return null; }
        }

        /// <summary>
        /// Updates prices in the database every minute
        /// </summary>
        /// <returns>Task performing the updates</returns>
        public async Task UpdatePrices() => await Task.Run(async () =>
        {
            await Task.Delay(1000);
            int secondsBetweenUpdates = 60;
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    secondsBetweenUpdates = 60;
                    foreach (Order order in PortfolioDatabase.Orders.Where(o => o.OrderType != (int)OrderType.Sell))
                    {
                        try
                        {
                            order.CurrentPrice = await StockPrice.Price(order.Ticker);
                        }
                        catch
                        {
                            secondsBetweenUpdates = 5;
                        }
                    }
                    await PortfolioDatabase.SaveChangesAsync().ContinueWith(_ => UpdatePortfolioStats());
                    await Task.Delay(secondsBetweenUpdates * 1000, cancellationToken);
                }
                catch { }
            }
        });

        /// <summary>
        /// Update the portfolioStats every minute
        /// </summary>
        private void UpdatePortfolioStats()
        {
            try
            {
                decimal invested = 0;
                decimal currentValue = 0;
                bool success = true;
                foreach (var company in Companies)
                {
                   try
                   {
                        invested += company.BuyPrice * company.Owned;
                        currentValue += company.CurrentPrice * company.Owned;
                   }
                   catch
                   {
                        success = false;
                        break;
                   }
                }
                if (success)
                {
                    Portfolio.Invested = invested;
                    Portfolio.CurrentValue = currentValue;
                    Portfolio.Profit = Portfolio.CurrentValue - Portfolio.Invested;
                    MainDatabaseManager.UpdatePortfolio(Portfolio);
                    UpdateHandler.OnUpdate();
                }
            }
            catch { }
        }

        public async void Dispose()
        {
            try
            {
                this.cancellationTokenSource.Cancel();
                await Task.Delay(1000); // to give time for the tasks to end
                this.cancellationTokenSource.Dispose();
                PortfolioDatabase.Dispose();
            }
            catch { }
        }
    }
}