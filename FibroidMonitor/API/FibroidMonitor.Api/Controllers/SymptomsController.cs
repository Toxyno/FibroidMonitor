using FibroidMonitor.Api.Auth;
using FibroidMonitor.Application.Contracts.FMonitorInterface;
using FibroidMonitor.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FibroidMonitor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/symptoms")]
    public sealed class SymptomsController(ISymptomLogRepository logs) : ControllerBase
    {
        public sealed record SymptomLogDto(
            DateOnly LogDate,
            int BleedingIntensity,
            int PainScore,
            int EnergyLevel,
            int MoodScore,
            string? Notes
        );

        [HttpGet]
        public async Task<ActionResult<List<SymptomLogDto>>> GetRange([FromQuery] DateOnly from, [FromQuery] DateOnly to, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);
            var items = await logs.GetRangeAsync(userId, from, to, ct);
            return items.Select(x => new SymptomLogDto(x.LogDate, x.BleedingIntensity, x.PainScore, x.EnergyLevel, x.MoodScore, x.Notes)).ToList();
        }

        [HttpPut]
        public async Task<IActionResult> Upsert(SymptomLogDto dto, CancellationToken ct)
        {
            static void Validate0to10(int v, string n)
            {
                if (v < 0 || v > 10) throw new ArgumentOutOfRangeException(n, "Must be 0..10");
            }

            Validate0to10(dto.BleedingIntensity, nameof(dto.BleedingIntensity));
            Validate0to10(dto.PainScore, nameof(dto.PainScore));
            Validate0to10(dto.EnergyLevel, nameof(dto.EnergyLevel));
            Validate0to10(dto.MoodScore, nameof(dto.MoodScore));

            var userId = UserContext.GetUserId(User);

            var log = new SymptomLog(
                Id: Guid.NewGuid(),
                UserId: userId,
                LogDate: dto.LogDate,
                BleedingIntensity: dto.BleedingIntensity,
                PainScore: dto.PainScore,
                EnergyLevel: dto.EnergyLevel,
                MoodScore: dto.MoodScore,
                Notes: dto.Notes,
                CreatedAtUtc: DateTime.UtcNow
            );

            await logs.UpsertAsync(log, ct);
            return NoContent();
        }
    }

}
