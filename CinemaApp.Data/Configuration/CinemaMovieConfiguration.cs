namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.EntityConstants.CinemaMovie;

    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> entity)
        {
            entity
                .HasKey(cm => cm.Id);

            // Define composite pseudo-PK
            entity
                .HasIndex(cm => new { cm.MovieId, cm.CinemaId, cm.Showtime })
                .IsUnique(true);

            entity
                .Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            entity
                .Property(cm => cm.AvailableTickets)
                .HasDefaultValue(AvailableTicketsDefaultValue);

            entity
                .Property(cm => cm.Showtime)
                .IsRequired(true)
                .HasMaxLength(ShowtimeMaxLength);

            entity
                .HasQueryFilter(cm => cm.IsDeleted == false &&
                                                cm.Movie.IsDeleted == false &&
                                                cm.Cinema.IsDeleted == false);

            entity
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.MovieProjections)
                .HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cm => cm.Cinema)
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasData(this.SeedProjections());
        }

        private IEnumerable<CinemaMovie> SeedProjections()
        {
            List<CinemaMovie> projections = new List<CinemaMovie>()
            {
                new CinemaMovie
                {
                    Id = Guid.Parse("71a411ec-d23c-4abb-b50c-75571d0a3cff"),
                    CinemaId = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), // Arena Mall Sofia
                    MovieId = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),  // Harry Potter
                    AvailableTickets = 120,
                    IsDeleted = false,
                    Showtime = "18:30"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("30c505d0-9833-4087-9377-43ac8ab34e07"),
                    CinemaId = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), // Cinema City Plovdiv
                    MovieId = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),  // Inception
                    AvailableTickets = 90,
                    IsDeleted = false,
                    Showtime = "21:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("130f6630-5593-4165-8e9e-de718ee1fb72"),
                    CinemaId = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"), // Eccoplexx Varna
                    MovieId = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),  // Interstellar
                    AvailableTickets = 70,
                    IsDeleted = false,
                    Showtime = "20:15"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("c96549ed-7a19-4e83-856e-976cf306d611"),
                    CinemaId = Guid.Parse("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), // Cinema City Burgas Plaza
                    MovieId = Guid.Parse("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"),  // Gladiator
                    AvailableTickets = 60,
                    IsDeleted = false,
                    Showtime = "19:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("30864830-db09-412a-a816-6dbaccc1374c"),
                    CinemaId = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), // IMAX Mall of Sofia
                    MovieId = Guid.Parse("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),  // Avatar
                    AvailableTickets = 150,
                    IsDeleted = false,
                    Showtime = "17:45"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("a22c43b7-bd1d-46cd-b419-dba244e533cc"),
                    CinemaId = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), // Cinema City Plovdiv
                    MovieId = Guid.Parse("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),  // Shawshank
                    AvailableTickets = 85,
                    IsDeleted = false,
                    Showtime = "20:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("93f61e0c-6e62-41bb-b4e2-fc770ac48128"),
                    CinemaId = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"), // Eccoplexx Varna
                    MovieId = Guid.Parse("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),  // Pulp Fiction
                    AvailableTickets = 40,
                    IsDeleted = false,
                    Showtime = "22:30"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("6a7071a9-0c3d-42e5-9514-639c5fb259a3"),
                    CinemaId = Guid.Parse("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), // Cinema City Burgas Plaza
                    MovieId = Guid.Parse("844d9abd-104d-41ab-b14a-ce059779ad91"),  // The Matrix
                    AvailableTickets = 100,
                    IsDeleted = false,
                    Showtime = "16:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("3291c19e-5995-48af-9124-35f855bf8476"),
                    CinemaId = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), // Arena Mall Sofia
                    MovieId = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),  // The Dark Knight
                    AvailableTickets = 95,
                    IsDeleted = false,
                    Showtime = "19:45"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("d00af316-049a-4bd5-97c2-c55fcab99783"),
                    CinemaId = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), // IMAX Mall of Sofia
                    MovieId = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),  // Lord of the Rings
                    AvailableTickets = 80,
                    IsDeleted = false,
                    Showtime = "20:30"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("0241c54a-37e7-4c9a-bcfb-43f0a60749e2"),
                    CinemaId = Guid.Parse("5ae6c761-1363-4a23-9965-171c70f935de"), // Eccoplexx Varna
                    MovieId = Guid.Parse("54082f99-023b-4d68-89ac-44c00a0958d0"),  // Forrest Gump
                    AvailableTickets = 60,
                    IsDeleted = false,
                    Showtime = "17:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("0e5c76b7-9e27-4217-a113-5cafd558d00f"),
                    CinemaId = Guid.Parse("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), // Cinema City Plovdiv
                    MovieId = Guid.Parse("ab2c3213-48a7-41ea-9393-45c60ef813e6"),  // Titanic
                    AvailableTickets = 70,
                    IsDeleted = false,
                    Showtime = "18:15"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("ef18d96e-2e5b-4218-b552-e094e98ac178"),
                    CinemaId = Guid.Parse("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), // Arena Mall Sofia
                    MovieId = Guid.Parse("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),  // Pulp Fiction
                    AvailableTickets = 50,
                    IsDeleted = false,
                    Showtime = "22:00"
                },
                new CinemaMovie
                {
                    Id = Guid.Parse("9d54f01b-b33d-4ed5-8c4c-6874d62b24dd"),
                    CinemaId = Guid.Parse("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), // IMAX Mall of Sofia
                    MovieId = Guid.Parse("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),  // Shawshank Redemption
                    AvailableTickets = 110,
                    IsDeleted = false,
                    Showtime = "20:00"
                }
            };

            return projections;
        }
    }
}
