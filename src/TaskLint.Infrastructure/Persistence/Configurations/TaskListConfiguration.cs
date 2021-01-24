using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskLint.Domain.Entities;

namespace TaskLint.Infrastructure.Persistence.Configurations
{
    public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
