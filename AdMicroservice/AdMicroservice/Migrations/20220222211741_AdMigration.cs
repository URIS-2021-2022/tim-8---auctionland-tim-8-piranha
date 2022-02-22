using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdMicroservice.Migrations
{
    public partial class AdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.JournalId);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_Ads_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Journals",
                columns: new[] { "JournalId", "DateOfIssue", "JournalNumber", "Municipality" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "12.02.2021.", "J001", "Ruma" });

            migrationBuilder.InsertData(
                table: "Journals",
                columns: new[] { "JournalId", "DateOfIssue", "JournalNumber", "Municipality" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "22.03.2021.", "J002", "Sremska Mitrovica" });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "AdId", "JournalId", "PublicBiddingId", "PublicationDate" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("d7d314b0-2f22-4af5-8909-238b23383249"), "01.06.2020." });

            migrationBuilder.InsertData(
                table: "Ads",
                columns: new[] { "AdId", "JournalId", "PublicBiddingId", "PublicationDate" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new Guid("d7d314b0-2f22-4af5-8909-238b23383249"), "01.06.2020." });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_JournalId",
                table: "Ads",
                column: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Journals");
        }
    }
}
