using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicBidding.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "PublicBiddings",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPricePerHa = table.Column<double>(type: "float", nullable: false),
                    IsExcepted = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BestBidder = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentPeriod = table.Column<int>(type: "int", nullable: false),
                    NumberOfApplicants = table.Column<int>(type: "int", nullable: false),
                    DepositSupplement = table.Column<double>(type: "float", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBiddings", x => x.PublicBiddingId);
                    table.ForeignKey(
                        name: "FK_PublicBiddings_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicBiddings_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicBiddingAuthorizedPerson",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorizedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBiddingAuthorizedPerson", x => new { x.PublicBiddingId, x.AuthorizedPersonId });
                    table.ForeignKey(
                        name: "FK_PublicBiddingAuthorizedPerson_PublicBiddings_PublicBiddingId",
                        column: x => x.PublicBiddingId,
                        principalTable: "PublicBiddings",
                        principalColumn: "PublicBiddingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicBiddingBuyer",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBiddingBuyer", x => new { x.PublicBiddingId, x.BuyerId });
                    table.ForeignKey(
                        name: "FK_PublicBiddingBuyer_PublicBiddings_PublicBiddingId",
                        column: x => x.PublicBiddingId,
                        principalTable: "PublicBiddings",
                        principalColumn: "PublicBiddingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicBiddingPlotPart",
                columns: table => new
                {
                    PublicBiddingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlotPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBiddingPlotPart", x => new { x.PublicBiddingId, x.PlotPartId });
                    table.ForeignKey(
                        name: "FK_PublicBiddingPlotPart_PublicBiddings_PublicBiddingId",
                        column: x => x.PublicBiddingId,
                        principalTable: "PublicBiddings",
                        principalColumn: "PublicBiddingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { new Guid("2233cbba-607a-4182-9f83-7ff8ffe6e5ac"), "Prvi krug" },
                    { new Guid("770a32d4-1db9-4844-868e-6bf8171ffc20"), "Drugi krug sa novim uslovima" },
                    { new Guid("28273376-994b-461d-8097-d03654c5268d"), "Drugi krug sa starim uslovima" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "TypeName" },
                values: new object[,]
                {
                    { new Guid("8010f254-e872-49d9-9c2c-1d5783719019"), "Javna licitacija" },
                    { new Guid("9b926999-151c-458c-8ae8-3d4a7e9f6459"), "Otvaranje zatvorenih ponuda" }
                });

            migrationBuilder.InsertData(
                table: "PublicBiddings",
                columns: new[] { "PublicBiddingId", "AddressId", "BestBidder", "Date", "DepositSupplement", "EndTime", "IsExcepted", "NumberOfApplicants", "Price", "RentPeriod", "Round", "StartPricePerHa", "StartTime", "StatusId", "TypeId" },
                values: new object[] { new Guid("d7d314b0-2f22-4af5-8909-238b23383249"), new Guid("01f759bd-fb38-49f5-a4a7-f8a938fbd541"), null, new DateTime(2018, 12, 10, 1, 0, 0, 0, DateTimeKind.Local), 120.5, new DateTime(2018, 12, 10, 16, 45, 0, 0, DateTimeKind.Local), false, 1, 600.5, 2, 4, 500.35000000000002, new DateTime(2018, 12, 10, 14, 45, 0, 0, DateTimeKind.Local), new Guid("2233cbba-607a-4182-9f83-7ff8ffe6e5ac"), new Guid("8010f254-e872-49d9-9c2c-1d5783719019") });

            migrationBuilder.InsertData(
                table: "PublicBiddings",
                columns: new[] { "PublicBiddingId", "AddressId", "BestBidder", "Date", "DepositSupplement", "EndTime", "IsExcepted", "NumberOfApplicants", "Price", "RentPeriod", "Round", "StartPricePerHa", "StartTime", "StatusId", "TypeId" },
                values: new object[] { new Guid("62c28c9a-7306-45c7-a5b3-1603eed4fd5a"), new Guid("50394b74-3ed0-4364-a8f2-aeb0bcb783ef"), null, new DateTime(2018, 8, 9, 2, 0, 0, 0, DateTimeKind.Local), 200.19999999999999, new DateTime(2018, 8, 9, 19, 45, 0, 0, DateTimeKind.Local), false, 4, 1800.4000000000001, 4, 2, 1200.5999999999999, new DateTime(2018, 8, 9, 17, 45, 0, 0, DateTimeKind.Local), new Guid("770a32d4-1db9-4844-868e-6bf8171ffc20"), new Guid("9b926999-151c-458c-8ae8-3d4a7e9f6459") });

            migrationBuilder.CreateIndex(
                name: "IX_PublicBiddings_StatusId",
                table: "PublicBiddings",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicBiddings_TypeId",
                table: "PublicBiddings",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicBiddingAuthorizedPerson");

            migrationBuilder.DropTable(
                name: "PublicBiddingBuyer");

            migrationBuilder.DropTable(
                name: "PublicBiddingPlotPart");

            migrationBuilder.DropTable(
                name: "PublicBiddings");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
