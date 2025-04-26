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
        double priceF = double.Parse(price);
        Stock stock = new Stock("GOOGL", priceF);
        Order order = new Order(stock, 1, (float)4.0, DateTime.Today);
        //await Data.AddOrder(order);
        var text = await Data.GetOrder(1);
        string r = "F";
        if (text != null) r = text.Amount.ToString();
        TextBlock.Text = r;
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