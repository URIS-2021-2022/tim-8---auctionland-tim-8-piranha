using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplaintMicroservice.Migrations
{
    public partial class ComplaintMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintEvent",
                columns: table => new
                {
                    ComplaintEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintEvent", x => x.ComplaintEventId);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintStatus",
                columns: table => new
                {
                    ComplaintStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatus", x => x.ComplaintStatusId);
                });

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

            migrationBuilder.CreateTable(
                name: "Complaint",
                columns: table => new
                {
                    ComplaintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolutionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplaintTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComplaintEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.ComplaintId);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintEvent_ComplaintEventId",
                        column: x => x.ComplaintEventId,
                        principalTable: "ComplaintEvent",
                        principalColumn: "ComplaintEventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintStatus_ComplaintStatusId",
                        column: x => x.ComplaintStatusId,
                        principalTable: "ComplaintStatus",
                        principalColumn: "ComplaintStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Complaint_ComplaintTypes_ComplaintTypeId",
                        column: x => x.ComplaintTypeId,
                        principalTable: "ComplaintTypes",
                        principalColumn: "ComplaintTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComplaintEvent",
                columns: new[] { "ComplaintEventId", "Event" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "JN ide u drugi krug sa novim uslovima" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "JN ide u drugi krug sa starim uslovima" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintStatus",
                columns: new[] { "ComplaintStatusId", "Status" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Usvojena" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Odbijena" }
                });

            migrationBuilder.InsertData(
                table: "ComplaintTypes",
                columns: new[] { "ComplaintTypeId", "ComplaintType" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "Zalba na tok javnog nadmetanja" },
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "Zalba na Odluku o davanju u zakup" }
                });

            migrationBuilder.InsertData(
                table: "Complaint",
                columns: new[] { "ComplaintId", "BuyerId", "ComplaintEventId", "ComplaintStatusId", "ComplaintTypeId", "DecisionNumber", "Explanation", "PublicBiddingId", "Reason", "SolutionNumber", "SubmissionDate" },
                values: new object[] { new Guid("eb6bac2d-aea4-485a-8cb6-991bf8b1e1d4"), new Guid("861f142c-4707-416d-ad14-7debbd2031ed"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "DN001", "Complaint explanation", new Guid("d7d314b0-2f22-4af5-8909-238b23383249"), "Complaint reason", "SN001", new DateTime(2021, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Complaint",
                columns: new[] { "ComplaintId", "BuyerId", "ComplaintEventId", "ComplaintStatusId", "ComplaintTypeId", "DecisionNumber", "Explanation", "PublicBiddingId", "Reason", "SolutionNumber", "SubmissionDate" },
                values: new object[] { new Guid("b16abef5-5d4e-43a5-9bf3-1fe0618da6f7"), new Guid("861f142c-4707-416d-ad14-7debbd2031ed"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "DN002", "Complaint explanation 2", new Guid("d7d314b0-2f22-4af5-8909-238b23383249"), "Complaint reason 2", "SN002", new DateTime(2021, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ComplaintEventId",
                table: "Complaint",
                column: "ComplaintEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ComplaintStatusId",
                table: "Complaint",
                column: "ComplaintStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_ComplaintTypeId",
                table: "Complaint",
                column: "ComplaintTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Complaint");

            migrationBuilder.DropTable(
                name: "ComplaintEvent");

            migrationBuilder.DropTable(
                name: "ComplaintStatus");

            migrationBuilder.DropTable(
                name: "ComplaintTypes");
        }
    }
}
