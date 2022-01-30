using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotCultureCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "PlotCultures",
                columns: new[] { "PlotCultureId", "Culture" },
                values: new object[,]
                {
                    { new Guid("ba9777ce-d43f-4f71-a163-7c974e36654f"), "Njive" },
                    { new Guid("60644cdd-795b-47a2-96ac-55f623862efe"), "Vrtovi" },
                    { new Guid("2484a534-4e5f-4b0c-af35-190ae0d68fcc"), "Voćnjaci" },
                    { new Guid("7b199139-6b41-4087-89b0-84d911b5fe2b"), "Vinogradi" },
                    { new Guid("97adad6e-f225-4164-8830-b59004c812c3"), "Livade" },
                    { new Guid("cc506ecd-fb9e-48d8-af90-26ecc5d9feba"), "Pašnjaci" },
                    { new Guid("3262a3e8-a113-431f-8f2f-98a10d97c5a4"), "Šume" },
                    { new Guid("a0c1727d-bb2c-4243-a907-be6f3a005558"), "Trstici-močvare" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotCultures");
        }
    }
}
