using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionMicroservice.Migrations
{
    public partial class AuctionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auction",
                columns: table => new
                {
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionNum = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Restriction = table.Column<int>(type: "int", nullable: false),
                    PriceStep = table.Column<int>(type: "int", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auction", x => x.AuctionId);
                });

            migrationBuilder.CreateTable(
                name: "auctionPublicBidding",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auctionPublicBidding", x => x.PublicBiddingId);
                    table.ForeignKey(
                        name: "FK_auctionPublicBidding_auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "auction",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documentationIndividual",
                columns: table => new
                {
                    DocumentationIndividualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentationIndividual", x => x.DocumentationIndividualId);
                    table.ForeignKey(
                        name: "FK_documentationIndividual_auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "auction",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documentationLegalEntity",
                columns: table => new
                {
                    DocumentationLegalEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentationLegalEntity", x => x.DocumentationLegalEntityId);
                    table.ForeignKey(
                        name: "FK_documentationLegalEntity_auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "auction",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "auction",
                columns: new[] { "AuctionId", "ApplicationDeadline", "AuctionNum", "Date", "PriceStep", "Restriction", "Year" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 25, 2022 });

            migrationBuilder.InsertData(
                table: "documentationIndividual",
                columns: new[] { "DocumentationIndividualId", "AuctionId", "FirstName", "IdentificationNumber", "Surname" },
                values: new object[] { new Guid("6a411a17-a195-48f7-8dbd-67596c3974c0"), new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), "Stefan", "0214120948120", "Zoric" });

            migrationBuilder.InsertData(
                table: "documentationLegalEntity",
                columns: new[] { "DocumentationLegalEntityId", "Address", "AuctionId", "IdentificationNumber", "Name" },
                values: new object[] { new Guid("6a411c13-a295-48f7-8dbd-67596c3974c0"), "Uzun mirkova 8", new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), "17", "Goran" });

            migrationBuilder.CreateIndex(
                name: "IX_auctionPublicBidding_AuctionId",
                table: "auctionPublicBidding",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_documentationIndividual_AuctionId",
                table: "documentationIndividual",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_documentationLegalEntity_AuctionId",
                table: "documentationLegalEntity",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auctionPublicBidding");

            migrationBuilder.DropTable(
                name: "documentationIndividual");

            migrationBuilder.DropTable(
                name: "documentationLegalEntity");

            migrationBuilder.DropTable(
                name: "auction");
        }
    }
}
