namespace PortfolioTracker.Models
{
    public class Stock
    {
        public string ticker { get; private set; }
        public double price { get; private set; }
        public double currentPrice { get; private set; }
        public Stock(string ticker, double price)
        {
            this.ticker = ticker;
            this.price = price;
            this.currentPrice = price;
        }
        public void UpdatePrice(double currentPrice)
        {
            this.currentPrice = currentPrice;
        }
    }
}
