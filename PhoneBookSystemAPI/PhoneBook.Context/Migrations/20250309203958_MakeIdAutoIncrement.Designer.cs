﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhoneBook.Context;

#nullable disable

namespace PhoneBook.Context.Migrations
{
    [DbContext(typeof(PhoneBookContext))]
    [Migration("20250309203958_MakeIdAutoIncrement")]
    partial class MakeIdAutoIncrement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhoneBook.Model.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .HasDatabaseName("IX_Contact_Email_GIN");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Email"), "gin");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("Email"), new[] { "gin_trgm_ops" });

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Contact_Name_GIN");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Name"), "gin");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("Name"), new[] { "gin_trgm_ops" });

                    b.HasIndex("PhoneNumber")
                        .HasDatabaseName("IX_Contact_Phone_GIN");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("PhoneNumber"), "gin");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("PhoneNumber"), new[] { "gin_trgm_ops" });

                    b.ToTable("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
