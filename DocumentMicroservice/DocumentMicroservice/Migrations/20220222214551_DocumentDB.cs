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
                    docStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.docStatusID);
                });

            migrationBuilder.CreateTable(
                name: "GuaranteeTypes",
                columns: table => new
                {
                    guaranteeTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranteeTypes", x => x.guaranteeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    documentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    registrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documentCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    documentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    documentTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    docStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documentStatusdocStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    auctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.documentId);
                    table.ForeignKey(
                        name: "FK_Document_DocumentStatus_documentStatusdocStatusID",
                        column: x => x.documentStatusdocStatusID,
                        principalTable: "DocumentStatus",
                        principalColumn: "docStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contractLease",
                columns: table => new
                {
                    contractLeaseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    submissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deadlineLandRestitution = table.Column<DateTime>(type: "datetime2", nullable: true),
                    placeOfSigning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfSigning = table.Column<DateTime>(type: "datetime2", nullable: false),
                    guaranteeTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    personId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractLease", x => x.contractLeaseID);
                    table.ForeignKey(
                        name: "FK_contractLease_Document_documentId",
                        column: x => x.documentId,
                        principalTable: "Document",
                        principalColumn: "documentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contractLease_GuaranteeTypes_guaranteeTypeID",
                        column: x => x.guaranteeTypeID,
                        principalTable: "GuaranteeTypes",
                        principalColumn: "guaranteeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "documentId", "auctionId", "docStatusID", "documentCreationDate", "documentDate", "documentStatusdocStatusID", "documentTemplate", "registrationNumber", "userId" },
                values: new object[,]
                {
                    { new Guid("07af89f2-feee-4680-b489-9d0e31699588"), null, new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Kreiranje predloga plana", "119833332", null },
                    { new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), null, new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Obrazovanje komisije za davanje misljenja", "122267432", null },
                    { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), null, new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Saglasnost ministra", "119834232", null }
                });

            migrationBuilder.InsertData(
                table: "DocumentStatus",
                columns: new[] { "docStatusID", "status" },
                values: new object[,]
                {
                    { new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Usvojen" },
                    { new Guid("458adb42-62a5-4117-8101-7d933fa88abb"), "Odbijen" },
                    { new Guid("84ff030b-7067-45b7-8bb2-10719534f91e"), "Otvoren" }
                });

            migrationBuilder.InsertData(
                table: "GuaranteeTypes",
                columns: new[] { "guaranteeTypeID", "type" },
                values: new object[,]
                {
                    { new Guid("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"), "Jemstvo" },
                    { new Guid("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"), "Bankarska garancija" },
                    { new Guid("372d9458-a560-4b56-8119-ada1f7feb723"), "Garancija nekretnine" },
                    { new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "Zirantska" },
                    { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), "Uplata gotovine" }
                });

            migrationBuilder.InsertData(
                table: "contractLease",
                columns: new[] { "contractLeaseID", "buyerId", "dateOfSigning", "deadlineLandRestitution", "documentId", "guaranteeTypeID", "personId", "placeOfSigning", "serialNumber", "submissionDate" },
                values: new object[] { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), null, "Zrenjanin", "12345", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_contractLease_documentId",
                table: "contractLease",
                column: "documentId");

            migrationBuilder.CreateIndex(
                name: "IX_contractLease_guaranteeTypeID",
                table: "contractLease",
                column: "guaranteeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_documentStatusdocStatusID",
                table: "Document",
                column: "documentStatusdocStatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractLease");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "GuaranteeTypes");

            migrationBuilder.DropTable(
                name: "DocumentStatus");
        }
    }
}
