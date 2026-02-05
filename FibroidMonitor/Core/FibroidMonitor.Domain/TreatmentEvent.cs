using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FibroidMonitor.Domain.Enumeration;

namespace FibroidMonitor.Domain
{
    public sealed record TreatmentEvent(
        Guid Id,
        Guid UserId,
        TreatmentEventType Type,
        string Title,
        string? Details,
        DateOnly StartDate,
        DateOnly? EndDate,
        DateTime CreatedAtUtc
    );
}
