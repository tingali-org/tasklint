using TaskLint.Domain.Common;
using TaskLint.Domain.Entities;

namespace TaskLint.Domain.Events
{
    public class NoteItemCompletedEvent : DomainEvent
    {
        public NoteItemCompletedEvent(TaskItem item)
        {
            Item = item;
        }

        public TaskItem Item { get; }
    }
}
