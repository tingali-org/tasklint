using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using TaskLint.Domain.Entities;

namespace TaskLint.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaskList> TaskLists { get; set; }

        DbSet<TaskItem> TaskItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
