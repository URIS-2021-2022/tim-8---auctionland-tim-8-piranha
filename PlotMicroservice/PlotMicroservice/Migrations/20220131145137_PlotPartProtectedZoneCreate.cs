using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotPartProtectedZoneCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotPartProtectedZones");
        }
    }
}
