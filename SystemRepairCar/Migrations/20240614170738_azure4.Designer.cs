﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemRepairCar;

#nullable disable

namespace SystemRepairCar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240614170738_azure4")]
    partial class azure4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SystemRepairCar.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DamageReports")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SystemRepairCar.Models.EventMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventMessages");
                });

            modelBuilder.Entity("SystemRepairCar.Models.ElectricCar", b =>
                {
                    b.HasBaseType("SystemRepairCar.Models.Car");

                    b.Property<int>("BatteryCapacity")
                        .HasColumnType("int");

                    b.ToTable("ElectricCars", (string)null);
                });

            modelBuilder.Entity("SystemRepairCar.Models.GasolineCar", b =>
                {
                    b.HasBaseType("SystemRepairCar.Models.Car");

                    b.Property<int>("FuelCapacity")
                        .HasColumnType("int");

                    b.ToTable("GasolineCars", (string)null);
                });

            modelBuilder.Entity("SystemRepairCar.Models.ElectricCar", b =>
                {
                    b.HasOne("SystemRepairCar.Models.Car", null)
                        .WithOne()
                        .HasForeignKey("SystemRepairCar.Models.ElectricCar", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemRepairCar.Models.GasolineCar", b =>
                {
                    b.HasOne("SystemRepairCar.Models.Car", null)
                        .WithOne()
                        .HasForeignKey("SystemRepairCar.Models.GasolineCar", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}