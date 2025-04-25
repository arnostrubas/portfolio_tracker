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
    class OrderModel
    {
        public Stock stock;
        public int Id { get; set; }
        public OrderType orderType { get; set; }
        public int amout { get; set; }
        public DateTime date { get; set; }
        public int totalInvestment { get; private set; }
    }
}
