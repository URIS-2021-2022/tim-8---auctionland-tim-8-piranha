using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentMicroservice.Migrations
{
    public partial class DocumentDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "plotId",
                table: "contractLease",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "plotId",
                table: "contractLease");
        }
    }
}
