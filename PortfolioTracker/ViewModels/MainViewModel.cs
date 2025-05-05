using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Views;

namespace PortfolioTracker.ViewModels
{
    public class MainViewModel
    {
        public RelayCommand ChangeToOverviewCommand { get; set; }
        public RelayCommand ChangeToOrdersCommand { get; set; }
        public RelayCommand ChangeToAddPortfolioCommand { get; set; }
        public RelayCommand ChangeToAddOrderCommand { get; set; }
        public ContentControl CurrentView { get; set; } = new();
        public ObservableCollection<string> Portfolios { get; set; }
        private PortfolioManager _manager;
        private string _selectedPortfolioName;
        public string SelectedPortfolioName {
            get => _selectedPortfolioName;
            set {
                if (_selectedPortfolioName != value) {
                    _selectedPortfolioName = value;
                    ChangePortfolio(null);
                }
            }
        }
        public MainViewModel()
        {
            Portfolios = MainDatabaseManager.GetPortfolios();
            _selectedPortfolioName = Portfolios.First();
            _manager = new PortfolioManager(_selectedPortfolioName);
            CurrentView.Content = new Overview(_manager);

            ChangeToOverviewCommand = new RelayCommand(ChangeToOverview, _ => true);
            ChangeToOrdersCommand = new RelayCommand(ChangeToOrders, _ => true);
            ChangeToAddPortfolioCommand = new RelayCommand(ChangeToAddPortfolio, _ => true);
            ChangeToAddOrderCommand = new RelayCommand(ChangeToAddOrder, _ => true);
            DelayedChangeToOverview();
        }
        private async void DelayedChangeToOverview()
        {
            ChangeToOverviewCommand.Execute(null);
            await Task.Delay(1000);
            ChangeToOverviewCommand.Execute(null);
        }
        private void ChangeToOverview(object? obj) 
        {
            try
            {
                CurrentView.Content = new Overview(_manager);
            } 
            catch { }
         }
        private void ChangeToOrders(object? obj)
        {
            try
            {
                CurrentView.Content = new Orders(_manager);
            }
            catch { }
        }
        private void ChangeToAddPortfolio(object? obj)
        {
            try
            {
                CurrentView.Content = new AddPortfolio(_manager);
            }
            catch { }
        }
        private void ChangeToAddOrder(object? obj)
        {
            try
            {
                CurrentView.Content = new AddOrder(_manager);
            }
            catch {  }
        }
        private async void ChangePortfolio(object? obj)
        {
            _manager.Dispose();
            await Task.Delay(500);
            _manager = new PortfolioManager(_selectedPortfolioName);
            DelayedChangeToOverview();
        }
    }
}
