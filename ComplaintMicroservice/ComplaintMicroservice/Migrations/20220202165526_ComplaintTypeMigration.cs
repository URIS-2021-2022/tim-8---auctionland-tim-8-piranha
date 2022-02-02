using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintMicroservice.Migrations
{
    public partial class ComplaintTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintTypes",
                columns: table => new
                {
                    ComplaintTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintTypes", x => x.ComplaintTypeId);
                });

            migrationBuilder.InsertData(
                table: "ComplaintTypes",
                columns: new[] { "ComplaintTypeId", "ComplaintType" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Zalba na tok javnog nadmetanja" });

            migrationBuilder.InsertData(
                table: "ComplaintTypes",
                columns: new[] { "ComplaintTypeId", "ComplaintType" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Zalba na Odluku o davanju u zakup" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintTypes");
        }
    }
}
