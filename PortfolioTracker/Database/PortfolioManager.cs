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

namespace PortfolioTracker.Database
{
    /// <summary>
    /// Class that manages a single portfolio from PortfolioManager Database
    /// </summary>
    public class PortfolioManager
    {
        private readonly PortfolioManagerDbContext manager = new ();
        public Portfolio portfolio;
        private PortfolioDbContext portfolioDatabase = null!;

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
        /// Exports portfolio into CSV
        /// </summary>
        /// <returns>Task which is exporting the data</returns>
        public void DatabaseToCSV() => Task.Run(async () =>
        {
            try
            {
                string exportPath = Path.Combine("..", "..", "..", "Export", this.portfolio.Name + "Export.csv");
                var orders = await portfolioDatabase.Orders.ToListAsync();
                using var file = File.Create(exportPath);
                using var csv = new StreamWriter(file);

                csv.WriteLine("Id;OrderType;Ticker;Amount;Date;Price;CurrentPrice");

                foreach (var order in orders)
                {
                    csv.WriteLine($"{order.Id};{order.OrderType};{order.Ticker};{order.Amount};{order.Date:dd-MM-yyyy};{order.Price};{order.CurrentPrice}");
                }
            }
            catch (Exception ex) 
            { 
                throw new Exception("Export failed! " + ex.Message);
            }
        });

        /// <summary>
        /// Imports all data (that is not already in the database) into the database
        /// </summary>
        /// <param name="fileName">File from which the orders will be imported</param>
        /// <returns>Task performing the import</returns>
        /// <exception cref="Exception">Import failed + reason of exception</exception>
        public void CSVToDatabase(string fileName) => Task.Run(async () =>
        {
            try
            {
                string importPath = Path.Combine("..", "..", "..", "Export", fileName);
                var csv = File.ReadLines(importPath);
                foreach (var line in csv.Skip(1))
                {
                    if (line is not  null)
                    {
                        var order = CsvLineToOrder(line);
                        if (!portfolioDatabase.Orders.Any(p => p.Equals(order))) await AddOrder(order);
                    }
                }
            }
            catch (Exception ex)  
            {
                throw new Exception("Import failed! " + ex.Message);
            }
        });

        /// <summary>
        /// Takes a csv string and converts it into order
        /// </summary>
        /// <param name="line">Line to be parsed</param>
        /// <returns>Order parsed from the line</returns>
        private static Order CsvLineToOrder(string line)
        {
            try
            {
                string[] parts = line.Split(';');
                Stock stock = new(parts[2], double.Parse(parts[5]));
                Order order = new(stock, int.Parse(parts[1]), double.Parse(parts[3]), DateTime.Parse(parts[4]));
                return order;
            }
            catch (Exception ex) 
            { 
                throw new Exception("Parsing failed! " + ex.Message); 
            }
        }

    }
}
