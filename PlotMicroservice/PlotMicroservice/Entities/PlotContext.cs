﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotContext : DbContext
    {
        public PlotContext(DbContextOptions<PlotContext> options) : base(options)
        {

        }

        public DbSet<PlotCadastralMunicipality> PlotCadastralMunicipalities { get; set; }

        public DbSet<PlotCulture> PlotCultures { get; set; }

        public DbSet<PlotWorkability> PlotWorkabilities { get; set; }

        public DbSet<PlotPartFormOfOwnership> PlotPartFormOfOwnerships { get; set; }

        public DbSet<PlotPartClass> PlotPartClasses { get; set; }

        public DbSet<PlotPartProtectedZone> PlotPartProtectedZones { get; set; }

        public DbSet<Plot> Plots { get; set; }

        public DbSet<PlotPart> PlotParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PLOT CADASTRAL MUNICIPALITIES

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    CadastralMunicipality = "Čantavir"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    CadastralMunicipality = "Bački Vinogradi"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),
                    CadastralMunicipality = "Bikovo"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("98b39864-1763-49d4-91c7-3d95060ebd5e"),
                    CadastralMunicipality = "Đuđin"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("f305096b-52fd-4c43-8699-05bc3ee664b7"),
                    CadastralMunicipality = "Žedin"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("37841f52-2e51-45ea-af4e-bc67b5c5d0e9"),
                    CadastralMunicipality = "Tavankut"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("372d9458-a560-4b56-8119-ada1f7feb723"),
                    CadastralMunicipality = "Bajmok"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("321e3608-d760-4067-bfb5-695784bd2dd3"),
                    CadastralMunicipality = "Donji Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("aee6dace-3f2d-43b5-b853-7d08e20ac81f"),
                    CadastralMunicipality = "Stari Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("5bffadaf-117e-4d87-9f32-ef39e83d1499"),
                    CadastralMunicipality = "Novi Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"),
                    CadastralMunicipality = "Palić"
                });


            // PLOT CULTURES

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("ba9777ce-d43f-4f71-a163-7c974e36654f"),
                    Culture = "Njive"
                });

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("60644cdd-795b-47a2-96ac-55f623862efe"),
                    Culture = "Vrtovi"
                });

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("2484a534-4e5f-4b0c-af35-190ae0d68fcc"),
                    Culture = "Voćnjaci"
                });

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("7b199139-6b41-4087-89b0-84d911b5fe2b"),
                    Culture = "Vinogradi"
                });

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("97adad6e-f225-4164-8830-b59004c812c3"),
                    Culture = "Livade"
                });

            modelBuilder.Entity<PlotCulture>().HasData(
                new
                {
                    PlotCultureId = Guid.Parse("cc506ecd-fb9e-48d8-af90-26ecc5d9feba"),
                    Culture = "Pašnjaci"
                });


            modelBuilder.Entity<PlotCulture>().HasData(
               new
               {
                   PlotCultureId = Guid.Parse("3262a3e8-a113-431f-8f2f-98a10d97c5a4"),
                   Culture = "Šume"
               });

            modelBuilder.Entity<PlotCulture>().HasData(
               new
               {
                   PlotCultureId = Guid.Parse("a0c1727d-bb2c-4243-a907-be6f3a005558"),
                   Culture = "Trstici-močvare"
               });


            // PLOT WORKABILITIES

            modelBuilder.Entity<PlotWorkability>().HasData(
                new
                {
                    PlotWorkabilityId = Guid.Parse("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"),
                    Workability = "Obradivo"
                });

            modelBuilder.Entity<PlotWorkability>().HasData(
                new
                {
                    PlotWorkabilityId = Guid.Parse("40d2641b-8b85-4625-b01c-a129389a6aad"),
                    Workability = "Ostalo"
                });


           // PLOT PART FORM OF OWNERSHIPS

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"),
                    FormOfOwnership = "Privatna svojina"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                    FormOfOwnership = "Državna svojina RS"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("3075f4ce-e8f4-4b68-bd22-246363d71a57"),
                    FormOfOwnership = "Državna svojina"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("aa444022-1e63-44f5-8cf4-7df5045af184"),
                    FormOfOwnership = "Društvena svojina"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("b8e349da-6c4d-4282-acb1-872628128fc1"),
                    FormOfOwnership = "Zadružna svojina"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                    FormOfOwnership = "Mešovita svojina"
                });

            modelBuilder.Entity<PlotPartFormOfOwnership>().HasData(
                new
                {
                    PlotPartFormOfOwnershipId = Guid.Parse("a2c789e8-9e35-43d6-bf2e-156d776aeceb"),
                    FormOfOwnership = "Drugi oblici"
                });


            // PLOT PART CLASSES

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("1794fc01-2d12-4f5d-aaec-7eb219635052"),
                    Class = "I"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("5b957c06-8ca6-4658-ad45-78e62c465b3d"),
                    Class = "II"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("6f2629db-8de7-496c-97e0-75b1a94b1db3"),
                    Class = "III"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("5e69aeb5-4fec-4dd9-ba69-a474f06721f2"),
                    Class = "IV"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                    Class = "V"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("b2ddef8e-eddc-4fb0-884b-1701ab983bed"),
                    Class = "VI"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("a9a4427b-889d-4be4-bf9c-386edb323d9c"),
                    Class = "VII"
                });

            modelBuilder.Entity<PlotPartClass>().HasData(
                new
                {
                    PlotPartClassId = Guid.Parse("1965dce3-a24a-4e7c-a6d1-fddbbfeabc44"),
                    Class = "VIII"
                });


            // PLOT PART PROTECTED ZONES

            modelBuilder.Entity<PlotPartProtectedZone>().HasData(
                new
                {
                    PlotPartProtectedZoneId = Guid.Parse("f66b8360-33d2-48e9-9be5-b208988d1fb1"),
                    ProtectedZone = "1"
                });

            modelBuilder.Entity<PlotPartProtectedZone>().HasData(
                new
                {
                    PlotPartProtectedZoneId = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                    ProtectedZone = "2"
                });

            modelBuilder.Entity<PlotPartProtectedZone>().HasData(
                new
                {
                    PlotPartProtectedZoneId = Guid.Parse("de569d06-4787-4808-b4f6-0efea24f6b03"),
                    ProtectedZone = "3"
                });

            modelBuilder.Entity<PlotPartProtectedZone>().HasData(
                new
                {
                    PlotPartProtectedZoneId = Guid.Parse("4debaa6a-1a2f-43e0-bb82-1b7ca1824318"),
                    ProtectedZone = "4"
                });


            // PLOTS

            modelBuilder.Entity<Plot>().HasData(
                new
                {
                    PlotId = Guid.Parse("b281612e-8013-40cc-b9ce-f9d063295420"),
                    PlotCultureId = Guid.Parse("ba9777ce-d43f-4f71-a163-7c974e36654f"),
                    PlotCadastralMunicipalityId = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    PlotWorkabilityId = Guid.Parse("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"),
                    PlotSurfaceArea = 4500,
                    PlotNumber = "112",
                    PlotRealEstateListNumber = "LN100",
                    PlotCurrentCulture = "",
                    PlotCurrentWorkability = "",
                    BuyerId = Guid.Parse("82604d24-94d3-4490-9ae3-3669cbbf498f")
                });

            modelBuilder.Entity<Plot>().HasData(
                new
                {
                    PlotId = Guid.Parse("c6ea356d-c1c1-4374-985b-f8f91d35daa1"),
                    PlotCultureId = Guid.Parse("60644cdd-795b-47a2-96ac-55f623862efe"),
                    PlotCadastralMunicipalityId = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    PlotWorkabilityId = Guid.Parse("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"),
                    PlotSurfaceArea = 5600,
                    PlotNumber = "146",
                    PlotRealEstateListNumber = "LN202",
                    PlotCurrentCulture = "",
                    PlotCurrentWorkability = "",
                    BuyerId = Guid.Parse("1278d3e0-5aa7-4b8b-9477-cf7e35221062")
                });

            modelBuilder.Entity<Plot>().HasData(
                new
                {
                    PlotId = Guid.Parse("226480a5-74db-4507-958a-8963c4a36716"),
                    PlotCultureId = Guid.Parse("2484a534-4e5f-4b0c-af35-190ae0d68fcc"),
                    PlotCadastralMunicipalityId = Guid.Parse("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"),
                    PlotWorkabilityId = Guid.Parse("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"),
                    PlotSurfaceArea = 3850,
                    PlotNumber = "5308",
                    PlotRealEstateListNumber = "LN550",
                    PlotCurrentCulture = "",
                    PlotCurrentWorkability = "",
                    BuyerId = Guid.Parse("1daad0bf-8b24-439d-ba78-a68dcd10083b")
                });

            modelBuilder.Entity<Plot>().HasData(
                new
                {
                    PlotId = Guid.Parse("5f37ba98-ca19-4c9e-8914-708e38bba8bf"),
                    PlotCultureId = Guid.Parse("97adad6e-f225-4164-8830-b59004c812c3"),
                    PlotCadastralMunicipalityId = Guid.Parse("372d9458-a560-4b56-8119-ada1f7feb723"),
                    PlotWorkabilityId = Guid.Parse("40d2641b-8b85-4625-b01c-a129389a6aad"),
                    PlotSurfaceArea = 7602,
                    PlotNumber = "97",
                    PlotRealEstateListNumber = "LN90",
                    PlotCurrentCulture = "",
                    PlotCurrentWorkability = "",
                    BuyerId = Guid.Parse("abaa1bb8-f8c7-4f61-b0db-8bb062bb3f7d")
                });


            // PLOT PARTS

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("d14d555d-9637-4244-8ab3-dad55097259b"),
                    PlotPartNumber = "112/1",
                    PlotPartSurfaceArea = 1900,
                    PlotId = Guid.Parse("b281612e-8013-40cc-b9ce-f9d063295420"),
                    PlotPartClassId = Guid.Parse("1794fc01-2d12-4f5d-aaec-7eb219635052"),
                    PlotPartProtectedZoneId = Guid.Parse("f66b8360-33d2-48e9-9be5-b208988d1fb1"),
                    PlotPartFormOfOwnershipId = Guid.Parse("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("1f61ce66-fc70-42a5-ae25-6671d294f879"),
                    PlotPartNumber = "112/3",
                    PlotPartSurfaceArea = 2600,
                    PlotId = Guid.Parse("b281612e-8013-40cc-b9ce-f9d063295420"),
                    PlotPartClassId = Guid.Parse("1794fc01-2d12-4f5d-aaec-7eb219635052"),
                    PlotPartProtectedZoneId = Guid.Parse("f66b8360-33d2-48e9-9be5-b208988d1fb1"),
                    PlotPartFormOfOwnershipId = Guid.Parse("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("fb974419-6f20-4969-950e-e0f0ccb58593"),
                    PlotPartNumber = "146/2",
                    PlotPartSurfaceArea = 2200,
                    PlotId = Guid.Parse("c6ea356d-c1c1-4374-985b-f8f91d35daa1"),
                    PlotPartClassId = Guid.Parse("5b957c06-8ca6-4658-ad45-78e62c465b3d"),
                    PlotPartProtectedZoneId = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                    PlotPartFormOfOwnershipId = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                    PlotPartNumber = "146/4",
                    PlotPartSurfaceArea = 1600,
                    PlotId = Guid.Parse("c6ea356d-c1c1-4374-985b-f8f91d35daa1"),
                    PlotPartClassId = Guid.Parse("5b957c06-8ca6-4658-ad45-78e62c465b3d"),
                    PlotPartProtectedZoneId = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                    PlotPartFormOfOwnershipId = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"),
                    PlotPartNumber = "146/5",
                    PlotPartSurfaceArea = 2800,
                    PlotId = Guid.Parse("c6ea356d-c1c1-4374-985b-f8f91d35daa1"),
                    PlotPartClassId = Guid.Parse("6f2629db-8de7-496c-97e0-75b1a94b1db3"),
                    PlotPartProtectedZoneId = Guid.Parse("f66b8360-33d2-48e9-9be5-b208988d1fb1"),
                    PlotPartFormOfOwnershipId = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("f083e28b-6352-4fda-a172-0d579390e632"),
                    PlotPartNumber = "5308/1",
                    PlotPartSurfaceArea = 2700,
                    PlotId = Guid.Parse("226480a5-74db-4507-958a-8963c4a36716"),
                    PlotPartClassId = Guid.Parse("6f2629db-8de7-496c-97e0-75b1a94b1db3"),
                    PlotPartProtectedZoneId = Guid.Parse("de569d06-4787-4808-b4f6-0efea24f6b03"),
                    PlotPartFormOfOwnershipId = Guid.Parse("aa444022-1e63-44f5-8cf4-7df5045af184"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });


            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("861f142c-4707-416d-ad14-7debbd2031ed"),
                    PlotPartNumber = "5308/2",
                    PlotPartSurfaceArea = 1150,
                    PlotId = Guid.Parse("226480a5-74db-4507-958a-8963c4a36716"),
                    PlotPartClassId = Guid.Parse("6f2629db-8de7-496c-97e0-75b1a94b1db3"),
                    PlotPartProtectedZoneId = Guid.Parse("de569d06-4787-4808-b4f6-0efea24f6b03"),
                    PlotPartFormOfOwnershipId = Guid.Parse("aa444022-1e63-44f5-8cf4-7df5045af184"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });

            modelBuilder.Entity<PlotPart>().HasData(
                new
                {
                    PlotPartId = Guid.Parse("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                    PlotPartNumber = "97",
                    PlotPartSurfaceArea = 7602,
                    PlotId = Guid.Parse("5f37ba98-ca19-4c9e-8914-708e38bba8bf"),
                    PlotPartClassId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                    PlotPartProtectedZoneId = Guid.Parse("4debaa6a-1a2f-43e0-bb82-1b7ca1824318"),
                    PlotPartFormOfOwnershipId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                    PlotPartCurrentClass = "",
                    PlotPartCurrentProtectedZone = ""
                });
        }
    }
}
