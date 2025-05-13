using System.IO;
using Microsoft.EntityFrameworkCore;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public static class Csv
    {
        /// <summary>
        /// Exports portfolio into CSV
        /// </summary>
        /// <returns>Task which is exporting the data</returns>
        public static void DatabaseToCSV(Portfolio portfolio, PortfolioManagerDbContext portfolioDatabase) => Task.Run(async () =>
        {
            try
            {
                string exportPath = Path.Combine("..", "..", "..", "Export", portfolio.Name + "Export.csv");
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
        public static void CSVToDatabase(string filePath, PortfolioManager manager) => Task.Run(async () =>
        {
            try
            {
                var csv = File.ReadLines(filePath);
                foreach (var line in csv.Skip(1))
                {
                    if (line is not null)
                    {
                        var order = CsvLineToOrder(line);
                        await manager.AddOrder(order);
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
                Stock stock = new(parts[2], decimal.Parse(parts[5]));
                Order order = new(stock, int.Parse(parts[1]), decimal.Parse(parts[3]), DateOnly.Parse(parts[4]));
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing failed! " + ex.Message);
            }
        }

    }
}
