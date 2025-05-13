using System.Collections.ObjectModel;
using System.Windows.Media;
using OxyPlot;
using OxyPlot.Series;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
    public class OverviewViewModel
    {
        private readonly PortfolioManager _manager;
        private decimal _profit = 0;
        private decimal _invested = 0;
        private decimal _currentValue = 0;
        public string Profit { get; private set; } = "0.00";
        public string Invested { get; private set; } = "0.00";
        public string CurrentValue { get; private set; } = "0.00";
        public Brush ProfitColor { get; set; } = Brushes.Black;
        public RelayCommand UpdateCommand { get; set; }
        public ObservableCollection<Company> Companies { get; set; }
        public PlotModel Pie { get; set; }
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
            Companies = [.. _manager.Companies.OrderBy(c => c.Ticker)];
            _profit = _manager.Portfolio.Profit;
            _invested = _manager.Portfolio.Invested;
            _currentValue = _manager.Portfolio.CurrentValue;

            Invested = "$" + Math.Round(_invested, 2).ToString();
            CurrentValue = "$" + Math.Round(_currentValue, 2).ToString();
            Profit = "$" + Math.Round(_profit, 2).ToString();
            if (_manager.Portfolio.Profit > 0) ProfitColor = Brushes.Green;
            else if (_manager.Portfolio.Profit < 0) ProfitColor = Brushes.Red;

            UpdatePie();
        }
        public void OnUpdate(object sender, EventArgs e)
        {
            UpdateCommand.Execute(null);
        }
        private void UpdatePie()
        {
            Pie = new PlotModel { Title = "Total value of companies", TitleColor = OxyColor.FromRgb(0x1E, 0x3A, 0x8A) };

            var pieSeries = new PieSeries
            {
                StrokeThickness = 1,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 270,
                InsideLabelFormat = "{1}: ${0:0}",
                OutsideLabelFormat = null,
            };
            foreach (var company in Companies.OrderByDescending(c => c.CurrentPrice * c.Owned))
            {
                double totalInvestment = (double)((company.CurrentPrice * company.Owned));
                var text = company.Ticker;
                var slice = new PieSlice(text, totalInvestment);
                pieSeries.Slices.Add(slice);
            }

            Pie.Series.Add(pieSeries);
        }
    }
}
