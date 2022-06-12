using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class _20220614140608 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSignIn",
                table: "OrganisationUser",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "OrganisationUser",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "OrganisationUser",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrganisationUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrganisationRole",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "OrganisationRole",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganisationId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceRole",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ReleaseCreate = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseApprove = table.Column<bool>(type: "boolean", nullable: false),
                    ReleasePublish = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseMetadataEdit = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseLocalizedMetadataEdit = table.Column<bool>(type: "boolean", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRole", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StaticMetadata = table.Column<string>(type: "json", nullable: false),
                    LocalizedMetadata = table.Column<string>(type: "json", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    LanguageCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locale_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Release",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApprovedById = table.Column<int>(type: "integer", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublishedById = table.Column<int>(type: "integer", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    BuildNumber = table.Column<int>(type: "integer", nullable: false),
                    Metadata = table.Column<string>(type: "json", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Release", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Release_OrganisationUser_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "OrganisationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Release_OrganisationUser_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "OrganisationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Release_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReleaseTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiresApproval = table.Column<bool>(type: "boolean", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseTarget_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSevice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    ServiceRoleName = table.Column<string>(type: "character varying(50)", nullable: false),
                    OrganisationUserId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSevice_OrganisationUser_OrganisationUserId",
                        column: x => x.OrganisationUserId,
                        principalTable: "OrganisationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSevice_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSevice_ServiceRole_ServiceRoleName",
                        column: x => x.ServiceRoleName,
                        principalTable: "ServiceRole",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseTrigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Event = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ReleaseTargetId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseTrigger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseTrigger_ReleaseTarget_ReleaseTargetId",
                        column: x => x.ReleaseTargetId,
                        principalTable: "ReleaseTarget",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationRole_OrganisationId",
                table: "OrganisationRole",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locale_ServiceId",
                table: "Locale",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_ApprovedById",
                table: "Release",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Release_PublishedById",
                table: "Release",
                column: "PublishedById");

            migrationBuilder.CreateIndex(
                name: "IX_Release_ServiceId",
                table: "Release",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseTarget_ServiceId",
                table: "ReleaseTarget",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseTrigger_ReleaseTargetId",
                table: "ReleaseTrigger",
                column: "ReleaseTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_OrganisationId",
                table: "Service",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRole_Name",
                table: "ServiceRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplates_Name",
                table: "ServiceTemplates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSevice_OrganisationUserId",
                table: "UserSevice",
                column: "OrganisationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSevice_ServiceId",
                table: "UserSevice",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSevice_ServiceRoleName",
                table: "UserSevice",
                column: "ServiceRoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganisationRole_Organisations_OrganisationId",
                table: "OrganisationRole",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganisationRole_Organisations_OrganisationId",
                table: "OrganisationRole");

            migrationBuilder.DropTable(
                name: "Locale");

            migrationBuilder.DropTable(
                name: "Release");

            migrationBuilder.DropTable(
                name: "ReleaseTrigger");

            migrationBuilder.DropTable(
                name: "ServiceTemplates");

            migrationBuilder.DropTable(
                name: "UserSevice");

            migrationBuilder.DropTable(
                name: "ReleaseTarget");

            migrationBuilder.DropTable(
                name: "ServiceRole");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_OrganisationRole_OrganisationId",
                table: "OrganisationRole");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrganisationUser");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "OrganisationRole");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSignIn",
                table: "OrganisationUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "OrganisationUser",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "OrganisationUser",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrganisationRole",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
