using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotWorkabilityCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "PlotWorkabilities",
                columns: new[] { "PlotWorkabilityId", "Workability" },
                values: new object[] { new Guid("c0615a4d-faa4-4e17-8f2f-93ec25383f9b"), "Obradivo" });

            migrationBuilder.InsertData(
                table: "PlotWorkabilities",
                columns: new[] { "PlotWorkabilityId", "Workability" },
                values: new object[] { new Guid("40d2641b-8b85-4625-b01c-a129389a6aad"), "Ostalo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotWorkabilities");
        }
    }
}
