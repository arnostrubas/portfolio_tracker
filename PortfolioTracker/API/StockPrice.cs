using YahooFinanceApi;

namespace PortfolioTracker
{
    public static class StockPrice
    {
        /// <param name="ticker">Ticker of the company</param>
        /// <returns>Price in decimal</returns>
        /// <exception cref="Exception">If the ticker doesnt exist</exception>
        public static async Task<decimal> Price(string ticker)
        {
            try
            {
                var securities = await Yahoo.Symbols(ticker)
                    .Fields(Field.RegularMarketPrice)
                    .QueryAsync();
                return (decimal)(securities[ticker][Field.RegularMarketPrice]);
            }
            catch 
            {
                throw new Exception("Stock doesnt exist");
            }
        }
    }
}

