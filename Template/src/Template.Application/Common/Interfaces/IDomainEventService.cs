using System.Threading.Tasks;

using Template.Domain.Common;

namespace Template.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
