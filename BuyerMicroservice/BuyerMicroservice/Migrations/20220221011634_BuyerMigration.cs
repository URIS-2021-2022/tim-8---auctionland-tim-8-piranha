using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyerMicroservice.Migrations
{
    public partial class BuyerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIndividual",
                table: "buyer");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "buyer",
                newName: "BuyerType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerType",
                table: "buyer",
                newName: "Discriminator");

            migrationBuilder.AddColumn<bool>(
                name: "IsIndividual",
                table: "buyer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "buyer",
                keyColumn: "buyerID",
                keyValue: new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                column: "IsIndividual",
                value: true);
        }
    }
}
