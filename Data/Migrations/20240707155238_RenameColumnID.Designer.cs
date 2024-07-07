﻿// <auto-generated />
using System;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ContextCommand))]
    [Migration("20240707155238_RenameColumnID")]
    partial class RenameColumnID
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.SettingsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<bool>("IsVisibleLastSeen")
                        .HasColumnType("boolean")
                        .HasColumnName("IS_VISIBLE_LAST_SEEN");

                    b.Property<bool>("IsVisibleMessageSeen")
                        .HasColumnType("boolean")
                        .HasColumnName("IS_VISIBLE_MESSAGE_SEEN");

                    b.Property<bool>("IsVisibleStatus")
                        .HasColumnType("boolean")
                        .HasColumnName("IS_VISIBLE_STATUS");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("settings", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_DATE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("EMAIL");

                    b.Property<DateTime?>("LastSeen")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LAST_SEEN");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("NAME");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("NICK_NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("PHOTO");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.SettingsModel", b =>
                {
                    b.HasOne("Domain.Entities.UserModel", "User")
                        .WithOne("Settings")
                        .HasForeignKey("Domain.Entities.SettingsModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Settings_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserModel", b =>
                {
                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}
