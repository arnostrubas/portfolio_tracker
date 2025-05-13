using System.Collections.ObjectModel;
using Microsoft.Win32;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Enums;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
    class AddOrderViewModel
    {
        private readonly PortfolioManager _manager;
        public RelayCommand AddOrderCommand { get; set; }
        public RelayCommand ImportCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }

        private OrderType _orderType;
        private string _ticker = "";
        private string _amount = "0";
        private string _price = "0";
        private DateOnly _date = DateOnly.FromDateTime(DateTime.Today);
        private decimal decimalPrice;
        private decimal decimalAmount;

        public OrderType OrderType { get => _orderType; set { _orderType = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        public string Ticker { get => _ticker; set { _ticker = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        public string Amount { get => _amount; set { _amount = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        public DateTime Date { get => DateTime.Parse(_date.ToString()); set { _date = DateOnly.FromDateTime(value); AddOrderCommand.RaiseCanExecuteChanged(); } }
        public string Price { get => _price; set { _price = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        public ObservableCollection<OrderType> OrderTypes { get; set; } = [.. (OrderType[])Enum.GetValues(typeof(OrderType))];

        public AddOrderViewModel(PortfolioManager manager)
        {
            _manager = manager;
            AddOrderCommand = new RelayCommand(AddOrder, CanAddOrder);
            ExportCommand = new RelayCommand(_ => Csv.DatabaseToCSV(_manager.Portfolio, _manager.PortfolioDatabase), _ => true);
            ImportCommand = new RelayCommand(Import, _ => true);
        }

        private void Import(object? obj)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {
                    string path = openFileDialog.FileName;
                    Csv.CSVToDatabase(path, _manager);
                }
            }
            catch { }
        }

        private bool CanAddOrder(object? obj)
        {
            return Decimal.TryParse(_price, out decimalPrice) && decimalPrice != 0 &&
                   Decimal.TryParse(_amount, out decimalAmount) && decimalAmount > 0;
        }
        private async void AddOrder(object? obj)
        {
            try
            {
                Stock stock = new(_ticker, decimalPrice);
                Order order = new(stock, (int)_orderType, decimalAmount, _date);
                await _manager.AddOrder(order);
            }
            catch { }
        }
    }
}
