﻿// <auto-generated />
using System;
using AuctionMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuctionMicroservice.Migrations
{
    [DbContext(typeof(DocumentationIndividualContext))]
    partial class DocumentationIndividualContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuctionMicroservice.Entities.DocumentationIndividual", b =>
                {
                    b.Property<Guid>("DocumentationIndividualId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentationIndividualId");

                    b.ToTable("documentationIndividuals");

                    b.HasData(
                        new
                        {
                            DocumentationIndividualId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            FirstName = "Marko",
                            IdentificationNumber = "0819423841941",
                            Surname = "Milic"
                        },
                        new
                        {
                            DocumentationIndividualId = new Guid("6a411c17-a195-48f7-8dbd-67596c3974c0"),
                            FirstName = "Stefan",
                            IdentificationNumber = "0214120948120",
                            Surname = "Zoric"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
