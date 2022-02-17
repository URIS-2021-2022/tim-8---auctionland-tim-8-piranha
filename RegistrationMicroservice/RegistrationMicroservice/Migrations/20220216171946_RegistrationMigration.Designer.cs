﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationMicroservice.Entities;

namespace RegistrationMicroservice.Migrations
{
    [DbContext(typeof(RegistrationContext))]
    [Migration("20220216171946_RegistrationMigration")]
    partial class RegistrationMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RegistrationMicroservice.Entities.Auction", b =>
                {
                    b.Property<Guid>("AuctionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApplicationDeadline")
                        .HasColumnType("datetime2");

                    b.Property<int>("AuctionNum")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PriceStep")
                        .HasColumnType("int");

                    b.Property<int>("Restriction")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("AuctionId");

                    b.ToTable("auction");

                    b.HasData(
                        new
                        {
                            AuctionId = new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                            ApplicationDeadline = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AuctionNum = 1,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PriceStep = 13,
                            Restriction = 25,
                            Year = 2022
                        });
                });

            modelBuilder.Entity("RegistrationMicroservice.Entities.Buyer", b =>
                {
                    b.Property<Guid>("BuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BoughtSurface")
                        .HasColumnType("int");

                    b.Property<DateTime>("RestrictionEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestrictionPeriodInYears")
                        .HasColumnType("int");

                    b.Property<DateTime>("RestrictionStart")
                        .HasColumnType("datetime2");

                    b.HasKey("BuyerId");

                    b.ToTable("buyer");

                    b.HasData(
                        new
                        {
                            BuyerId = new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"),
                            BoughtSurface = 23,
                            RestrictionEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestrictionPeriodInYears = 12,
                            RestrictionStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RegistrationMicroservice.Entities.Registration", b =>
                {
                    b.Property<Guid>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuctionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegistrationId");

                    b.HasIndex("AuctionId");

                    b.HasIndex("BuyerId");

                    b.ToTable("registration");

                    b.HasData(
                        new
                        {
                            RegistrationId = new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                            AuctionId = new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                            BuyerId = new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"),
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "TEst"
                        });
                });

            modelBuilder.Entity("RegistrationMicroservice.Entities.Registration", b =>
                {
                    b.HasOne("RegistrationMicroservice.Entities.Auction", "auction")
                        .WithMany()
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RegistrationMicroservice.Entities.Buyer", "buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("auction");

                    b.Navigation("buyer");
                });
#pragma warning restore 612, 618
        }
    }
}
