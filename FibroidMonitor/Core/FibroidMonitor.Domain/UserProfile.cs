using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibroidMonitor.Domain
{
    public sealed record UserProfile(
        Guid Id,
        Guid UserId,
        string DisplayName,
        int? Age,
        string TreatmentType,
        DateOnly? TreatmentStartDate
    );
}
