using System.Security.Claims;

namespace FibroidMonitor.Api.Auth
{
    public static class UserContext
    {
        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
                      ?? user.Claims.FirstOrDefault(c => c.Type.EndsWith("/nameidentifier"))?.Value;

            if (sub is null || !Guid.TryParse(sub, out var id))
                throw new InvalidOperationException("Invalid user context.");

            return id;
        }
    }
}
