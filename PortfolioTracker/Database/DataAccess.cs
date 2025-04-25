using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PortfolioTracker;
using PortfolioTracker.Models;

namespace PortfolioTracker.Database
{
    public class DataAccess
    {
        public async Task AddOrder(Order order)
        {
            using var db = new portfolioDbContext();
            db.Orders.Add(order);
            await db.SaveChangesAsync();
        }
    }
}
