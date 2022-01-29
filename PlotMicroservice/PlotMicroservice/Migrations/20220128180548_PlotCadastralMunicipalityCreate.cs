using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotCadastralMunicipalityCreate : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotCadastralMunicipalities");
        }
    }
}
