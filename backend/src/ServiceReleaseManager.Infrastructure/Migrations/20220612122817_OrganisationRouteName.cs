using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class OrganisationRouteName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteName",
                table: "Organisations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteName",
                table: "Organisations");
        }
    }
}
