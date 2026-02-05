namespace FibroidMonitor.Infrastructure.LoggingService
{
    public  class LoggerAdapter<T>:IAppLogger<T>
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILoggerFactory loggerfactory)
        {
            _logger = loggerfactory.CreateLogger<T>();
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogError(string messgae, params object[] args)
        {
            _logger.LogError(messgae, args);
        }
    }
}
