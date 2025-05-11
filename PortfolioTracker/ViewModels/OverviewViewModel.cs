using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Update.Internal;
using OxyPlot;
using OxyPlot.Series;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Models;
using PortfolioTracker.Views;

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
        public PlotModel Pie {  get; set; }
        public OverviewViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Companies = [.. _manager.Companies];
            UpdateCommand = new RelayCommand(Update, _ => true);
            UpdateCommand.Execute(null);
            _manager.UpdateHandler.Update += OnUpdate;
            UpdatePie();
        }
        private void Update(object? obj)
        {
            Companies = [.. _manager.Companies];
            Profit = "$" + Math.Round(_manager.Portfolio.Profit, 2).ToString();
            if (_manager.Portfolio.Profit > 0) ProfitColor = Brushes.Green;
            else if (_manager.Portfolio.Profit < 0) ProfitColor = Brushes.Red;

            Invested = "$" + Math.Round(_manager.Portfolio.Invested, 2).ToString();
            CurrentValue = "$" + Math.Round(_manager.Portfolio.CurrentValue, 2).ToString();
            UpdatePie();
        }
        public void OnUpdate(object sender, EventArgs e)
        {
            UpdateCommand.Execute(null);
        }
        private void UpdatePie()
        {
            Pie = new PlotModel { Title = "Market Share" };

            var pieSeries = new PieSeries
            {
                StrokeThickness = 1,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 270,
                InsideLabelFormat = "{1}: ${0:0}",
                OutsideLabelFormat = null
            };
            foreach (var company in Companies.OrderByDescending(c => c.CurrentPrice * c.Owned))
            {
                pieSeries.Slices.Add(new PieSlice(company.Ticker, (double)((company.CurrentPrice * company.Owned))));
            }

            Pie.Series.Add(pieSeries);
        }
    }
}
