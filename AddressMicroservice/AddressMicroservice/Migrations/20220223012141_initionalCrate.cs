using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AddressMicroservice.Migrations
{
    public partial class initionalCrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_State_StateID",
                        column: x => x.StateID,
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "StateID", "NameState" },
                values: new object[] { new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Hrvatska" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "StateID", "NameState" },
                values: new object[] { new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), "Srbija" });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "StateID", "NameState" },
                values: new object[] { new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), "Makedonija" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Place", "StateID", "Street", "StreetNumber", "ZipCode" },
                values: new object[] { new Guid("07af89f2-feee-4680-b489-9d0e31699588"), "Zagreb", new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Jadranska avenija", "23b", "10000" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Place", "StateID", "Street", "StreetNumber", "ZipCode" },
                values: new object[] { new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), "Pancevo", new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), "Svetog Save", "5", "26000" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Place", "StateID", "Street", "StreetNumber", "ZipCode" },
                values: new object[] { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), "Skoplje", new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), "Drezdenska", "10", "1010" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_StateID",
                table: "Address",
                column: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
