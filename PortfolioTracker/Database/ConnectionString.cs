namespace PortfolioTracker.Database
{
    public static class ConnectionString
    {
        public static string GetConnectionString(string name)
        {
            return @"server=(localdb)\MSSQLLocalDB;Initial Catalog = " + name + "; Integrated Security = true";
        }
    }
}
