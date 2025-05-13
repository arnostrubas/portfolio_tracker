using System.Windows.Controls;
using PortfolioTracker.Database;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Views
{
    /// <summary>
    /// Interakční logika pro Overview.xaml
    /// </summary>
    public partial class Overview : UserControl
    {
        public Overview(PortfolioManager manager)
        {
            InitializeComponent();
            DataContext = new OverviewViewModel(manager);
        }
    }
}
