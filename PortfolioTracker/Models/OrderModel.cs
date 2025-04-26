using System.ComponentModel.DataAnnotations.Schema;
using PortfolioTracker.Enums;

namespace PortfolioTracker.Models
{
    [Table("Portfolio")]
    public class Order
    {
        public int Id { get; set; }
        public int OrderType { get; set; }
        public string Ticker { get; set; } = "";
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double CurrentPrice { get; set; }
        public Order() { }
        public Order(Stock stock, int orderType, double amount, DateTime date)
        {
            this.Ticker = stock.ticker;
            this.Price = stock.price;
            this.CurrentPrice = stock.currentPrice;
            this.OrderType = orderType;
            this.Amount = amount;
            this.Date = date;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Order order) return false;
            return this.Ticker == order.Ticker && this.OrderType == order.OrderType && this.Amount == order.Amount && 
                this.Date == order.Date;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Ticker, OrderType, Amount, Date);
        }
    }
}
