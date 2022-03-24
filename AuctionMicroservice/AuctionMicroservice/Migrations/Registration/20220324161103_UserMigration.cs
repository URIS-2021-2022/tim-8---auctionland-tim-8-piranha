using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionMicroservice.Migrations.Registration
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registration",
                columns: table => new
                {
                    RegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registration", x => x.RegistrationId);
                });

            migrationBuilder.InsertData(
                table: "registration",
                columns: new[] { "RegistrationId", "Email", "FirstName", "Password", "Surname" },
                values: new object[] { new Guid("6a421c13-a195-48f7-8dbd-67596c3974c1"), "lukap181@gmail.com", "Milan", "12345", "Miki" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "registration");
        }
    }
}
