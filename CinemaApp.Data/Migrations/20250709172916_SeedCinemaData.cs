#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class SeedCinemaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "ManagerId", "Name" },
                values: new object[,]
                {
                    { new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), "Burgas, Bulgaria", null, "Cinema City Burgas Plaza" },
                    { new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), "Varna, Bulgaria", null, "Eccoplexx Varna" },
                    { new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), "Sofia, Bulgaria", null, "Arena Mall Sofia" },
                    { new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), "Sofia, Bulgaria", null, "IMAX Mall of Sofia" },
                    { new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), "Plovdiv, Bulgaria", null, "Cinema City Plovdiv" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("5ae6c761-1363-4a23-9965-171c70f935de"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"));
        }
    }
}
