using FibroidMonitor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibroidMonitor.Application.Contracts.FMonitorInterface
{
    public interface ITreatmentEventRepository
    {
        Task<IReadOnlyList<TreatmentEvent>> ListAsync(Guid userId, CancellationToken ct);
        Task<TreatmentEvent?> GetAsync(Guid userId, Guid id, CancellationToken ct);
        Task CreateAsync(TreatmentEvent ev, CancellationToken ct);
        Task UpdateAsync(TreatmentEvent ev, CancellationToken ct);
        Task DeleteAsync(Guid userId, Guid id, CancellationToken ct);
    }
}
