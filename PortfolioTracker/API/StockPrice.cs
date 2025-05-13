using YahooFinanceApi;

namespace PortfolioTracker.API
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
                    .Fields(Field.RegularMarketPrice, Field.Currency)
                    .QueryAsync();
                double conversionRate = 1;
                var currency = securities[ticker][Field.Currency];
                if (currency != "USD")
                {
                    var rate = await Yahoo.Symbols(currency + "USD=X")
                        .Fields(Field.RegularMarketPrice)
                        .QueryAsync();
                    conversionRate = rate[currency + "USD=X"][Field.RegularMarketPrice];
                }
                return Math.Round((decimal)securities[ticker][Field.RegularMarketPrice] * (decimal)conversionRate, 2);

            }
            catch
            {
                throw new Exception("Stock doesnt exist");
            }
        }
    }
}

