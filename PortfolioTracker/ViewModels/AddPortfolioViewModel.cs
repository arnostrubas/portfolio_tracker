using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;
using PortfolioTracker.Models;

namespace PortfolioTracker.ViewModels
{
    public class AddPortfolioViewModel
    {
        private readonly PortfolioManager _manager;
        public RelayCommand AddPortfolioCommand { get; set; }
        public RelayCommand DeletePortfolioCommand { get; set; }
        public ObservableCollection<string> Portfolios { get; set; }
        private string name = "";
        public string Name { get => name; set { name = value; AddPortfolioCommand.RaiseCanExecuteChanged(); 
                                                              DeletePortfolioCommand.RaiseCanExecuteChanged(); } }
        
        public AddPortfolioViewModel(PortfolioManager manager) { 
            Portfolios = MainDatabaseManager.GetPortfolios();
            AddPortfolioCommand = new RelayCommand(AddPortfolio, CanAddPortfolio);
            DeletePortfolioCommand = new RelayCommand(DeletePortfolio, CanDeletePortfolio);
            _manager = manager;
        }

        private bool CanAddPortfolio(object? obj) => Name != "" && Name is not null && !Name.Contains(' ') 
                                                        && !MainDatabaseManager.manager.PortfolioNames().Contains(Name);

        private void AddPortfolio(object? obj) => new PortfolioManager(Name);

        private bool CanDeletePortfolio(object? obj) => Name != "" && Name is not null && MainDatabaseManager.manager.PortfolioNames().Contains(Name);
        private void DeletePortfolio(object? obj)
        {
            var portfolio = MainDatabaseManager.manager.Portfolios.FirstOrDefault(p => p.Name == Name);
            if (portfolio != null)
            {
                var tempManager = new PortfolioManager(portfolio.Name);
                tempManager.PortfolioDatabase.Database.EnsureDeleted();
                MainDatabaseManager.RemovePortfolio(portfolio);
            }
        }
    }
}
