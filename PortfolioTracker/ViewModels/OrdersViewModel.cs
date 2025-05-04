using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Database;

namespace PortfolioTracker.ViewModels
{
    class OrdersViewModel
    {
        private readonly PortfolioManager _manager;
        public OrdersViewModel(PortfolioManager manager) 
        { 
            _manager = manager;
        }
    }
}
