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
                return $"{securities[ticker][Field.RegularMarketPrice]}";
        }
    }
}

