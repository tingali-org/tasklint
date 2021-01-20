using System.Collections.Generic;

using Template.Domain.Common;
using Template.Domain.Enums;
using Template.Domain.Events;

using static Template.Domain.Common.DomainEvent;

namespace Template.Domain.Entities
{
    public class NoteItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public NoteList List { get; set; }

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
