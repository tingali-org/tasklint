using System.Collections.Generic;

using TaskLint.Domain.Common;
using TaskLint.Domain.Enums;
using TaskLint.Domain.Events;

using static TaskLint.Domain.Common.DomainEvent;

namespace TaskLint.Domain.Entities
{
    public class TaskItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public TaskList List { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public BackgroundColor BackgroundColor { get; set; }

        private bool _done;
        public bool Done {
            get => _done;
            set {
                if (value && !_done)
                {
                    DomainEvents.Add(new NoteItemCompletedEvent(this));
                }

                _done = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
