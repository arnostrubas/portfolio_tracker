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
