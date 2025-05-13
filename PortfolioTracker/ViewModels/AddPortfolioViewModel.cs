using System.Collections.ObjectModel;
using PortfolioTracker.Commands;
using PortfolioTracker.Database;

namespace PortfolioTracker.ViewModels
{
    public class AddPortfolioViewModel
    {
        private readonly PortfolioManager _manager;
        public RelayCommand AddPortfolioCommand { get; set; }
        public RelayCommand DeletePortfolioCommand { get; set; }
        public ObservableCollection<string> Portfolios { get; set; }
        private string name = "";
        public string Name
        {
            get => name; set
            {
                name = value; AddPortfolioCommand.RaiseCanExecuteChanged();
                DeletePortfolioCommand.RaiseCanExecuteChanged();
            }
        }
        public AddPortfolioViewModel(PortfolioManager manager)
        {
            Portfolios = MainDatabaseManager.GetPortfolios();
            AddPortfolioCommand = new RelayCommand(AddPortfolio, CanAddPortfolio);
            DeletePortfolioCommand = new RelayCommand(DeletePortfolio, CanDeletePortfolio);
            _manager = manager;
        }

        private bool CanAddPortfolio(object? obj) => Name != "" && Name is not null && !Name.Contains(' ')
                                                        && !MainDatabaseManager.manager.NamesOfPortfolios().Contains(Name);
        private bool CanDeletePortfolio(object? obj) => Name != "" && Name is not null && MainDatabaseManager.manager.NamesOfPortfolios().Contains(Name)
                                                        && _manager.Portfolio.Name != Name;

        private void AddPortfolio(object? obj) => new PortfolioManager(Name);
        private void DeletePortfolio(object? obj)
        {
            try
            {
                var portfolio = MainDatabaseManager.manager.Portfolios.FirstOrDefault(p => p.Name == Name);
                if (portfolio != null)
                {
                    var tempManager = new PortfolioManager(portfolio.Name);
                    MainDatabaseManager.RemovePortfolio(portfolio);
                    tempManager.PortfolioDatabase.Database.EnsureDeleted();
                    tempManager.Dispose();
                }
            }
            catch { }
        }
    }
}
