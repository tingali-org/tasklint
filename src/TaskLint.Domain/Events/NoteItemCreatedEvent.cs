using TaskLint.Domain.Common;
using TaskLint.Domain.Entities;

namespace TaskLint.Domain.Events
{
    public class NoteItemCreatedEvent : DomainEvent
    {
        public NoteItemCreatedEvent(NoteItem item)
        {
            Item = item;
        }

        public NoteItem Item { get; }
    }
}
