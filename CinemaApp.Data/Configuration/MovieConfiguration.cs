namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.EntityConstants.Movie;

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> entity)
        {
            // Define the primary key of the Movie entity
            entity
                .HasKey(m => m.Id);

            // Define constraints for the Title column
            entity
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            // Define constraints for the Genre column
            entity
                .Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            // Define constraints for the ReleaseDate column
            entity
                .Property(m => m.ReleaseDate)
                .IsRequired();

            // Define constraints for the Director column
            entity
                .Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(DirectorNameMaxLength);

            // Define constraints for the Duration column
            entity
                .Property(m => m.Duration)
                .IsRequired();

            // Define constraints for the Description column
            entity
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            // Define constraints for the ImageUrl column
            entity
                .Property(m => m.ImageUrl)
                .IsRequired(false)
                .HasMaxLength(ImageUrlMaxLength);

            // Define constraints for the IsDeleted column
            entity
                .Property(m => m.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Filter out only the active (non-deleted) entries
            entity
                .HasQueryFilter(m => m.IsDeleted == false);

            // Seed movies data with migration for development
            entity
                .HasData(this.SeedMovies());
        }

        public List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = Guid.Parse("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                    Title = "Harry Potter and the Goblet of Fire",
                    Genre = "Fantasy",
                    ReleaseDate = new DateOnly(2005, 11, 01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTI1NDMyMjExOF5BMl5BanBnXkFtZTcwOTc4MjQzMQ@@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                    Title = "Lord of the Rings",
                    Genre = "Fantasy",
                    ReleaseDate = new DateOnly(2001, 05, 01),
                    Director = "Peter Jackson",
                    Duration = 178,
                    Description = "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNzIxMDQ2YTctNDY4MC00ZTRhLTk4ODQtMTVlOWY4NTdiYmMwXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateOnly(2010, 07, 16),
                    Director = "Christopher Nolan",
                    Duration = 148,
                    Description = "A thief who enters the dreams of others to steal secrets is given the inverse task: planting an idea into someone's mind.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                    Title = "The Dark Knight",
                    Genre = "Action",
                    ReleaseDate = new DateOnly(2008, 07, 18),
                    Director = "Christopher Nolan",
                    Duration = 152,
                    Description = "Batman faces the Joker, who seeks to create chaos in Gotham through psychological warfare.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                    Title = "Interstellar",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateOnly(2014, 11, 07),
                    Director = "Christopher Nolan",
                    Duration = 169,
                    Description = "A group of explorers travel through a wormhole in space in search of a new habitable planet.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),
                    Title = "Avatar",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateOnly(2009, 12, 18),
                    Director = "James Cameron",
                    Duration = 162,
                    Description = "A paraplegic Marine is sent to the moon Pandora on a mission but becomes torn between following orders and protecting an alien civilization.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMDEzMmQwZjctZWU2My00MWNlLWE0NjItMDJlYTRlNGJiZjcyXkEyXkFqcGc@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("ab2c3213-48a7-41ea-9393-45c60ef813e6"),
                    Title = "Titanic",
                    Genre = "Romance",
                    ReleaseDate = new DateOnly(1997, 12, 19),
                    Director = "James Cameron",
                    Duration = 195,
                    Description = "A love story unfolds on the doomed voyage of the Titanic.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BYzYyN2FiZmUtYWYzMy00MzViLWJkZTMtOGY1ZjgzNWMwN2YxXkEyXkFqcGc@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("844d9abd-104d-41ab-b14a-ce059779ad91"),
                    Title = "The Matrix",
                    Genre = "Sci-Fi",
                    ReleaseDate = new DateOnly(1999, 03, 31),
                    Director = "Lana Wachowski, Lilly Wachowski",
                    Duration = 136,
                    Description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BN2NmN2VhMTQtMDNiOS00NDlhLTliMjgtODE2ZTY0ODQyNDRhXkEyXkFqcGc@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("54082f99-023b-4d68-89ac-44c00a0958d0"),
                    Title = "Forrest Gump",
                    Genre = "Drama",
                    ReleaseDate = new DateOnly(1994, 07, 06),
                    Director = "Robert Zemeckis",
                    Duration = 142,
                    Description = "The story of a man with a kind heart and an incredible life journey.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNDYwNzVjMTItZmU5YS00YjQ5LTljYjgtMjY2NDVmYWMyNWFmXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"),
                    Title = "Gladiator",
                    Genre = "Action",
                    ReleaseDate = new DateOnly(2000, 05, 05),
                    Director = "Ridley Scott",
                    Duration = 155,
                    Description = "A betrayed Roman general seeks revenge against the corrupt emperor who murdered his family and sent him into slavery.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/f/fb/Gladiator_%282000_film_poster%29.png"
                },
                new Movie()
                {
                    Id = Guid.Parse("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),
                    Title = "The Shawshank Redemption",
                    Genre = "Drama",
                    ReleaseDate = new DateOnly(1994, 09, 23),
                    Director = "Frank Darabont",
                    Duration = 142,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMDAyY2FhYjctNDc5OS00MDNlLThiMGUtY2UxYWVkNGY2ZjljXkEyXkFqcGc@._V1_.jpg"
                },
                new Movie()
                {
                    Id = Guid.Parse("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),
                    Title = "Pulp Fiction",
                    Genre = "Crime",
                    ReleaseDate = new DateOnly(1994, 10, 14),
                    Director = "Quentin Tarantino",
                    Duration = 154,
                    Description = "The lives of two hitmen, a boxer, and a gangster intertwine in four tales of violence and redemption.",
                    ImageUrl = "https://www.tallengestore.com/cdn/shop/products/PulpFiction-JohnTravoltaAndSamuelLJackson-MovieStill1_d3db6d19-235a-45aa-97b2-ab690665c224.jpg?v=1684129898"
                }
            };

            return movies;
        }
    }
}
