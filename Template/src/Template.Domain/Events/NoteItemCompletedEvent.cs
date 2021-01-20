using Template.Domain.Common;
using Template.Domain.Entities;

namespace Template.Domain.Events
{
    public class NoteItemCompletedEvent : DomainEvent
    {
        public NoteItemCompletedEvent(NoteItem item)
        {
            Item = item;
        }

        public NoteItem Item { get; }
    }
}
