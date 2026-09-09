using System.Windows.Controls;
using PortfolioTracker.Database;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Views
{
    /// <summary>
    /// Interakční logika pro AddOrder.xaml
    /// </summary>
    public partial class AddOrder : UserControl
    {
        public AddOrder(PortfolioManager manager)
        {
            InitializeComponent();
            DataContext = new AddOrderViewModel(manager);
        }
    }
}
