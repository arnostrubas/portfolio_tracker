using System.Windows;
using System.Windows.Controls;
using PortfolioTracker.Database;
using PortfolioTracker.Enums;
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
        /*await Task.Delay(500);
        Data.UpdatePrices();
        Data.Dispose();*/
        decimal price = await Test.Price("GOOGL");
        Stock stock = new Stock("GOOGL", price);
        Order order = new Order(stock, (int)OrderType.Sell, (decimal)5.8, DateTime.Today);
        //await Data.AddOrder(order);
        await Task.Delay(10);
        Data.UpdatePrices();
        
        /*
        var order1 = await Data.GetOrder(10);
        if (order1 != null) await Data.RemoveOrder(order1);
        await Data.DatabaseToCSV();*/
    }
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        TextBlock.Text = (await Test.Price("AAPL")).ToString();
    }

    private async void Button_Click_1(object sender, RoutedEventArgs e)
    {
        TextBlock.Text = (await Test.Price("GOOGL")).ToString();
    }

    private async void Repeat()
    {
        while (true)
        {
            await Task.Delay(2000);
            TextBlock.Text = (await Test.Price("GOOGL")).ToString();
        }
    }
}