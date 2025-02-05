﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using seda_dll.Data;

#nullable disable

namespace seda_dll.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250105120646_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("seda_dll.Models.IncidentDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeFirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("employee_first_name");

                    b.Property<string>("EmployeeLastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("employee_last_name");

                    b.Property<string>("FileSystemId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_system_id");

                    b.Property<string>("IncidentLevel")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("incident_level");

                    b.Property<string>("SecurityOrganizationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("security_name");

                    b.Property<string>("TargetedOrganizationAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("targeted_address");

                    b.Property<double>("TargetedOrganizationLatitude")
                        .HasColumnType("double precision")
                        .HasColumnName("targeted_longitude");

                    b.Property<double>("TargetedOrganizationLongitude")
                        .HasColumnType("double precision")
                        .HasColumnName("targeted_latitude");

                    b.Property<string>("TargetedOrganizationName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("targeted_name");

                    b.HasKey("Id");

                    b.ToTable("incident_documents", (string)null);
                });

            modelBuilder.Entity("seda_dll.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
