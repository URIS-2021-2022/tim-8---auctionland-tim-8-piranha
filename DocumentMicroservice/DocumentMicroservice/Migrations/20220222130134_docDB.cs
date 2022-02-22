using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentMicroservice.Migrations
{
    public partial class docDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentStatus_DocStatusID",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_DocStatusID",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "GuaranteeTypes",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "GuaranteeTypeID",
                table: "GuaranteeTypes",
                newName: "guaranteeTypeID");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "DocumentStatus",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "DocStatusID",
                table: "DocumentStatus",
                newName: "docStatusID");

            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "Document",
                newName: "registrationNumber");

            migrationBuilder.RenameColumn(
                name: "DocumentTemplate",
                table: "Document",
                newName: "documentTemplate");

            migrationBuilder.RenameColumn(
                name: "DocumentDate",
                table: "Document",
                newName: "documentDate");

            migrationBuilder.RenameColumn(
                name: "DocumentCreationDate",
                table: "Document",
                newName: "documentCreationDate");

            migrationBuilder.RenameColumn(
                name: "DocStatusID",
                table: "Document",
                newName: "docStatusID");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Document",
                newName: "documentId");

            migrationBuilder.AddColumn<Guid>(
                name: "documentStatusdocStatusID",
                table: "Document",
                type: "uniqueidentifier",
                nullable: true);

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
                    documentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                table: "contractLease",
                columns: new[] { "contractLeaseID", "dateOfSigning", "deadlineLandRestitution", "documentId", "guaranteeTypeID", "placeOfSigning", "serialNumber", "submissionDate" },
                values: new object[] { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3a3e6366-3a20-4d3b-ae15-be85ba277683"), new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), "Zrenjanin", "12345", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Document_documentStatusdocStatusID",
                table: "Document",
                column: "documentStatusdocStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_contractLease_documentId",
                table: "contractLease",
                column: "documentId");

            migrationBuilder.CreateIndex(
                name: "IX_contractLease_guaranteeTypeID",
                table: "contractLease",
                column: "guaranteeTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentStatus_documentStatusdocStatusID",
                table: "Document",
                column: "documentStatusdocStatusID",
                principalTable: "DocumentStatus",
                principalColumn: "docStatusID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentStatus_documentStatusdocStatusID",
                table: "Document");

            migrationBuilder.DropTable(
                name: "contractLease");

            migrationBuilder.DropIndex(
                name: "IX_Document_documentStatusdocStatusID",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "documentStatusdocStatusID",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "GuaranteeTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "guaranteeTypeID",
                table: "GuaranteeTypes",
                newName: "GuaranteeTypeID");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "DocumentStatus",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "docStatusID",
                table: "DocumentStatus",
                newName: "DocStatusID");

            migrationBuilder.RenameColumn(
                name: "registrationNumber",
                table: "Document",
                newName: "RegistrationNumber");

            migrationBuilder.RenameColumn(
                name: "documentTemplate",
                table: "Document",
                newName: "DocumentTemplate");

            migrationBuilder.RenameColumn(
                name: "documentDate",
                table: "Document",
                newName: "DocumentDate");

            migrationBuilder.RenameColumn(
                name: "documentCreationDate",
                table: "Document",
                newName: "DocumentCreationDate");

            migrationBuilder.RenameColumn(
                name: "docStatusID",
                table: "Document",
                newName: "DocStatusID");

            migrationBuilder.RenameColumn(
                name: "documentId",
                table: "Document",
                newName: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocStatusID",
                table: "Document",
                column: "DocStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentStatus_DocStatusID",
                table: "Document",
                column: "DocStatusID",
                principalTable: "DocumentStatus",
                principalColumn: "DocStatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
