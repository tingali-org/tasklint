using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations
{
    public class NoteItemConfiguration : IEntityTypeConfiguration<NoteItem>
    {
        public void Configure(EntityTypeBuilder<NoteItem> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
