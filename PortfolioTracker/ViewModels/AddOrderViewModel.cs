using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
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
        public OrderType OrderType { get => _orderType; set { _orderType = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        public ObservableCollection<OrderType> OrderTypes { get; set; } = new((OrderType[])Enum.GetValues(typeof(OrderType)));

        private string _ticker = "";
        public string Ticker { get => _ticker; set 
            { 
                _ticker = value; 
                AddOrderCommand.RaiseCanExecuteChanged();
            } 
        }
        private string _amount = "0";
        public string Amount { get => _amount; set { _amount = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        private decimal decimalAmount;

        private DateOnly _date = DateOnly.FromDateTime(DateTime.Today);
        public DateTime Date { get => DateTime.Parse(_date.ToString()); set { _date = DateOnly.FromDateTime(value); AddOrderCommand.RaiseCanExecuteChanged(); } }
        
        private string _price = "0";
        public string Price { get => _price; set { _price = value; AddOrderCommand.RaiseCanExecuteChanged(); } }
        private decimal decimalPrice;

        public AddOrderViewModel(PortfolioManager manager)
        { 
            _manager = manager;
            AddOrderCommand = new RelayCommand(AddOrder, CanAddOrder);
            ExportCommand = new RelayCommand(_ => Csv.DatabaseToCSV(_manager.portfolio, _manager.portfolioDatabase), _ => true);
            ImportCommand = new RelayCommand(Import, _ => true);
        }

        private void Import(object? obj)
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

        private bool CanAddOrder(object? obj)
        { 
            return Decimal.TryParse(_price, out decimalPrice) && decimalPrice != 0 &&
                   Decimal.TryParse(_amount, out decimalAmount) && decimalAmount > 0;
        }
        private void AddOrder(object? obj)
        {
            try
            {
                Stock stock = new(_ticker, decimalPrice);
                Order order = new(stock, (int)_orderType, decimalAmount, _date);
                _manager.AddOrder(order);
            } catch { }
        }
    }
}
