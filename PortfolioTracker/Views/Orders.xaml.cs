using System.Windows.Controls;
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
