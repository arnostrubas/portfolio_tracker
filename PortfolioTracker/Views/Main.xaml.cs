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
using System.Windows.Shapes;
using PortfolioTracker.Database;
using PortfolioTracker.Models;
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
            ContentView.Content = new Overview();
            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new Orders();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new AddOrder();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new Overview();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ContentView.Content = new AddPortfolio();
        }
    }
}
