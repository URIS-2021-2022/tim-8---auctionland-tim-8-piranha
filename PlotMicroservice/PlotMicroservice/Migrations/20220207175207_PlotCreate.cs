using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plots",
                columns: table => new
                {
                    PlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotCultureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotCadastralMunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotWorkabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotSurfaceArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "PlotPart",
                columns: table => new
                {
                    PlotPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartProtectedZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartFormOfOwnershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartSurfaceArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotPartCurrentClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlotPartCurrentProtectedZone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlotPart", x => x.PlotPartId);
                    table.ForeignKey(
                        name: "FK_PlotPart_PlotPartClasses_PlotPartClassId",
                        column: x => x.PlotPartClassId,
                        principalTable: "PlotPartClasses",
                        principalColumn: "PlotPartClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotPart_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                        column: x => x.PlotPartFormOfOwnershipId,
                        principalTable: "PlotPartFormOfOwnerships",
                        principalColumn: "PlotPartFormOfOwnershipId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotPart_PlotPartProtectedZones_PlotPartProtectedZoneId",
                        column: x => x.PlotPartProtectedZoneId,
                        principalTable: "PlotPartProtectedZones",
                        principalColumn: "PlotPartProtectedZoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlotPart_Plots_PlotId",
                        column: x => x.PlotId,
                        principalTable: "Plots",
                        principalColumn: "PlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Plots",
                columns: new[] { "PlotId", "PlotCadastralMunicipalityId", "PlotCultureId", "PlotCurrentCulture", "PlotCurrentWorkability", "PlotNumber", "PlotRealEstateListNumber", "PlotSurfaceArea", "PlotWorkabilityId" },
                values: new object[,]
                {
                    { new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), new Guid("ba9777ce-d43f-4f71-a163-7c974e36654f"), "", "", "112", "LN100", "4500 m2", new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), new Guid("60644cdd-795b-47a2-96ac-55f623862efe"), "", "", "146", "LN202", "5600 m2", new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"), new Guid("2484a534-4e5f-4b0c-af35-190ae0d68fcc"), "", "", "5308", "LN550", "3850 m2", new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b") },
                    { new Guid("5f37ba98-ca19-4c9e-8914-708e38bba8bf"), new Guid("372d9458-a560-4b56-8119-ada1f7feb723"), new Guid("97adad6e-f225-4164-8830-b59004c812c3"), "", "", "97", "LN90", "7602 m2", new Guid("40d2641b-8b85-4625-b01c-a129389a6aad") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlotPart_PlotId",
                table: "PlotPart",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotPart_PlotPartClassId",
                table: "PlotPart",
                column: "PlotPartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotPart_PlotPartFormOfOwnershipId",
                table: "PlotPart",
                column: "PlotPartFormOfOwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_PlotPart_PlotPartProtectedZoneId",
                table: "PlotPart",
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
                name: "PlotPart");

            migrationBuilder.DropTable(
                name: "Plots");
        }
    }
}
