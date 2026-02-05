using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FibroidMonitor.Domain;

namespace FibroidMonitor.Application.Contracts.FMonitorInterface
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email, CancellationToken ct);
        Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<bool> CreateAsync(AppUser user, CancellationToken ct);
    }
}
