using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotPartFormOfOwnershipCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlotPartFormOfOwnerships");
        }
    }
}
