using TaskLint.Domain.Common;
using TaskLint.Domain.Entities;

namespace TaskLint.Domain.Events
{
    public class NoteItemCreatedEvent : DomainEvent
    {
        public NoteItemCreatedEvent(TaskItem item)
        {
            Item = item;
        }

        public TaskItem Item { get; }
    }
}
