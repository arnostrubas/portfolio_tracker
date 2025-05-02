using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;

namespace PortfolioTracker.ViewModels
{
    public class OverviewViewModel
    {
        private PortfolioManager _manager;
        public string Profit { get; set; } = "0.000";
        public string Invested { get; set; } = "0.000";
        public string CurrentValue { get; set; } = "0.000";
        public Brush ProfitColor { get; set; } = Brushes.Black;
        public OverviewViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Update(null);
        }

        private void Update(object? obj)
        {
            Profit = _manager.portfolio.Profit.ToString();
            if (_manager.portfolio.Profit > 0) ProfitColor = Brushes.Green;
            else if (_manager.portfolio.Profit < 0) ProfitColor = Brushes.Red;

            Invested = _manager.portfolio.Invested.ToString();
            CurrentValue = _manager.portfolio.CurrentValue.ToString();
        }
    }
}
