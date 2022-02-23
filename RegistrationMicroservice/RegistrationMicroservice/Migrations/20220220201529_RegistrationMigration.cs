using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistrationMicroservice.Migrations
{
    public partial class RegistrationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registration",
                columns: table => new
                {
                    RegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registration", x => x.RegistrationId);
                });

            migrationBuilder.InsertData(
                table: "registration",
                columns: new[] { "RegistrationId", "AuctionId", "BuyerId", "Date", "Location" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a421c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TEst" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registration");
        }
    }
}
