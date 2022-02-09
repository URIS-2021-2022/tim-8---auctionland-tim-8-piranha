using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlotCadastralMunicipalities",
                columns: table => new
                {
                    PlotCadastralMunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadastralMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotCadastralMunicipalities", x => x.PlotCadastralMunicipalityId);
                });

            migrationBuilder.CreateTable(
                name: "PlotCultures",
                columns: table => new
                {
                    PlotCultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotCultures", x => x.PlotCultureId);
                });

            migrationBuilder.CreateTable(
                name: "PlotPartClasses",
                columns: table => new
                {
                    PlotPartClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotPartClasses", x => x.PlotPartClassId);
                });

            migrationBuilder.CreateTable(
                name: "PlotPartFormOfOwnerships",
                columns: table => new
                {
                    PlotPartFormOfOwnershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormOfOwnership = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotPartFormOfOwnerships", x => x.PlotPartFormOfOwnershipId);
                });

            migrationBuilder.CreateTable(
                name: "PlotPartProtectedZones",
                columns: table => new
                {
                    PlotPartProtectedZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProtectedZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotPartProtectedZones", x => x.PlotPartProtectedZoneId);
                });

            migrationBuilder.CreateTable(
                name: "PlotWorkabilities",
                columns: table => new
                {
                    PlotWorkabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Workability = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotWorkabilities", x => x.PlotWorkabilityId);
                });

            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    PlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotCultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotCadastralMunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotWorkabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotSurfaceArea = table.Column<int>(type: "int", nullable: false),
                    PlotNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotRealEstateListNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotCurrentCulture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotCurrentWorkability = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plots", x => x.PlotId);
                    table.ForeignKey(
                        name: "FK_Plots_PlotCadastralMunicipalities_PlotCadastralMunicipalityId",
                        column: x => x.PlotCadastralMunicipalityId,
                        principalTable: "PlotCadastralMunicipalities",
                        principalColumn: "PlotCadastralMunicipalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plots_PlotCultures_PlotCultureId",
                        column: x => x.PlotCultureId,
                        principalTable: "PlotCultures",
                        principalColumn: "PlotCultureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plots_PlotWorkabilities_PlotWorkabilityId",
                        column: x => x.PlotWorkabilityId,
                        principalTable: "PlotWorkabilities",
                        principalColumn: "PlotWorkabilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlotParts",
                columns: table => new
                {
                    PlotPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartProtectedZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartFormOfOwnershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartSurfaceArea = table.Column<int>(type: "int", nullable: false),
                    PlotPartCurrentClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotPartCurrentProtectedZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotParts", x => x.PlotPartId);
                    table.ForeignKey(
                        name: "FK_PlotParts_PlotPartClasses_PlotPartClassId",
                        column: x => x.PlotPartClassId,
                        principalTable: "PlotPartClasses",
                        principalColumn: "PlotPartClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotParts_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                        column: x => x.PlotPartFormOfOwnershipId,
                        principalTable: "PlotPartFormOfOwnerships",
                        principalColumn: "PlotPartFormOfOwnershipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotParts_PlotPartProtectedZones_PlotPartProtectedZoneId",
                        column: x => x.PlotPartProtectedZoneId,
                        principalTable: "PlotPartProtectedZones",
                        principalColumn: "PlotPartProtectedZoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotParts_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "PlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlotCadastralMunicipalities",
                columns: new[] { "PlotCadastralMunicipalityId", "CadastralMunicipality" },
                values: new object[,]
                {
                    { new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Čantavir" },
                    { new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), "Bački Vinogradi" },
                    { new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), "Bikovo" },
                    { new Guid("98b39864-1763-49d4-91c7-3d95060ebd5e"), "Đuđin" },
                    { new Guid("f305096b-52fd-4c43-8699-05bc3ee664b7"), "Žedin" },
                    { new Guid("37841f52-2e51-45ea-af4e-bc67b5c5d0e9"), "Tavankut" },
                    { new Guid("372d9458-a560-4b56-8119-ada1f7feb723"), "Bajmok" },
                    { new Guid("321e3608-d760-4067-bfb5-695784bd2dd3"), "Donji Grad" },
                    { new Guid("aee6dace-3f2d-43b5-b853-7d08e20ac81f"), "Stari Grad" },
                    { new Guid("5bffadaf-117e-4d87-9f32-ef39e83d1499"), "Novi Grad" },
                    { new Guid("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"), "Palić" }
                });

            migrationBuilder.InsertData(
                table: "PlotCultures",
                columns: new[] { "PlotCultureId", "Culture" },
                values: new object[,]
                {
                    { new Guid("a0c1727d-bb2c-4243-a907-be6f3a005558"), "Trstici-močvare" },
                    { new Guid("3262a3e8-a113-431f-8f2f-98a10d97c5a4"), "Šume" },
                    { new Guid("cc506ecd-fb9e-48d8-af90-26ecc5d9feba"), "Pašnjaci" },
                    { new Guid("97adad6e-f225-4164-8830-b59004c812c3"), "Livade" },
                    { new Guid("ba9777ce-d43f-4f71-a163-7c974e36654f"), "Njive" },
                    { new Guid("2484a534-4e5f-4b0c-af35-190ae0d68fcc"), "Voćnjaci" },
                    { new Guid("60644cdd-795b-47a2-96ac-55f623862efe"), "Vrtovi" },
                    { new Guid("7b199139-6b41-4087-89b0-84d911b5fe2b"), "Vinogradi" }
                });

            migrationBuilder.InsertData(
                table: "PlotPartClasses",
                columns: new[] { "PlotPartClassId", "Class" },
                values: new object[,]
                {
                    { new Guid("b2ddef8e-eddc-4fb0-884b-1701ab983bed"), "VI" },
                    { new Guid("1965dce3-a24a-4e7c-a6d1-fddbbfeabc44"), "VIII" },
                    { new Guid("a9a4427b-889d-4be4-bf9c-386edb323d9c"), "VII" },
                    { new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), "V" },
                    { new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "I" },
                    { new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "III" },
                    { new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "II" },
                    { new Guid("5e69aeb5-4fec-4dd9-ba69-a474f06721f2"), "IV" }
                });

            migrationBuilder.InsertData(
                table: "PlotPartFormOfOwnerships",
                columns: new[] { "PlotPartFormOfOwnershipId", "FormOfOwnership" },
                values: new object[,]
                {
                    { new Guid("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"), "Privatna svojina" },
                    { new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "Državna svojina RS" },
                    { new Guid("3075f4ce-e8f4-4b68-bd22-246363d71a57"), "Državna svojina" },
                    { new Guid("aa444022-1e63-44f5-8cf4-7df5045af184"), "Društvena svojina" },
                    { new Guid("b8e349da-6c4d-4282-acb1-872628128fc1"), "Zadružna svojina" },
                    { new Guid("07af89f2-feee-4680-b489-9d0e31699588"), "Mešovita svojina" },
                    { new Guid("a2c789e8-9e35-43d6-bf2e-156d776aeceb"), "Drugi oblici" }
                });

            migrationBuilder.InsertData(
                table: "PlotPartProtectedZones",
                columns: new[] { "PlotPartProtectedZoneId", "ProtectedZone" },
                values: new object[,]
                {
                    { new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), "1" },
                    { new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "2" },
                    { new Guid("de569d06-4787-4808-b4f6-0efea24f6b03"), "3" },
                    { new Guid("4debaa6a-1a2f-43e0-bb82-1b7ca1824318"), "4" }
                });

            migrationBuilder.InsertData(
                table: "PlotWorkabilities",
                columns: new[] { "PlotWorkabilityId", "Workability" },
                values: new object[,]
                {
                    { new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"), "Obradivo" },
                    { new Guid("40d2641b-8b85-4625-b01c-a129389a6aad"), "Ostalo" }
                });

            migrationBuilder.InsertData(
                table: "Plots",
                columns: new[] { "PlotId", "PlotCadastralMunicipalityId", "PlotCultureId", "PlotCurrentCulture", "PlotCurrentWorkability", "PlotNumber", "PlotRealEstateListNumber", "PlotSurfaceArea", "PlotWorkabilityId" },
                values: new object[,]
                {
                    { new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), new Guid("ba9777ce-d43f-4f71-a163-7c974e36654f"), "", "", "112", "LN100", 4500, new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), new Guid("60644cdd-795b-47a2-96ac-55f623862efe"), "", "", "146", "LN202", 5600, new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"), new Guid("2484a534-4e5f-4b0c-af35-190ae0d68fcc"), "", "", "5308", "LN550", 3850, new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("5f37ba98-ca19-4c9e-8914-708e38bba8bf"), new Guid("372d9458-a560-4b56-8119-ada1f7feb723"), new Guid("97adad6e-f225-4164-8830-b59004c812c3"), "", "", "97", "LN90", 7602, new Guid("40d2641b-8b85-4625-b01c-a129389a6aad") }
                });

            migrationBuilder.InsertData(
                table: "PlotParts",
                columns: new[] { "PlotPartId", "PlotId", "PlotPartClassId", "PlotPartCurrentClass", "PlotPartCurrentProtectedZone", "PlotPartFormOfOwnershipId", "PlotPartNumber", "PlotPartProtectedZoneId", "PlotPartSurfaceArea" },
                values: new object[,]
                {
                    { new Guid("d14d555d-9637-4244-8ab3-dad55097259b"), new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "", "", new Guid("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"), "112/1", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), 1900 },
                    { new Guid("1f61ce66-fc70-42a5-ae25-6671d294f879"), new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "", "", new Guid("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"), "112/3", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), 2600 },
                    { new Guid("fb974419-6f20-4969-950e-e0f0ccb58593"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/2", new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), 2200 },
                    { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/4", new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), 1600 },
                    { new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/5", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), 2800 },
                    { new Guid("f083e28b-6352-4fda-a172-0d579390e632"), new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("aa444022-1e63-44f5-8cf4-7df5045af184"), "5308/1", new Guid("de569d06-4787-4808-b4f6-0efea24f6b03"), 2700 },
                    { new Guid("861f142c-4707-416d-ad14-7debbd2031ed"), new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("aa444022-1e63-44f5-8cf4-7df5045af184"), "5308/2", new Guid("de569d06-4787-4808-b4f6-0efea24f6b03"), 1150 },
                    { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), new Guid("5f37ba98-ca19-4c9e-8914-708e38bba8bf"), new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), "", "", new Guid("07af89f2-feee-4680-b489-9d0e31699588"), "97", new Guid("4debaa6a-1a2f-43e0-bb82-1b7ca1824318"), 7602 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlotParts_PlotId",
                table: "PlotParts",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotParts_PlotPartClassId",
                table: "PlotParts",
                column: "PlotPartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotParts_PlotPartFormOfOwnershipId",
                table: "PlotParts",
                column: "PlotPartFormOfOwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotParts_PlotPartProtectedZoneId",
                table: "PlotParts",
                column: "PlotPartProtectedZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_PlotCadastralMunicipalityId",
                table: "Plots",
                column: "PlotCadastralMunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_PlotCultureId",
                table: "Plots",
                column: "PlotCultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Plots_PlotWorkabilityId",
                table: "Plots",
                column: "PlotWorkabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotParts");

            migrationBuilder.DropTable(
                name: "PlotPartClasses");

            migrationBuilder.DropTable(
                name: "PlotPartFormOfOwnerships");

            migrationBuilder.DropTable(
                name: "PlotPartProtectedZones");

            migrationBuilder.DropTable(
                name: "Plots");

            migrationBuilder.DropTable(
                name: "PlotCadastralMunicipalities");

            migrationBuilder.DropTable(
                name: "PlotCultures");

            migrationBuilder.DropTable(
                name: "PlotWorkabilities");
        }
    }
}
