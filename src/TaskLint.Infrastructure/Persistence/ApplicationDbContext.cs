
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using TaskLint.Application.Common.Interfaces;
using TaskLint.Domain.Common;
using TaskLint.Domain.Entities;

using static TaskLint.Domain.Common.DomainEvent;

namespace TaskLint.Infrastructure.Persistence
{
    /// <summary>
    /// The Application DB Context
    /// To add a migration, run in terminal from solution root: `dotnet ef migrations add MIGRATION_NAME --project src\TaskLint.Infrastructure --startup-project src\TaskLint.Api --output-dir Persistence\Migrations`
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            if (options is null)
                throw new System.ArgumentNullException(nameof(options));
            _domainEventService = domainEventService ?? throw new System.ArgumentNullException(nameof(domainEventService));
            _dateTime = dateTime ?? throw new System.ArgumentNullException(nameof(dateTime));
        }

        public DbSet<TaskItem> TaskItems { get; set; }

        public DbSet<TaskList> TaskLists { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
