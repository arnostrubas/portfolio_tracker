using System.ComponentModel.DataAnnotations.Schema;
using PortfolioTracker.Enums;

namespace PortfolioTracker.Models
{
    [Table("Portfolio")]
    public class Order
    {
        public int Id { get; set; }
        //OrderType is stored as int, had some trouble with storing Enum in database
        public int OrderType { get; set; }
        public string Ticker { get; set; } = "";
        public Decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public Decimal Price { get; set; }
        public Decimal CurrentPrice { get; set; }
        public Order() { }
        public Order(Stock stock, int orderType, Decimal amount, DateOnly date)
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
