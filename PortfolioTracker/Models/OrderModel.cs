using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using PortfolioTracker.Enums;

namespace PortfolioTracker.Models
{
    [Table("Portfolio")]
    public class Order
    {
        public string ticker {  get; set; }
        public float price { get; set; }
        public float currentPrice { get; set; }
        public int Id { get; set; }
        public OrderType orderType { get; set; }
        public float amount { get; set; }
        public DateTime date { get; set; }
        public Order() { }
        public Order(Stock stock, OrderType orderType, float amount, DateTime date)
        {
            this.ticker = stock.ticker;
            this.price = stock.price;
            this.currentPrice = stock.currentPrice;
            this.orderType = orderType;
            this.amount = amount;
            this.date = date;
        }
    }
}
