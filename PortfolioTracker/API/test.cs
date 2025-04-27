using YahooFinanceApi;

namespace PortfolioTracker
{
    public static class Test
    {
        public static async Task<decimal> Price(string ticker)
        {
            var securities = await Yahoo.Symbols(ticker)
                .Fields(Field.RegularMarketPrice)
                .QueryAsync();
            return (decimal)(securities[ticker][Field.RegularMarketPrice]);
        }
    }
}

