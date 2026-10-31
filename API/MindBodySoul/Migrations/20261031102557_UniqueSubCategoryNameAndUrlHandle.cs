using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindBodySoul.Migrations
{
    /// <inheritdoc />
    public partial class UniqueSubCategoryNameAndUrlHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_Name",
                table: "SubCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_UrlHandle",
                table: "SubCategories",
                column: "UrlHandle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubCategories_Name",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_UrlHandle",
                table: "SubCategories");
        }
    }
}
