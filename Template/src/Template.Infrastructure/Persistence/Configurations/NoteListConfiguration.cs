using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations
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
