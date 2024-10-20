using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindBodySoul.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "SubCategories",
                newName: "Icon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "SubCategories",
                newName: "Logo");
        }
    }
}
