using System.Windows;
using System.Windows.Controls;
using PortfolioTracker.Database;
using PortfolioTracker.Models;

namespace PortfolioTracker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        A();
    }

    private async void A()
    {
        var Data = new PortfolioManager("Portfolio");
        string price = await Test.Price("GOOGL");
        float priceF = float.Parse(price);
        Stock stock = new Stock("GOOGL", priceF);
        Order order = new Order(stock, Enums.OrderType.Buy, 4, DateTime.Today);
        await Data.AddOrder(order);
        TextBlock.Text = "DONE";
    }
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        TextBlock.Text = await Test.Price("AAPL");
    }

    private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        TextBlock.Text = await Test.Price("GOOGL"); ;
    }

    private async void Repeat()
    {
        while (true)
        {
            await Task.Delay(2000);
            TextBlock.Text = await Test.Price("GOOGL");
        }
    }
}