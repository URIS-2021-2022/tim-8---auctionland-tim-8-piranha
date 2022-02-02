﻿// <auto-generated />
using System;
using ComplaintMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComplaintMicroservice.Migrations
{
    [DbContext(typeof(ComplaintContext))]
    [Migration("20220202165526_ComplaintTypeMigration")]
    partial class ComplaintTypeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
#pragma warning restore 612, 618
        }
    }
}
