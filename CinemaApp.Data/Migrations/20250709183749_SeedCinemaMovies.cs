#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class SeedCinemaMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CinemasMovies",
                columns: new[] { "Id", "AvailableTickets", "CinemaId", "MovieId", "Showtime" },
                values: new object[,]
                {
                    { new Guid("0241c54a-37e7-4c9a-bcfb-43f0a60749e2"), 60, new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"), "17:00" },
                    { new Guid("0e5c76b7-9e27-4217-a113-5cafd558d00f"), 70, new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"), "18:15" },
                    { new Guid("130f6630-5593-4165-8e9e-de718ee1fb72"), 70, new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"), "20:15" },
                    { new Guid("30864830-db09-412a-a816-6dbaccc1374c"), 150, new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"), "17:45" },
                    { new Guid("30c505d0-9833-4087-9377-43ac8ab34e07"), 90, new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), "21:00" },
                    { new Guid("3291c19e-5995-48af-9124-35f855bf8476"), 95, new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"), "19:45" },
                    { new Guid("6a7071a9-0c3d-42e5-9514-639c5fb259a3"), 100, new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"), "16:00" },
                    { new Guid("71a411ec-d23c-4abb-b50c-75571d0a3cff"), 120, new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), "18:30" },
                    { new Guid("93f61e0c-6e62-41bb-b4e2-fc770ac48128"), 40, new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"), "22:30" },
                    { new Guid("9d54f01b-b33d-4ed5-8c4c-6874d62b24dd"), 110, new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"), "20:00" },
                    { new Guid("a22c43b7-bd1d-46cd-b419-dba244e533cc"), 85, new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"), "20:00" },
                    { new Guid("c96549ed-7a19-4e83-856e-976cf306d611"), 60, new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"), "19:00" },
                    { new Guid("d00af316-049a-4bd5-97c2-c55fcab99783"), 80, new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), "20:30" },
                    { new Guid("ef18d96e-2e5b-4218-b552-e094e98ac178"), 50, new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"), "22:00" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("0241c54a-37e7-4c9a-bcfb-43f0a60749e2"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("0e5c76b7-9e27-4217-a113-5cafd558d00f"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("130f6630-5593-4165-8e9e-de718ee1fb72"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("30864830-db09-412a-a816-6dbaccc1374c"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("30c505d0-9833-4087-9377-43ac8ab34e07"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("3291c19e-5995-48af-9124-35f855bf8476"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("6a7071a9-0c3d-42e5-9514-639c5fb259a3"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("71a411ec-d23c-4abb-b50c-75571d0a3cff"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("93f61e0c-6e62-41bb-b4e2-fc770ac48128"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("9d54f01b-b33d-4ed5-8c4c-6874d62b24dd"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("a22c43b7-bd1d-46cd-b419-dba244e533cc"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("c96549ed-7a19-4e83-856e-976cf306d611"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("d00af316-049a-4bd5-97c2-c55fcab99783"));

            migrationBuilder.DeleteData(
                table: "CinemasMovies",
                keyColumn: "Id",
                keyValue: new Guid("ef18d96e-2e5b-4218-b552-e094e98ac178"));
        }
    }
}
