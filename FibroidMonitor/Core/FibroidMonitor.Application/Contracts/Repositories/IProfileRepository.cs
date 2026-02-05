using FibroidMonitor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibroidMonitor.Application.Contracts.Repositories
{
    public interface IProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct);
        Task UpsertAsync(UserProfile profile, CancellationToken ct);
    }
}
