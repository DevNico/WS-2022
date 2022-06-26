using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class ServiceWithTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId1",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceTemplateId",
                table: "Services",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganisationId1",
                table: "Services",
                column: "OrganisationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTemplateId",
                table: "Services",
                column: "ServiceTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organisations_OrganisationId1",
                table: "Services",
                column: "OrganisationId1",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceTemplates_ServiceTemplateId",
                table: "Services",
                column: "ServiceTemplateId",
                principalTable: "ServiceTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organisations_OrganisationId1",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceTemplates_ServiceTemplateId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_OrganisationId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceTemplateId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceTemplateId",
                table: "Services");
        }
    }
}
