namespace PortfolioTracker.Models
{
    public class Stock
    {
        public string ticker { get; private set; }
        public float price { get; private set; }
        public float currentPrice { get; private set; }
        public Stock(string ticker, float price)
        {
            this.ticker = ticker;
            this.price = price;
            this.currentPrice = price;
        }
        public void UpdatePrice(float currentPrice)
        {
            this.currentPrice = currentPrice;
        }
    }
}
