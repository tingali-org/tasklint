using System.Collections.Generic;

using TaskLint.Domain.Common;

namespace TaskLint.Domain.Entities
{
    public class TaskList : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<TaskItem> Items { get; private set; } = new List<TaskItem>();
    }
}
