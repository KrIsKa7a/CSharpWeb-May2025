using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoviesDev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ImageUrl", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"), "Batman faces the Joker, who seeks to create chaos in Gotham through psychological warfare.", "Christopher Nolan", 152, "Action", "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg", new DateOnly(2008, 7, 18), "The Dark Knight" },
                    { new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"), "A group of explorers travel through a wormhole in space in search of a new habitable planet.", "Christopher Nolan", 169, "Sci-Fi", "https://m.media-amazon.com/images/M/MV5BYzdjMDAxZGItMjI2My00ODA1LTlkNzItOWFjMDU5ZDJlYWY3XkEyXkFqcGc@._V1_.jpg", new DateOnly(2014, 11, 7), "Interstellar" },
                    { new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"), "The lives of two hitmen, a boxer, and a gangster intertwine in four tales of violence and redemption.", "Quentin Tarantino", 154, "Crime", "https://www.tallengestore.com/cdn/shop/products/PulpFiction-JohnTravoltaAndSamuelLJackson-MovieStill1_d3db6d19-235a-45aa-97b2-ab690665c224.jpg?v=1684129898", new DateOnly(1994, 10, 14), "Pulp Fiction" },
                    { new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"), "The story of a man with a kind heart and an incredible life journey.", "Robert Zemeckis", 142, "Drama", "https://m.media-amazon.com/images/M/MV5BNDYwNzVjMTItZmU5YS00YjQ5LTljYjgtMjY2NDVmYWMyNWFmXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", new DateOnly(1994, 7, 6), "Forrest Gump" },
                    { new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), "A thief who enters the dreams of others to steal secrets is given the inverse task: planting an idea into someone's mind.", "Christopher Nolan", 148, "Sci-Fi", "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg", new DateOnly(2010, 7, 16), "Inception" },
                    { new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), "The Lord of the Rings: The Fellowship of the Ring is a 2001 epic high fantasy adventure film directed by Peter Jackson from a screenplay by Fran Walsh, Philippa Boyens, and Jackson, based on 1954's The Fellowship of the Ring, the first volume of the novel The Lord of the Rings by J. R. R. Tolkien.", "Peter Jackson", 178, "Fantasy", "https://m.media-amazon.com/images/M/MV5BNzIxMDQ2YTctNDY4MC00ZTRhLTk4ODQtMTVlOWY4NTdiYmMwXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", new DateOnly(2001, 5, 1), "Lord of the Rings" },
                    { new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"), "A paraplegic Marine is sent to the moon Pandora on a mission but becomes torn between following orders and protecting an alien civilization.", "James Cameron", 162, "Sci-Fi", "https://m.media-amazon.com/images/M/MV5BMDEzMmQwZjctZWU2My00MWNlLWE0NjItMDJlYTRlNGJiZjcyXkEyXkFqcGc@._V1_.jpg", new DateOnly(2009, 12, 18), "Avatar" },
                    { new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"), "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.", "Lana Wachowski, Lilly Wachowski", 136, "Sci-Fi", "https://m.media-amazon.com/images/M/MV5BN2NmN2VhMTQtMDNiOS00NDlhLTliMjgtODE2ZTY0ODQyNDRhXkEyXkFqcGc@._V1_.jpg", new DateOnly(1999, 3, 31), "The Matrix" },
                    { new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"), "A love story unfolds on the doomed voyage of the Titanic.", "James Cameron", 195, "Romance", "https://m.media-amazon.com/images/M/MV5BYzYyN2FiZmUtYWYzMy00MzViLWJkZTMtOGY1ZjgzNWMwN2YxXkEyXkFqcGc@._V1_.jpg", new DateOnly(1997, 12, 19), "Titanic" },
                    { new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), "Harry Potter and the Goblet of Fire is a 2005 fantasy film directed by Mike Newell from a screenplay by Steve Kloves. It is based on the 2000 novel Harry Potter and the Goblet of Fire by J. K. Rowling.", "Mike Newel", 157, "Fantasy", "https://m.media-amazon.com/images/M/MV5BMTI1NDMyMjExOF5BMl5BanBnXkFtZTcwOTc4MjQzMQ@@._V1_.jpg", new DateOnly(2005, 11, 1), "Harry Potter and the Goblet of Fire" },
                    { new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"), "A betrayed Roman general seeks revenge against the corrupt emperor who murdered his family and sent him into slavery.", "Ridley Scott", 155, "Action", "https://upload.wikimedia.org/wikipedia/en/f/fb/Gladiator_%282000_film_poster%29.png", new DateOnly(2000, 5, 5), "Gladiator" },
                    { new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"), "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "Frank Darabont", 142, "Drama", "https://m.media-amazon.com/images/M/MV5BMDAyY2FhYjctNDc5OS00MDNlLThiMGUtY2UxYWVkNGY2ZjljXkEyXkFqcGc@._V1_.jpg", new DateOnly(1994, 9, 23), "The Shawshank Redemption" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"));
        }
    }
}
