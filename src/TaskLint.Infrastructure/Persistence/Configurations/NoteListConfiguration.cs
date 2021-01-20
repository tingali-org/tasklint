using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskLint.Domain.Entities;

namespace TaskLint.Infrastructure.Persistence.Configurations
{
    public class NoteListConfiguration : IEntityTypeConfiguration<NoteList>
    {
        public void Configure(EntityTypeBuilder<NoteList> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
