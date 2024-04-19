using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LB12BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class scdc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[,]
                {
                    { 10, 37, "example1", "Tom" },
                    { 11, 41, "example2", "Bob" },
                    { 12, 24, "example3", "Sam" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 37, "example1", "Tom" },
                    { 2, 41, "example2", "Bob" },
                    { 3, 24, "example3", "Sam" }
                });
        }
    }
}
