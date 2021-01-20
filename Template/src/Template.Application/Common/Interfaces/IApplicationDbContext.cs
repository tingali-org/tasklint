using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using Template.Domain.Entities;

namespace Template.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<NoteList> NoteLists { get; set; }

        DbSet<NoteItem> NoteItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
