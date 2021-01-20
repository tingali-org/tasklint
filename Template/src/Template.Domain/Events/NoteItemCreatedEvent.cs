using Template.Domain.Common;
using Template.Domain.Entities;

namespace Template.Domain.Events
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
