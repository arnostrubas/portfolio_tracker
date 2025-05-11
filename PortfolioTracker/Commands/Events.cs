using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTracker.Commands
{
    public class Events
    {
        public event EventHandler? Update;
        public void OnUpdate()
        {
            Update?.Invoke(null, EventArgs.Empty);
        }
    }
}
