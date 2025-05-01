using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Database;

namespace PortfolioTracker.ViewModels
{
    public class OverviewViewModel
    {
        private PortfolioManager _manager;
        public string Profit { get; set; } = "0";
        public OverviewViewModel(PortfolioManager manager)
        {
            _manager = manager;
            Profit = _manager.portfolio.Profit.ToString();
        }
    }
}
