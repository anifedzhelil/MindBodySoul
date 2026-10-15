using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindBodySoul.Migrations
{
    /// <inheritdoc />
    public partial class AddPusblishAndArchiveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentText",
                table: "Articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Articles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("UPDATE \"Articles\" SET \"IsPublished\" = true,   \"PublishedDate\" = \"CreatedDate\" WHERE \"PublishedDate\" IS NULL;");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Articles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE \"Articles\" SET \"Slug\" = \"Id\"::text;");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Slug",
                table: "Articles",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_Slug",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ContentText",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Articles");
        }
    }
}
