using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyerMicroservice.Migrations
{
    public partial class BuyerM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardNumber",
                columns: table => new
                {
                    authorizedPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardNumber", x => new { x.authorizedPersonID, x.Id });
                    table.ForeignKey(
                        name: "FK_BoardNumber_authorizedPerson_authorizedPersonID",
                        column: x => x.authorizedPersonID,
                        principalTable: "authorizedPerson",
                        principalColumn: "authorizedPersonID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardNumber");
        }
    }
}
