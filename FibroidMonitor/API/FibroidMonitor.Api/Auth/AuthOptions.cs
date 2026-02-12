namespace FibroidMonitor.Api.Auth
{
    public sealed class AuthOptions
    {
        public string Issuer { get; set; } = "FibroidMonitor";
        public string Audience { get; set; } = "FibroidMonitorClients";
        public string JwtSigningKey { get; set; } = "REPLACE_WITH_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS";
    }
}
