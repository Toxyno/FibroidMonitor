using FibroidMonitor.Api.Auth;
using FibroidMonitor.Application.Contracts.Repositories;
using FibroidMonitor.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FibroidMonitor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/profile")]
    public sealed class ProfileController(IProfileRepository profiles) : ControllerBase
    {
        public sealed record ProfileDto(string DisplayName, int? Age, string TreatmentType, DateOnly? TreatmentStartDate);

        [HttpGet]
        public async Task<ActionResult<ProfileDto>> Get(CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);
            var p = await profiles.GetByUserIdAsync(userId, ct);
            if (p is null) return NotFound();
            return new ProfileDto(p.DisplayName, p.Age, p.TreatmentType, p.TreatmentStartDate);
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(ProfileDto dto, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);

            var profile = new UserProfile(
                Id: Guid.NewGuid(),
                UserId: userId,
                DisplayName: dto.DisplayName?.Trim() ?? "",
                Age: dto.Age,
                TreatmentType: dto.TreatmentType?.Trim() ?? "",
                TreatmentStartDate: dto.TreatmentStartDate
            );

            await profiles.UpsertAsync(profile, ct);
            return NoContent();
        }
    }

}
