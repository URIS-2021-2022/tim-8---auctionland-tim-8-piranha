﻿// <auto-generated />
using System;
using ComplaintMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComplaintMicroservice.Migrations
{
    [DbContext(typeof(ComplaintContext))]
    partial class ComplaintContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComplaintMicroservice.Entities.Complaint.Complaint", b =>
                {
                    b.Property<Guid>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComplaintEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComplaintStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComplaintTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DecisionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Explanation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ComplaintId");

                    b.HasIndex("ComplaintEventId");

                    b.HasIndex("ComplaintStatusId");

                    b.HasIndex("ComplaintTypeId");

                    b.ToTable("Complaint");

                    b.HasData(
                        new
                        {
                            ComplaintId = new Guid("eb6bac2d-aea4-485a-8cb6-991bf8b1e1d4"),
                            ComplaintEventId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            ComplaintStatusId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            ComplaintTypeId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DecisionNumber = "DN001",
                            Explanation = "Complaint explanation",
                            Reason = "Complaint reason",
                            SolutionNumber = "SN001",
                            SubmissionDate = new DateTime(2021, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ComplaintId = new Guid("b16abef5-5d4e-43a5-9bf3-1fe0618da6f7"),
                            ComplaintEventId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            ComplaintStatusId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            ComplaintTypeId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DecisionNumber = "DN002",
                            Explanation = "Complaint explanation 2",
                            Reason = "Complaint reason 2",
                            SolutionNumber = "SN002",
                            SubmissionDate = new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ComplaintMicroservice.Entities.ComplaintStatusEntities.ComplaintStatus", b =>
                {
                    b.Property<Guid>("ComplaintStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComplaintStatusId");

                    b.ToTable("ComplaintStatus");

                    b.HasData(
                        new
                        {
                            ComplaintStatusId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            Status = "Usvojena"
                        },
                        new
                        {
                            ComplaintStatusId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            Status = "Odbijena"
                        });
                });

            modelBuilder.Entity("ComplaintMicroservice.Entities.ComplaintTypeModel", b =>
                {
                    b.Property<Guid>("ComplaintTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ComplaintType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComplaintTypeId");

                    b.ToTable("ComplaintTypes");

                    b.HasData(
                        new
                        {
                            ComplaintTypeId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            ComplaintType = "Zalba na tok javnog nadmetanja"
                        },
                        new
                        {
                            ComplaintTypeId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            ComplaintType = "Zalba na Odluku o davanju u zakup"
                        });
                });

            modelBuilder.Entity("ComplaintMicroservice.Entities.Event.ComplaintEvent", b =>
                {
                    b.Property<Guid>("ComplaintEventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Event")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComplaintEventId");

                    b.ToTable("ComplaintEvent");

                    b.HasData(
                        new
                        {
                            ComplaintEventId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            Event = "JN ide u drugi krug sa novim uslovima"
                        },
                        new
                        {
                            ComplaintEventId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            Event = "JN ide u drugi krug sa starim uslovima"
                        });
                });

            modelBuilder.Entity("ComplaintMicroservice.Entities.Complaint.Complaint", b =>
                {
                    b.HasOne("ComplaintMicroservice.Entities.Event.ComplaintEvent", "ComplaintEvent")
                        .WithMany()
                        .HasForeignKey("ComplaintEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintMicroservice.Entities.ComplaintStatusEntities.ComplaintStatus", "ComplaintStatus")
                        .WithMany()
                        .HasForeignKey("ComplaintStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComplaintMicroservice.Entities.ComplaintTypeModel", "ComplaintType")
                        .WithMany()
                        .HasForeignKey("ComplaintTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComplaintEvent");

                    b.Navigation("ComplaintStatus");

                    b.Navigation("ComplaintType");
                });
#pragma warning restore 612, 618
        }
    }
}