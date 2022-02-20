using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentMicroservice.Migrations
{
    public partial class DocumentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentStatus",
                columns: table => new
                {
                    DocStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.DocStatusID);
                });

            migrationBuilder.CreateTable(
                name: "GuaranteeTypes",
                columns: table => new
                {
                    GuaranteeTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranteeTypes", x => x.GuaranteeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Document_DocumentStatus_DocStatusID",
                        column: x => x.DocStatusID,
                        principalTable: "DocumentStatus",
                        principalColumn: "DocStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DocumentStatus",
                columns: new[] { "DocStatusID", "Status" },
                values: new object[,]
                {
                    { new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Usvojen" },
                    { new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), "Odbijen" },
                    { new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), "Otvoren" }
                });

            migrationBuilder.InsertData(
                table: "GuaranteeTypes",
                columns: new[] { "GuaranteeTypeID", "Type" },
                values: new object[,]
                {
                    { new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "Jemstvo" },
                    { new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"), "Bankarska garancija" },
                    { new Guid("372d9458-a560-4b56-8119-ada1f7feb723"), "Garancija nekretnine" },
                    { new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "Zirantska" },
                    { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), "Uplata gotovine" }
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "DocumentId", "DocStatusID", "DocumentCreationDate", "DocumentDate", "DocumentTemplate", "RegistrationNumber" },
                values: new object[] { new Guid("07af89f2-feee-4680-b489-9d0e31699588"), new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kreiranje predloga plana", "119833332" });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "DocumentId", "DocStatusID", "DocumentCreationDate", "DocumentDate", "DocumentTemplate", "RegistrationNumber" },
                values: new object[] { new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Obrazovanje komisije za davanje misljenja", "122267432" });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "DocumentId", "DocStatusID", "DocumentCreationDate", "DocumentDate", "DocumentTemplate", "RegistrationNumber" },
                values: new object[] { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saglasnost ministra", "119834232" });

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocStatusID",
                table: "Document",
                column: "DocStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "GuaranteeTypes");

            migrationBuilder.DropTable(
                name: "DocumentStatus");
        }
    }
}
