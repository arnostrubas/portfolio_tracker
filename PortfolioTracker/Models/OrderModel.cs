using System.ComponentModel.DataAnnotations.Schema;
using PortfolioTracker.Enums;

namespace PortfolioTracker.Models
{
    [Table("Portfolio")]
    public class Order
    {
        public int Id { get; set; }
        public OrderType OrderType { get; set; }
        public string Ticker { get; set; } = "";
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public float CurrentPrice { get; set; }
        public Order() { }
        public Order(Stock stock, OrderType orderType, float amount, DateTime date)
        {
            this.Ticker = stock.ticker;
            this.Price = stock.price;
            this.CurrentPrice = stock.currentPrice;
            this.OrderType = orderType;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
