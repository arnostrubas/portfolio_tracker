using System.Windows;
using PortfolioTracker.ViewModels;

namespace PortfolioTracker.Views
{
    /// <summary>
    /// Interakční logika pro Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
