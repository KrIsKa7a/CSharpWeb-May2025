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

            entity
                .HasOne(c => c.Manager)
                .WithMany(m => m.ManagedCinemas)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Define composite index of columns Name and Location to ensure unique combinations only
            entity
                .HasIndex(c => new { c.Name, c.Location })
                .IsUnique(true);

            entity
                .HasQueryFilter(c => c.IsDeleted == false);

            entity
                .HasData(this.SeedCinemas());
        }

        private IEnumerable<Cinema> SeedCinemas()
        {
            List<Cinema> cinemas = new List<Cinema>()
            {
                new Cinema
                {
                    Id = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"),
                    Name = "Arena Mall Sofia",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Cinema
                {
                    Id = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"),
                    Name = "Cinema City Plovdiv",
                    Location = "Plovdiv, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Cinema
                {
                    Id = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"),
                    Name = "Eccoplexx Varna",
                    Location = "Varna, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Cinema
                {
                    Id = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"),
                    Name = "IMAX Mall of Sofia",
                    Location = "Sofia, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                },
                new Cinema
                {
                    Id = Guid.Parse("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"),
                    Name = "Cinema City Burgas Plaza",
                    Location = "Burgas, Bulgaria",
                    ManagerId = null,
                    IsDeleted = false,
                }
            };

            return cinemas;
        }
    }
}
