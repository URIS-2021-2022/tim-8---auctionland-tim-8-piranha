using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotPartCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlotPart",
                columns: new[] { "PlotPartId", "PlotId", "PlotPartClassId", "PlotPartCurrentClass", "PlotPartCurrentProtectedZone", "PlotPartFormOfOwnershipId", "PlotPartNumber", "PlotPartProtectedZoneId", "PlotPartSurfaceArea" },
                values: new object[,]
                {
                    { new Guid("d14d555d-9637-4244-8ab3-dad55097259b"), new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "", "", new Guid("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"), "112/1", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), "1900 m2" },
                    { new Guid("1f61ce66-fc70-42a5-ae25-6671d294f879"), new Guid("b281612e-8013-40cc-b9ce-f9d063295420"), new Guid("1794fc01-2d12-4f5d-aaec-7eb219635052"), "", "", new Guid("06d92fec-8bd5-4be1-a772-f52ae7ff6ee3"), "112/3", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), "2600 m2" },
                    { new Guid("fb974419-6f20-4969-950e-e0f0ccb58593"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/2", new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "2200 m2" },
                    { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("5b957c06-8ca6-4658-ad45-78e62c465b3d"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/4", new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "1600 m2" },
                    { new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"), new Guid("c6ea356d-c1c1-4374-985b-f8f91d35daa1"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "146/5", new Guid("f66b8360-33d2-48e9-9be5-b208988d1fb1"), "2800 m2" },
                    { new Guid("f083e28b-6352-4fda-a172-0d579390e632"), new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("aa444022-1e63-44f5-8cf4-7df5045af184"), "5308/1", new Guid("de569d06-4787-4808-b4f6-0efea24f6b03"), "2700 m2" },
                    { new Guid("861f142c-4707-416d-ad14-7debbd2031ed"), new Guid("226480a5-74db-4507-958a-8963c4a36716"), new Guid("6f2629db-8de7-496c-97e0-75b1a94b1db3"), "", "", new Guid("aa444022-1e63-44f5-8cf4-7df5045af184"), "5308/2", new Guid("de569d06-4787-4808-b4f6-0efea24f6b03"), "1150 m2" },
                    { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), new Guid("5f37ba98-ca19-4c9e-8914-708e38bba8bf"), new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), "", "", new Guid("07af89f2-feee-4680-b489-9d0e31699588"), "97", new Guid("4debaa6a-1a2f-43e0-bb82-1b7ca1824318"), "7602 m2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("1f61ce66-fc70-42a5-ae25-6671d294f879"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("861f142c-4707-416d-ad14-7debbd2031ed"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("d14d555d-9637-4244-8ab3-dad55097259b"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("f083e28b-6352-4fda-a172-0d579390e632"));

            migrationBuilder.DeleteData(
                table: "PlotPart",
                keyColumn: "PlotPartId",
                keyValue: new Guid("fb974419-6f20-4969-950e-e0f0ccb58593"));
        }
    }
}
