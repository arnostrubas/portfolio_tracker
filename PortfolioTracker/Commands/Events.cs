using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Commands
{
    public static class Events
    {
        public static event EventHandler? CompanyChanged;
        public static void OnCompanyChanged(EventArgs eventArgs)
        {
            CompanyChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
