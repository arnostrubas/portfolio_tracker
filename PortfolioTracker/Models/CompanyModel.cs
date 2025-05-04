using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Models
{
    public class Company(string ticker, decimal owned, decimal buyPrice, decimal currPrice)
    {
        public string Ticker { get; set; } = ticker;
        public decimal Owned { get; set; } = owned;
        public decimal BuyPrice { get; set; } = buyPrice;
        public decimal CurrentPrice { get; set; } = currPrice;
        public decimal Profit { get; set; } = Math.Round((currPrice - buyPrice) * owned, 2);

    }
}
