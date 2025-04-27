namespace PortfolioTracker.Models
{
    public class Stock
    {
        public string ticker { get; private set; }
        public Decimal price { get; private set; }
        public Decimal currentPrice { get; private set; }
        public Stock(string ticker, Decimal price)
        {
            this.ticker = ticker;
            this.price = price;
            this.currentPrice = price;
        }
        public void UpdatePrice(Decimal currentPrice)
        {
            this.currentPrice = currentPrice;
        }
    }
}
