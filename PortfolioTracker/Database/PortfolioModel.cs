using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace PortfolioTracker.Database
{
    [Table("PortfolioManager")]
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ConnectionString { get; set; } = "";
        public int NumberOfCompanies { get; set; } = 0;
        public float Invested { get; set; } = 0;
        public float CurrentValue { get; set; } = 0;
        public float Profit { get; set; } = 0;
        public Portfolio() { }

        public Portfolio(string name) 
        {
            this.Name = name;
            this.ConnectionString = GetConnectionString.getConnectionString(name);
        }

    }
}
