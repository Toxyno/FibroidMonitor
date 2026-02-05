namespace FibroidMonitor.Persistence.DatabaseContext
{
    public  class FibroidMonitorContext : IDisposable
    {
        private readonly IDbConnection _conn;

        public FibroidMonitorContext(string conn)
        {
            _conn = new SqlConnection(conn);
            _conn.Open();
        }

        public IDbConnection conn => _conn;
        

        public void Dispose()
        {
            if (_conn.State != ConnectionState.Closed)
            {
                _conn.Close();
            }
            _conn.Dispose();
        }
    }
}
