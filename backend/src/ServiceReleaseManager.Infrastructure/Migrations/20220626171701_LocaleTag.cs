using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class LocaleTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Locale");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Locale");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Locale",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Locale");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Locale",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Locale",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }
    }
}
