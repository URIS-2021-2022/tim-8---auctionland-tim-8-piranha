﻿// <auto-generated />
using System;
using AdMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdMicroservice.Migrations
{
    [DbContext(typeof(AdContext))]
    [Migration("20220216163847_AdMigration")]
    partial class AdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdMicroservice.Entities.Ad.AdModel", b =>
                {
                    b.Property<Guid>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JournalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PublicationDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdId");

                    b.HasIndex("JournalId");

                    b.ToTable("Ads");

                    b.HasData(
                        new
                        {
                            AdId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            JournalId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            PublicationDate = "01.06.2020."
                        },
                        new
                        {
                            AdId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            JournalId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            PublicationDate = "01.06.2020."
                        });
                });

            modelBuilder.Entity("AdMicroservice.Entities.Journal.JournalModel", b =>
                {
                    b.Property<Guid>("JournalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DateOfIssue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JournalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Municipality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JournalId");

                    b.ToTable("Journals");

                    b.HasData(
                        new
                        {
                            JournalId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DateOfIssue = "12.02.2021.",
                            JournalNumber = "J001",
                            Municipality = "Ruma"
                        },
                        new
                        {
                            JournalId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            DateOfIssue = "22.03.2021.",
                            JournalNumber = "J002",
                            Municipality = "Sremska Mitrovica"
                        });
                });

            modelBuilder.Entity("AdMicroservice.Entities.Ad.AdModel", b =>
                {
                    b.HasOne("AdMicroservice.Entities.Journal.JournalModel", "Journal")
                        .WithMany()
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journal");
                });
#pragma warning restore 612, 618
        }
    }
}
