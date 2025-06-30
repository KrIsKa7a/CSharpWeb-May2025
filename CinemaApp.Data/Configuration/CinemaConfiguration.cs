namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.EntityConstants.Cinema;

    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> entity)
        {
            entity
                .HasKey(c => c.Id);

            entity
                .Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength);

            entity
                .Property(c => c.Location)
                .IsRequired(true)
                .HasMaxLength(LocationMaxLength);

            entity
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            // Define composite index of columns Name and Location to ensure unique combinations only
            entity
                .HasIndex(c => new { c.Name, c.Location })
                .IsUnique(true);

            entity
                .HasQueryFilter(c => c.IsDeleted == false);
        }
    }
}
