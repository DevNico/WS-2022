using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class FixRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organisations_OrganisationId1",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTemplates_Organisations_OrganisationId1",
                table: "ServiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTemplates_OrganisationId1",
                table: "ServiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrganisationId1",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "ServiceTemplates");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId1",
                table: "ServiceTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId1",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplates_OrganisationId1",
                table: "ServiceTemplates",
                column: "OrganisationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganisationId1",
                table: "Services",
                column: "OrganisationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organisations_OrganisationId1",
                table: "Services",
                column: "OrganisationId1",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTemplates_Organisations_OrganisationId1",
                table: "ServiceTemplates",
                column: "OrganisationId1",
                principalTable: "Organisations",
                principalColumn: "Id");
        }
    }
}
