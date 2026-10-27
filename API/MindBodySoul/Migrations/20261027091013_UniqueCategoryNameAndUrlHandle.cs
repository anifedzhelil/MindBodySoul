using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindBodySoul.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCategoryNameAndUrlHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UrlHandle",
                table: "Categories",
                column: "UrlHandle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UrlHandle",
                table: "Categories");
        }
    }
}
