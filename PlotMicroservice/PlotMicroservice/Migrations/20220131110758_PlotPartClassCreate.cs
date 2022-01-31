using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotPartClassCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "PlotPartClasses",
                columns: new[] { "PlotPartClassId", "Class" },
                values: new object[,]
                {
                    { new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "I" },
                    { new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "II" },
                    { new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "III" },
                    { new Guid("5e69aeb5-4fec-4dd9-ba69-a474f06721f2"), "IV" },
                    { new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), "V" },
                    { new Guid("b2ddef8e-eddc-4fb0-884b-1701ab983bed"), "VI" },
                    { new Guid("a9a4427b-889d-4be4-bf9c-386edb323d9c"), "VII" },
                    { new Guid("1965dce3-a24a-4e7c-a6d1-fddbbfeabc44"), "VIII" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotPartClasses");
        }
    }
}
