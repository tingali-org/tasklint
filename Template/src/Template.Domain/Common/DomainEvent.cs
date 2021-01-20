using System;
using System.Collections.Generic;

namespace Template.Domain.Common
{
    public abstract class DomainEvent
    {
        public interface IHasDomainEvent
        {
            public List<DomainEvent> DomainEvents { get; set; }
        }

        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
