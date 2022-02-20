using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyerMicroservice.Migrations
{
    public partial class BuyerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorizedPerson",
                columns: table => new
                {
                    authorizedPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personalDocNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorizedPerson", x => x.authorizedPersonID);
                });

            migrationBuilder.CreateTable(
                name: "contactPerson",
                columns: table => new
                {
                    contactPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactPerson", x => x.contactPersonID);
                });

            migrationBuilder.CreateTable(
                name: "priority",
                columns: table => new
                {
                    priorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priorityType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority", x => x.priorityID);
                });

            migrationBuilder.CreateTable(
                name: "buyer",
                columns: table => new
                {
                    buyerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsIndividual = table.Column<bool>(type: "bit", nullable: false),
                    realizedArea = table.Column<int>(type: "int", nullable: false),
                    authorizedPersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hasBan = table.Column<bool>(type: "bit", nullable: false),
                    startDateOfBan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    durationOfBanInYear = table.Column<int>(type: "int", nullable: false),
                    endDateOfBan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    accountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    identificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fax = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyer", x => x.buyerID);
                    table.ForeignKey(
                        name: "FK_buyer_authorizedPerson_authorizedPersonID",
                        column: x => x.authorizedPersonID,
                        principalTable: "authorizedPerson",
                        principalColumn: "authorizedPersonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_buyer_priority_priorityID",
                        column: x => x.priorityID,
                        principalTable: "priority",
                        principalColumn: "priorityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "authorizedPerson",
                columns: new[] { "authorizedPersonID", "address", "country", "name", "personalDocNum", "surname" },
                values: new object[,]
                {
                    { new Guid("93a08cc2-1d17-46e6-bd95-4fa70bb11226"), "Mira popare 11", "Srbija", "Dimitrije", "8767834637274", "Corlija" },
                    { new Guid("07af89f2-feee-4680-b489-9d0e31699588"), "Bulevar Oslobodjenja 55", "Zrenjanin", "Marko", "8227834666274", "Markovic" }
                });

            migrationBuilder.InsertData(
                table: "contactPerson",
                columns: new[] { "contactPersonID", "name", "phone", "surname" },
                values: new object[,]
                {
                    { new Guid("e54364be-1fe6-43b5-9401-8b8bd2165aba"), "Petar", "0629349583", "Petrovic" },
                    { new Guid("68bf5d70-f26b-4c53-b014-bab74b7b86a0"), "Miljan", "06559349583", "Peric" }
                });

            migrationBuilder.InsertData(
                table: "priority",
                columns: new[] { "priorityID", "priorityType" },
                values: new object[,]
                {
                    { new Guid("784c7edd-c937-45e6-a493-f0b8dedab85f"), "1" },
                    { new Guid("21200907-0d08-44f3-8506-dc807ca2215b"), "2" }
                });

            migrationBuilder.InsertData(
                table: "buyer",
                columns: new[] { "buyerID", "Discriminator", "IsIndividual", "JMBG", "accountNumber", "addresse", "authorizedPersonID", "durationOfBanInYear", "email", "endDateOfBan", "hasBan", "name", "phone1", "phone2", "priorityID", "realizedArea", "startDateOfBan", "surname" },
                values: new object[] { new Guid("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"), "Individual", true, "1102999765578", "4224234876", "Prvomajska 5", new Guid("07af89f2-feee-4680-b489-9d0e31699588"), 1, "dinoR@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Dino", "062987999", "-0654442223", new Guid("784c7edd-c937-45e6-a493-f0b8dedab85f"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ristic" });

            migrationBuilder.InsertData(
                table: "buyer",
                columns: new[] { "buyerID", "Discriminator", "IsIndividual", "accountNumber", "addresse", "authorizedPersonID", "durationOfBanInYear", "email", "endDateOfBan", "fax", "hasBan", "identificationNumber", "name", "phone1", "phone2", "priorityID", "realizedArea", "startDateOfBan" },
                values: new object[] { new Guid("861f142c-4707-416d-ad14-7debbd2031ed"), "LegalEntity", false, "0074234876", "8765439744578", new Guid("07af89f2-feee-4680-b489-9d0e31699588"), 1, "rosa@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "212693-2377", true, "12121212333", "Rosa", "061999999", "067662529", new Guid("784c7edd-c937-45e6-a493-f0b8dedab85f"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_buyer_authorizedPersonID",
                table: "buyer",
                column: "authorizedPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_priorityID",
                table: "buyer",
                column: "priorityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "buyer");

            migrationBuilder.DropTable(
                name: "contactPerson");

            migrationBuilder.DropTable(
                name: "authorizedPerson");

            migrationBuilder.DropTable(
                name: "priority");
        }
    }
}
