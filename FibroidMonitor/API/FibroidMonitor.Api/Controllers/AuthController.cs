using FibroidMonitor.Api.Auth;
using FibroidMonitor.Application.Contracts.Repositories;
using FibroidMonitor.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FibroidMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController(IUserRepository users, IJwtTokenService jwt) : ControllerBase
    {
        public sealed record RegisterRequest(string Email, string Password);
        public sealed record LoginRequest(string Email, string Password);

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req, CancellationToken ct)
        {
            var email = req.Email.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(req.Password) || req.Password.Length < 6)
                return BadRequest(new { error = "Email and password (min 6 chars) are required." });

            var existing = await users.GetByEmailAsync(email, ct);
            if (existing != null) return Conflict(new { error = "Email already registered." });

            var user = new AppUser(Guid.NewGuid(), email, BCrypt.Net.BCrypt.HashPassword(req.Password), DateTime.UtcNow);
            await users.CreateAsync(user, ct);

            var token = jwt.CreateToken(user.Id, user.Email);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req, CancellationToken ct)
        {
            var email = req.Email.Trim().ToLowerInvariant();
            var user = await users.GetByEmailAsync(email, ct);
            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized(new { error = "Invalid credentials." });

            var token = jwt.CreateToken(user.Id, user.Email);
            return Ok(new { token });
        }
    }
}
