using FibroidMonitor.Application.Reports;
using FibroidMonitor.Application.Reports.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FibroidMonitor.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/reports")]
    public sealed class ReportsController(
        ISymptomLogRepository symptomLogs,
        ITreatmentEventRepository treatmentEvents,
        IReportPdfService pdfService,
        IProfileRepository profiles
    ) : ControllerBase
    {
        [HttpGet("pdf")]
        public async Task<IActionResult> GetPdf([FromQuery] DateOnly from, [FromQuery] DateOnly to, CancellationToken ct)
        {
            var userId = UserContext.GetUserId(User);

            var profile = await profiles.GetByUserIdAsync(userId, ct);
            var patientName = profile?.DisplayName?.Trim();

            if (string.IsNullOrWhiteSpace(patientName))
                patientName = "Patient";

            var logs = await symptomLogs.GetRangeAsync(userId, from, to, ct);
            var events = await treatmentEvents.ListRangeAsync(userId, from, to, ct);

            var report = new ClinicReportModel(
                UserId: userId,
                PatientName: patientName,
                RangeFrom: from,
                RangeTo: to,
                SymptomLogs: logs.Select(x => new ClinicReportModel.SymptomRow(
                    Date: x.LogDate,
                    Bleeding: x.BleedingIntensity,
                    Pain: x.PainScore,
                    Energy: x.EnergyLevel,
                    Mood: x.MoodScore,
                    Notes: x.Notes
                )).ToList(),
                TreatmentEvents: events.Select(x => new ClinicReportModel.TreatmentRow(
                    Type: x.Type.ToString(),
                    Title: x.Title,
                    StartDate: x.StartDate,
                    EndDate: x.EndDate,
                    Details: x.Details
                )).ToList()
            );

            var bytes = pdfService.BuildClinicSummaryPdf(report);

            var fileName = $"fibroid-report-{from:yyyy-MM-dd}-to-{to:yyyy-MM-dd}.pdf";
            return File(bytes, "application/pdf", fileName);
        }
    }
}
