﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceReleaseManager.Infrastructure.Data;

#nullable disable

namespace ServiceReleaseManager.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220626171701_LocaleTag")]
    partial class LocaleTag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RouteName")
                        .IsUnique();

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<bool>("ServiceDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("ServiceWrite")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("UserDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserRead")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserWrite")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.ToTable("OrganisationRole");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OrganisationId");

                    b.HasIndex("RoleId");

                    b.ToTable("OrganisationUsers");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ReleaseAggregate.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ApprovedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PublishedById")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("PublishedById");

                    b.HasIndex("ServiceId");

                    b.ToTable("Release");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ReleaseAggregate.ReleaseLocalisedMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LocaleId")
                        .HasColumnType("integer");

                    b.Property<string>("Metadata")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<int>("ReleaseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocaleId");

                    b.HasIndex("ReleaseId");

                    b.ToTable("ReleaseLocalisedMetadata");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.Locale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Locale");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<int>("OrganisationId1")
                        .HasColumnType("integer");

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ServiceTemplateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("OrganisationId1");

                    b.HasIndex("RouteName")
                        .IsUnique();

                    b.HasIndex("ServiceTemplateId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<bool>("ReleaseApprove")
                        .HasColumnType("boolean");

                    b.Property<bool>("ReleaseCreate")
                        .HasColumnType("boolean");

                    b.Property<bool>("ReleaseLocalizedMetadataEdit")
                        .HasColumnType("boolean");

                    b.Property<bool>("ReleaseMetadataEdit")
                        .HasColumnType("boolean");

                    b.Property<bool>("ReleasePublish")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganisationId");

                    b.ToTable("ServiceRole");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LocalizedMetadata")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.Property<int?>("OrganisationId1")
                        .HasColumnType("integer");

                    b.Property<string>("StaticMetadata")
                        .IsRequired()
                        .HasColumnType("json");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganisationId");

                    b.HasIndex("OrganisationId1");

                    b.ToTable("ServiceTemplates");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("OrganisationUserId")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceId")
                        .HasColumnType("integer");

                    b.Property<int>("ServiceRoleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationUserId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ServiceRoleId");

                    b.ToTable("ServiceUser");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationRole", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", null)
                        .WithMany("Roles")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", null)
                        .WithMany("Users")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ReleaseAggregate.Release", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", "PublishedBy")
                        .WithMany()
                        .HasForeignKey("PublishedById");

                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.Service", null)
                        .WithMany("Releases")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("PublishedBy");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ReleaseAggregate.ReleaseLocalisedMetadata", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.Locale", "Locale")
                        .WithMany()
                        .HasForeignKey("LocaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.ReleaseAggregate.Release", "Release")
                        .WithMany("LocalisedMetadataList")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locale");

                    b.Navigation("Release");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.Locale", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.Service", null)
                        .WithMany("Locales")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.Service", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", null)
                        .WithMany("Services")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.ServiceTemplate", "ServiceTemplate")
                        .WithMany()
                        .HasForeignKey("ServiceTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisation");

                    b.Navigation("ServiceTemplate");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceRole", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", null)
                        .WithMany("ServiceRoles")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceTemplate", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", null)
                        .WithMany("ServiceTemplates")
                        .HasForeignKey("OrganisationId1");

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.ServiceUser", b =>
                {
                    b.HasOne("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", "OrganisationUser")
                        .WithMany("ServiceUserList")
                        .HasForeignKey("OrganisationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.Service", null)
                        .WithMany("Users")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceReleaseManager.Core.ServiceAggregate.ServiceRole", "ServiceRole")
                        .WithMany()
                        .HasForeignKey("ServiceRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganisationUser");

                    b.Navigation("ServiceRole");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.Organisation", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("ServiceRoles");

                    b.Navigation("ServiceTemplates");

                    b.Navigation("Services");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.OrganisationAggregate.OrganisationUser", b =>
                {
                    b.Navigation("ServiceUserList");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ReleaseAggregate.Release", b =>
                {
                    b.Navigation("LocalisedMetadataList");
                });

            modelBuilder.Entity("ServiceReleaseManager.Core.ServiceAggregate.Service", b =>
                {
                    b.Navigation("Locales");

                    b.Navigation("Releases");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
