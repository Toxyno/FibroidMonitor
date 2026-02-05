namespace FibroidMonitor.Domain
{
    public sealed record AppUser(Guid Id, string Email, string PasswordHash, DateTime CreatedAtUtc);

}
