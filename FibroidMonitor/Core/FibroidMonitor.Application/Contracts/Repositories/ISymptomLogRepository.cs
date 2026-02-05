using FibroidMonitor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibroidMonitor.Application.Contracts.FMonitorInterface
{
    public interface ISymptomLogRepository
    {
        Task UpsertAsync(SymptomLog log, CancellationToken ct);
        Task<IReadOnlyList<SymptomLog>> GetRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken ct);
    }
}
