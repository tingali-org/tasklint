using System.Collections.Generic;

using TaskLint.Domain.Common;

namespace TaskLint.Domain.Entities
{
    public class NoteList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<NoteItem> Items { get; private set; } = new List<NoteItem>();
    }
}
