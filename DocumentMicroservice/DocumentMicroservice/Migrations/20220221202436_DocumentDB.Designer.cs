﻿// <auto-generated />
using System;
using DocumentMicroservice.Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentMicroservice.Migrations
{
    [DbContext(typeof(DocumentContext))]
    [Migration("20220221202436_DocumentDB")]
    partial class DocumentDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocumentMicroservice.Entities.ContractLease", b =>
                {
                    b.Property<Guid>("contractLeaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateOfSigning")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("deadlineLandRestitution")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("documentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("guaranteeTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("placeOfSigning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("submissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("contractLeaseID");

                    b.HasIndex("documentId");

                    b.HasIndex("guaranteeTypeID");

                    b.ToTable("contractLease");

                    b.HasData(
                        new
                        {
                            contractLeaseID = new Guid("86c9ac76-a632-4ffc-b2a2-26ea8600dc86"),
                            dateOfSigning = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            deadlineLandRestitution = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentId = new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                            guaranteeTypeID = new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                            placeOfSigning = "Zrenjanin",
                            serialNumber = "2342323",
                            submissionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DocumentMicroservice.Entities.Document", b =>
                {
                    b.Property<Guid>("documentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("docStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("documentCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("documentDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("documentStatusdocStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("documentTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("registrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("documentId");

                    b.HasIndex("documentStatusdocStatusID");

                    b.ToTable("Document");

                    b.HasData(
                        new
                        {
                            documentId = new Guid("07af89f2-feee-4680-b489-9d0e31699588"),
                            docStatusID = new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                            documentCreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentTemplate = "Kreiranje predloga plana",
                            registrationNumber = "119833332"
                        },
                        new
                        {
                            documentId = new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                            docStatusID = new Guid("458adb42-62a5-4117-8101-7d933fa88abb"),
                            documentCreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentTemplate = "Obrazovanje komisije za davanje misljenja",
                            registrationNumber = "122267432"
                        },
                        new
                        {
                            documentId = new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                            docStatusID = new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"),
                            documentCreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            documentTemplate = "Saglasnost ministra",
                            registrationNumber = "119834232"
                        });
                });

            modelBuilder.Entity("DocumentMicroservice.Entities.DocumentStatus", b =>
                {
                    b.Property<Guid>("docStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("docStatusID");

                    b.ToTable("DocumentStatus");

                    b.HasData(
                        new
                        {
                            docStatusID = new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                            status = "Usvojen"
                        },
                        new
                        {
                            docStatusID = new Guid("458adb42-62a5-4117-8101-7d933fa88abb"),
                            status = "Odbijen"
                        },
                        new
                        {
                            docStatusID = new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"),
                            status = "Otvoren"
                        });
                });

            modelBuilder.Entity("DocumentMicroservice.Entities.GuaranteeType", b =>
                {
                    b.Property<Guid>("guaranteeTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("guaranteeTypeID");

                    b.ToTable("GuaranteeTypes");

                    b.HasData(
                        new
                        {
                            guaranteeTypeID = new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                            type = "Jemstvo"
                        },
                        new
                        {
                            guaranteeTypeID = new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"),
                            type = "Bankarska garancija"
                        },
                        new
                        {
                            guaranteeTypeID = new Guid("372d9458-a560-4b56-8119-ada1f7feb723"),
                            type = "Garancija nekretnine"
                        },
                        new
                        {
                            guaranteeTypeID = new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                            type = "Zirantska"
                        },
                        new
                        {
                            guaranteeTypeID = new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                            type = "Uplata gotovine"
                        });
                });

            modelBuilder.Entity("DocumentMicroservice.Entities.ContractLease", b =>
                {
                    b.HasOne("DocumentMicroservice.Entities.Document", "document")
                        .WithMany()
                        .HasForeignKey("documentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentMicroservice.Entities.GuaranteeType", "guaranteeType")
                        .WithMany()
                        .HasForeignKey("guaranteeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("document");

                    b.Navigation("guaranteeType");
                });

            modelBuilder.Entity("DocumentMicroservice.Entities.Document", b =>
                {
                    b.HasOne("DocumentMicroservice.Entities.DocumentStatus", "documentStatus")
                        .WithMany()
                        .HasForeignKey("documentStatusdocStatusID");

                    b.Navigation("documentStatus");
                });
#pragma warning restore 612, 618
        }
    }
}