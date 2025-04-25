using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Models
{
    class Stock
    {
        public string ticker { get; private set; }
        public int price { get; private set; }
        public int currentPrice { get; private set; }
        public Stock(string ticker, int price)
        {
            this.ticker = ticker;
            this.price = price;
            this.currentPrice = price;
        }
        public void UpdatePrice(int currentPrice)
        {
            this.currentPrice = currentPrice;
        }
    }
}
