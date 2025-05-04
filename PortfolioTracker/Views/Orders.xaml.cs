using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PortfolioTracker.Database;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Views
{
    /// <summary>
    /// Interakční logika pro Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        public Orders(PortfolioManager manager)
        {
            InitializeComponent();
            DataContext = new OrdersViewModel(manager);
        }
    }
}
