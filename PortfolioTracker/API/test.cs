using YahooFinanceApi;

namespace PortfolioTracker
{
    public static class Test
    {
        public static async Task<double> Price(string ticker)
        {
            var securities = await Yahoo.Symbols(ticker)
                .Fields(Field.RegularMarketPrice)
                .QueryAsync();
            return securities[ticker][Field.RegularMarketPrice];
        }
    }
}

