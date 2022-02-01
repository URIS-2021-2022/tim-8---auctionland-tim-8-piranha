using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionMicroservice.Migrations
{
    public partial class DocumentationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documentationIndividuals",
                columns: table => new
                {
                    DocumentationIndividualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentationIndividuals", x => x.DocumentationIndividualId);
                });

            migrationBuilder.InsertData(
                table: "documentationIndividuals",
                columns: new[] { "DocumentationIndividualId", "FirstName", "IdentificationNumber", "Surname" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Marko", "0819423841941", "Milic" });

            migrationBuilder.InsertData(
                table: "documentationIndividuals",
                columns: new[] { "DocumentationIndividualId", "FirstName", "IdentificationNumber", "Surname" },
                values: new object[] { new Guid("6a411c17-a195-48f7-8dbd-67596c3974c0"), "Stefan", "0214120948120", "Zoric" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentationIndividuals");
        }
    }
}
