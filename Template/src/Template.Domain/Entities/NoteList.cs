using System.Collections.Generic;

using Template.Domain.Common;

namespace Template.Domain.Entities
{
    public class NoteList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<NoteItem> Items { get; private set; } = new List<NoteItem>();
    }
}
