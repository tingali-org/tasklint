using System.Threading.Tasks;

using TaskLint.Domain.Common;

namespace TaskLint.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
