using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
    public class OverviewViewModel
    {
        private PortfolioManager _manager;
        public string Profit { get; set; } = "0.00";
        public string Invested { get; set; } = "0.00";
        public string CurrentValue { get; set; } = "0.00";
        public Brush ProfitColor { get; set; } = Brushes.Black;
        public ObservableCollection<Company> Companies { get; set; }
        public OverviewViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Companies = new(manager.Companies); 
            Update(null);
        }

        private void Update(object? obj)
        {
            Profit = "$" + Math.Round(_manager.portfolio.Profit, 2).ToString();
            if (_manager.portfolio.Profit > 0) ProfitColor = Brushes.Green;
            else if (_manager.portfolio.Profit < 0) ProfitColor = Brushes.Red;

            Invested = "$" + Math.Round(_manager.portfolio.Invested, 2).ToString();
            CurrentValue = "$" + Math.Round(_manager.portfolio.CurrentValue, 2).ToString();
        }
    }
}
