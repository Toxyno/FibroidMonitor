using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibroidMonitor.Domain
{
    public sealed record SymptomLog(
        Guid Id,
        Guid UserId,
        DateOnly LogDate,
        int BleedingIntensity,
        int PainScore,
        int EnergyLevel,
        int MoodScore,
        string? Notes,
        DateTime CreatedAtUtc
    );
}
