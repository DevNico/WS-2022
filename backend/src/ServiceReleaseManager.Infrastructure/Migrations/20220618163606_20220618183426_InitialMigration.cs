using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    public partial class _20220618183426_InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RouteName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ReleaseCreate = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseApprove = table.Column<bool>(type: "boolean", nullable: false),
                    ReleasePublish = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseMetadataEdit = table.Column<bool>(type: "boolean", nullable: false),
                    ReleaseLocalizedMetadataEdit = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRole", x => x.Id);
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
                name: "OrganisationRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ServiceWrite = table.Column<bool>(type: "boolean", nullable: false),
                    ServiceDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserRead = table.Column<bool>(type: "boolean", nullable: false),
                    UserWrite = table.Column<bool>(type: "boolean", nullable: false),
                    UserDelete = table.Column<bool>(type: "boolean", nullable: false),
                    OrganisationId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationRole_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OrganisationId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastSignIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrganisationId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_OrganisationRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OrganisationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    LanguageCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locale_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiresApproval = table.Column<bool>(type: "boolean", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseTarget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseTarget_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Metadata = table.Column<string>(type: "json", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_Release_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceRoleId = table.Column<int>(type: "integer", nullable: false),
                    OrganisationUserId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceUser_OrganisationUser_OrganisationUserId",
                        column: x => x.OrganisationUserId,
                        principalTable: "OrganisationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUser_ServiceRole_ServiceRoleId",
                        column: x => x.ServiceRoleId,
                        principalTable: "ServiceRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUser_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
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
                name: "IX_Locale_ServiceId",
                table: "Locale",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationRole_OrganisationId",
                table: "OrganisationRole",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_RouteName",
                table: "Organisations",
                column: "RouteName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_OrganisationId",
                table: "OrganisationUser",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_RoleId",
                table: "OrganisationUser",
                column: "RoleId");

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
                name: "IX_ServiceRole_Name",
                table: "ServiceRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_OrganisationId",
                table: "Services",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplates_Name",
                table: "ServiceTemplates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUser_OrganisationUserId",
                table: "ServiceUser",
                column: "OrganisationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUser_ServiceId",
                table: "ServiceUser",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUser_ServiceRoleId",
                table: "ServiceUser",
                column: "ServiceRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locale");

            migrationBuilder.DropTable(
                name: "Release");

            migrationBuilder.DropTable(
                name: "ReleaseTrigger");

            migrationBuilder.DropTable(
                name: "ServiceTemplates");

            migrationBuilder.DropTable(
                name: "ServiceUser");

            migrationBuilder.DropTable(
                name: "ReleaseTarget");

            migrationBuilder.DropTable(
                name: "OrganisationUser");

            migrationBuilder.DropTable(
                name: "ServiceRole");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "OrganisationRole");

            migrationBuilder.DropTable(
                name: "Organisations");
        }
    }
}
