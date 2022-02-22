using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyerMicroservice.Migrations
{
    public partial class BuyerDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "boardNumber",
                columns: new[] { "boardNumberID", "number" },
                values: new object[] { new Guid("8d951bd9-497a-47ec-b1a7-c944492f4c8c"), 5 });

            migrationBuilder.InsertData(
                table: "boardNumber",
                columns: new[] { "boardNumberID", "number" },
                values: new object[] { new Guid("2018f35a-f49b-462f-a1c9-a105f297864b"), 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "boardNumber",
                keyColumn: "boardNumberID",
                keyValue: new Guid("2018f35a-f49b-462f-a1c9-a105f297864b"));

            migrationBuilder.DeleteData(
                table: "boardNumber",
                keyColumn: "boardNumberID",
                keyValue: new Guid("8d951bd9-497a-47ec-b1a7-c944492f4c8c"));
        }
    }
}
