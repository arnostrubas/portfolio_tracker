using System.Windows.Controls;
using YahooFinanceApi;

namespace PortfolioTracker
{
    public static class Test
    {
        public static async Task<string> Price(string ticker)
        {
                var securities = await Yahoo.Symbols(ticker)
                    .Fields(Field.RegularMarketPrice)
                    .QueryAsync();
                    var price = securities[ticker][Field.RegularMarketPrice];
                    return $"{ticker} current price is: ${price}";
        }
    }
}

