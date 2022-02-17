using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistrationMicroservice.Migrations
{
    public partial class RegistrationMigration : Migration
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
                name: "buyer",
                columns: table => new
                {
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoughtSurface = table.Column<int>(type: "int", nullable: false),
                    RestrictionStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestrictionPeriodInYears = table.Column<int>(type: "int", nullable: false),
                    RestrictionEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyer", x => x.BuyerId);
                });

            migrationBuilder.CreateTable(
                name: "registration",
                columns: table => new
                {
                    RegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registration", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_registration_auction_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "auction",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_registration_buyer_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "buyer",
                        principalColumn: "BuyerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "auction",
                columns: new[] { "AuctionId", "ApplicationDeadline", "AuctionNum", "Date", "PriceStep", "Restriction", "Year" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 25, 2022 });

            migrationBuilder.InsertData(
                table: "buyer",
                columns: new[] { "BuyerId", "BoughtSurface", "RestrictionEnd", "RestrictionPeriodInYears", "RestrictionStart" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"), 23, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "registration",
                columns: new[] { "RegistrationId", "AuctionId", "BuyerId", "Date", "Location" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TEst" });

            migrationBuilder.CreateIndex(
                name: "IX_registration_AuctionId",
                table: "registration",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_registration_BuyerId",
                table: "registration",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registration");

            migrationBuilder.DropTable(
                name: "auction");

            migrationBuilder.DropTable(
                name: "buyer");
        }
    }
}
