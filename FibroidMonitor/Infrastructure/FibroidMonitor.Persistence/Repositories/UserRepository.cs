namespace FibroidMonitor.Persistence.Repositories
{
    public sealed  class UserRepository() : IUserRepository
    {
        private readonly FibroidMonitorContext _fibroidMonitorContext;
        private readonly IAppLogger<UserRepository> _logger;
        public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation("Getting the AppUser Details for the email:{Email}",email);

                var sql = @"SELECT TOP 1 Id, Email, PasswordHash, CreatedAtUtc
                           FROM dbo.Users
                           WHERE Email = @Email";
                var parameter = new
                {
                    Email = email
                };
                var result = await _fibroidMonitorContext.conn.QueryAsync<AppUser>(sql, parameter);

                return result.FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError( "Error getting AppUser for email:{Email}", ex.Message);
                throw;
            }
        }

        public async Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation("Getting the AppUser Details for the email:{Id}", id);

                var sql = @"SELECT TOP 1 Id, Email, PasswordHash, CreatedAtUtc
                           FROM dbo.Users
                           WHERE Id = @id";
                var parameter = new
                {
                    Id = id
                };
                var result = await _fibroidMonitorContext.conn.QueryAsync<AppUser>(sql, parameter);

                return result.FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting AppUser for ID:{Id}", ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateAsync(AppUser user, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation("Starting the Insertion of AppUser for emailUser", user.Email);

                var sql = @"INSERT INTO dbo.Users (Id, Email, PasswordHash, CreatedAtUtc)
                      VALUES (@Id, @Email, @PasswordHash, @CreatedAtUtc)";

                var parameter = new
                {
                    Id = user.Id,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    CreatedAtUtc = user.CreatedAtUtc
                };
                var result = await _fibroidMonitorContext.conn.ExecuteAsync(sql, parameter);

                return result>0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting AppUser for ID:{Id}", ex.Message);
                throw;
            }
        }
    }
}
