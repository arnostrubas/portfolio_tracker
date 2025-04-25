using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Database
{
    public static class GetConnectionString
    {
        public static string getConnectionString(string name)
        {
            return @"server=(localdb)\MSSQLLocalDB;Initial Catalog = " + name + "; Integrated Security = true";
        }
    }
}
