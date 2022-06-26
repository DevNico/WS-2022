using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class ServiceTemplateByOrganisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "ServiceTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId1",
                table: "ServiceTemplates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplates_OrganisationId",
                table: "ServiceTemplates",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplates_OrganisationId1",
                table: "ServiceTemplates",
                column: "OrganisationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTemplates_Organisations_OrganisationId",
                table: "ServiceTemplates",
                column: "OrganisationId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTemplates_Organisations_OrganisationId",
                table: "ServiceTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTemplates_Organisations_OrganisationId1",
                table: "ServiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTemplates_OrganisationId",
                table: "ServiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTemplates_OrganisationId1",
                table: "ServiceTemplates");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "ServiceTemplates");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "ServiceTemplates");
        }
    }
}
