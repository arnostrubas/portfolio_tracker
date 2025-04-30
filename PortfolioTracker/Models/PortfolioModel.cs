using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace PortfolioTracker.Models
{
    [Table("PortfolioManager")]
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public int NumberOfOrders { get; set; } = 0;
        public Decimal Invested { get; set; } = 0;
        public Decimal CurrentValue { get; set; } = 0;
        public Decimal Profit { get; set; } = 0;
        public Portfolio() { }

        public Portfolio(string name) 
        {
            this.Name = name;
            this.ConnectionString = Database.ConnectionString.GetConnectionString(name);
        }
    }
}
