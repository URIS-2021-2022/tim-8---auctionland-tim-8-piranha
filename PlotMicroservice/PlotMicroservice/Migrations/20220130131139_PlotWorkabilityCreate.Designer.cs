﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlotMicroservice.Entities;

namespace PlotMicroservice.Migrations
{
    [DbContext(typeof(PlotContext))]
    [Migration("20220130131139_PlotWorkabilityCreate")]
    partial class PlotWorkabilityCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlotMicroservice.Entities.PlotCadastralMunicipality", b =>
                {
                    b.Property<Guid>("PlotCadastralMunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CadastralMunicipality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlotCadastralMunicipalityId");

                    b.ToTable("PlotCadastralMunicipalities");

                    b.HasData(
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                            CadastralMunicipality = "Čantavir"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("458adb42-62a5-4117-8101-7d933fa88abb"),
                            CadastralMunicipality = "Bački Vinogradi"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"),
                            CadastralMunicipality = "Bikovo"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("98b39864-1763-49d4-91c7-3d95060ebd5e"),
                            CadastralMunicipality = "Đuđin"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("f305096b-52fd-4c43-8699-05bc3ee664b7"),
                            CadastralMunicipality = "Žedin"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("37841f52-2e51-45ea-af4e-bc67b5c5d0e9"),
                            CadastralMunicipality = "Tavankut"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("372d9458-a560-4b56-8119-ada1f7feb723"),
                            CadastralMunicipality = "Bajmok"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("321e3608-d760-4067-bfb5-695784bd2dd3"),
                            CadastralMunicipality = "Donji Grad"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("aee6dace-3f2d-43b5-b853-7d08e20ac81f"),
                            CadastralMunicipality = "Stari Grad"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("5bffadaf-117e-4d87-9f32-ef39e83d1499"),
                            CadastralMunicipality = "Novi Grad"
                        },
                        new
                        {
                            PlotCadastralMunicipalityId = new Guid("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"),
                            CadastralMunicipality = "Palić"
                        });
                });

            modelBuilder.Entity("PlotMicroservice.Entities.PlotCulture", b =>
                {
                    b.Property<Guid>("PlotCultureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Culture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlotCultureId");

                    b.ToTable("PlotCultures");

                    b.HasData(
                        new
                        {
                            PlotCultureId = new Guid("ba9777ce-d43f-4f71-a163-7c974e36654f"),
                            Culture = "Njive"
                        },
                        new
                        {
                            PlotCultureId = new Guid("60644cdd-795b-47a2-96ac-55f623862efe"),
                            Culture = "Vrtovi"
                        },
                        new
                        {
                            PlotCultureId = new Guid("2484a534-4e5f-4b0c-af35-190ae0d68fcc"),
                            Culture = "Voćnjaci"
                        },
                        new
                        {
                            PlotCultureId = new Guid("7b199139-6b41-4087-89b0-84d911b5fe2b"),
                            Culture = "Vinogradi"
                        },
                        new
                        {
                            PlotCultureId = new Guid("97adad6e-f225-4164-8830-b59004c812c3"),
                            Culture = "Livade"
                        },
                        new
                        {
                            PlotCultureId = new Guid("cc506ecd-fb9e-48d8-af90-26ecc5d9feba"),
                            Culture = "Pašnjaci"
                        },
                        new
                        {
                            PlotCultureId = new Guid("3262a3e8-a113-431f-8f2f-98a10d97c5a4"),
                            Culture = "Šume"
                        },
                        new
                        {
                            PlotCultureId = new Guid("a0c1727d-bb2c-4243-a907-be6f3a005558"),
                            Culture = "Trstici-močvare"
                        });
                });

            modelBuilder.Entity("PlotMicroservice.Entities.PlotWorkability", b =>
                {
                    b.Property<Guid>("PlotWorkabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Workability")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlotWorkabilityId");

                    b.ToTable("PlotWorkabilities");

                    b.HasData(
                        new
                        {
                            PlotWorkabilityId = new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"),
                            Workability = "Obradivo"
                        },
                        new
                        {
                            PlotWorkabilityId = new Guid("40d2641b-8b85-4625-b01c-a129389a6aad"),
                            Workability = "Ostalo"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
