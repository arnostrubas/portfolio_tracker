using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Database;

namespace PortfolioTracker.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<string> Portfolios { get; set; }
        public MainViewModel()
        {
            Portfolios = MainDatabaseManager.GetPortfolios();
        }
    }
}
