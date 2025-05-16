namespace PortfolioTracker.Database
{
    public static class ConnectionString
    {
        /// <summary>
        /// </summary>
        /// <param name="name">name of the database</param>
        public static string GetConnectionString(string name)
        {
            return @"server=(localdb)\MSSQLLocalDB;Initial Catalog = " + name + "; Integrated Security = true";
        }
    }
}
