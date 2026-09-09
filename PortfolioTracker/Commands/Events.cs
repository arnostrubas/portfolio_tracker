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
