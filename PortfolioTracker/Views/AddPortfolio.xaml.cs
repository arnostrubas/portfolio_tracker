using System.Windows.Controls;
using PortfolioTracker.Database;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Views
{
    /// <summary>
    /// Interakční logika pro AddPortfolio.xaml
    /// </summary>
    public partial class AddPortfolio : UserControl
    {
        public AddPortfolio(PortfolioManager manager)
        {
            InitializeComponent();
            DataContext = new AddPortfolioViewModel(manager);
        }
    }
}
