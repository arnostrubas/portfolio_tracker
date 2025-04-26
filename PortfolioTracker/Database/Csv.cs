using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Database
{
    public class Csv
    {
        public async void DatabaseToCSV(Portfolio portfolio)
        {

        }

        public Task<Portfolio> CSVToDatabase() => Task.Run(() =>
        {

            return new Portfolio();
        });
    }
}
