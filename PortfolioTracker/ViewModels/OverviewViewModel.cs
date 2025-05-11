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
        private readonly PortfolioManager _manager;
        public string Profit { get; set; } = "0.00";
        public string Invested { get; set; } = "0.00";
        public string CurrentValue { get; set; } = "0.00";
        public Brush ProfitColor { get; set; } = Brushes.Black;
        public RelayCommand UpdateCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public OverviewViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Companies = new(_manager.Companies);
            UpdateCommand = new RelayCommand(Update, _ => true);
            UpdateCommand.Execute(null);
        }
        private void Update(object? obj)
        {
            Profit = "$" + Math.Round(_manager.Portfolio.Profit, 2).ToString();
            if (_manager.Portfolio.Profit > 0) ProfitColor = Brushes.Green;
            else if (_manager.Portfolio.Profit < 0) ProfitColor = Brushes.Red;

            Invested = "$" + Math.Round(_manager.Portfolio.Invested, 2).ToString();
            CurrentValue = "$" + Math.Round(_manager.Portfolio.CurrentValue, 2).ToString();
        }
    }
}
