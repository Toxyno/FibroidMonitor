using FibroidMonitor.Api.Auth;
using FibroidMonitor.Application.Contracts.FMonitorInterface;
using FibroidMonitor.Domain;
using FibroidMonitor.Domain.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FibroidMonitor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/treatment")]
    public sealed class TreatmentController(ITreatmentEventRepository repo) : ControllerBase
    {
        public sealed record TreatmentEventDto(
            Guid? Id,
            TreatmentEventType Type,
            string Title,
            string? Details,
            DateOnly StartDate,
            DateOnly? EndDate
        );

        [HttpGet]
        public async Task<ActionResult<List<TreatmentEventDto>>> List(CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);
            var items = await repo.ListAsync(userId, ct);
            return items.Select(x => new TreatmentEventDto(x.Id, x.Type, x.Title, x.Details, x.StartDate, x.EndDate)).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TreatmentEventDto dto, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);

            var ev = new TreatmentEvent(
                Id: Guid.NewGuid(),
                UserId: userId,
                Type: dto.Type,
                Title: dto.Title.Trim(),
                Details: dto.Details,
                StartDate: dto.StartDate,
                EndDate: dto.EndDate,
                CreatedAtUtc: DateTime.UtcNow
            );

            await repo.CreateAsync(ev, ct);
            return Ok(new { id = ev.Id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, TreatmentEventDto dto, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);

            var existing = await repo.GetAsync(userId, id, ct);
            if (existing is null) return NotFound();

            var updated = existing with
            {
                Type = dto.Type,
                Title = dto.Title.Trim(),
                Details = dto.Details,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            await repo.UpdateAsync(updated, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);
            await repo.DeleteAsync(userId, id, ct);
            return NoContent();
        }
    }
}
