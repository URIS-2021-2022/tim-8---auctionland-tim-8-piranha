using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.BoardId);
                    table.ForeignKey(
                        name: "FK_Boards_Persons_PresidentId",
                        column: x => x.PresidentId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardPerson",
                columns: table => new
                {
                    BoardsBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembersPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPerson", x => new { x.BoardsBoardId, x.MembersPersonId });
                    table.ForeignKey(
                        name: "FK_BoardPerson_Boards_BoardsBoardId",
                        column: x => x.BoardsBoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardPerson_Persons_MembersPersonId",
                        column: x => x.MembersPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Function", "Name", "Surname" },
                values: new object[] { new Guid("2d8607c5-f3cf-4ef5-9323-a9318eee6232"), "President", "Davor", "Jelic" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Function", "Name", "Surname" },
                values: new object[] { new Guid("2411cc63-1a91-4bb2-9432-c2f0515cef63"), "Judge", "Milan", "Novcic" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Function", "Name", "Surname" },
                values: new object[] { new Guid("81f63012-16d7-4f1a-a330-55dc295a6dcd"), "Member", "Mihajlo", "Strajin" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "BoardId", "PresidentId" },
                values: new object[] { new Guid("8010f254-e872-49d9-9c2c-1d5783719019"), new Guid("2d8607c5-f3cf-4ef5-9323-a9318eee6232") });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "BoardId", "PresidentId" },
                values: new object[] { new Guid("e53171cc-91f1-4716-8ea8-39b31a97dd84"), new Guid("2d8607c5-f3cf-4ef5-9323-a9318eee6232") });

            migrationBuilder.CreateIndex(
                name: "IX_BoardPerson_MembersPersonId",
                table: "BoardPerson",
                column: "MembersPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_PresidentId",
                table: "Boards",
                column: "PresidentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardPerson");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
