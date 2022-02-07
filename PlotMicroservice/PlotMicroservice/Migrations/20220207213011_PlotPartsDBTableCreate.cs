using Microsoft.EntityFrameworkCore.Migrations;

namespace PlotMicroservice.Migrations
{
    public partial class PlotPartsDBTableCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotPart_PlotPartClasses_PlotPartClassId",
                table: "PlotPart");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotPart_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                table: "PlotPart");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotPart_PlotPartProtectedZones_PlotPartProtectedZoneId",
                table: "PlotPart");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotPart_Plots_PlotId",
                table: "PlotPart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlotPart",
                table: "PlotPart");

            migrationBuilder.RenameTable(
                name: "PlotPart",
                newName: "PlotParts");

            migrationBuilder.RenameIndex(
                name: "IX_PlotPart_PlotPartProtectedZoneId",
                table: "PlotParts",
                newName: "IX_PlotParts_PlotPartProtectedZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotPart_PlotPartFormOfOwnershipId",
                table: "PlotParts",
                newName: "IX_PlotParts_PlotPartFormOfOwnershipId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotPart_PlotPartClassId",
                table: "PlotParts",
                newName: "IX_PlotParts_PlotPartClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotPart_PlotId",
                table: "PlotParts",
                newName: "IX_PlotParts_PlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlotParts",
                table: "PlotParts",
                column: "PlotPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlotParts_PlotPartClasses_PlotPartClassId",
                table: "PlotParts",
                column: "PlotPartClassId",
                principalTable: "PlotPartClasses",
                principalColumn: "PlotPartClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotParts_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                table: "PlotParts",
                column: "PlotPartFormOfOwnershipId",
                principalTable: "PlotPartFormOfOwnerships",
                principalColumn: "PlotPartFormOfOwnershipId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotParts_PlotPartProtectedZones_PlotPartProtectedZoneId",
                table: "PlotParts",
                column: "PlotPartProtectedZoneId",
                principalTable: "PlotPartProtectedZones",
                principalColumn: "PlotPartProtectedZoneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotParts_Plots_PlotId",
                table: "PlotParts",
                column: "PlotId",
                principalTable: "Plots",
                principalColumn: "PlotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlotParts_PlotPartClasses_PlotPartClassId",
                table: "PlotParts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotParts_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                table: "PlotParts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotParts_PlotPartProtectedZones_PlotPartProtectedZoneId",
                table: "PlotParts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlotParts_Plots_PlotId",
                table: "PlotParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlotParts",
                table: "PlotParts");

            migrationBuilder.RenameTable(
                name: "PlotParts",
                newName: "PlotPart");

            migrationBuilder.RenameIndex(
                name: "IX_PlotParts_PlotPartProtectedZoneId",
                table: "PlotPart",
                newName: "IX_PlotPart_PlotPartProtectedZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotParts_PlotPartFormOfOwnershipId",
                table: "PlotPart",
                newName: "IX_PlotPart_PlotPartFormOfOwnershipId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotParts_PlotPartClassId",
                table: "PlotPart",
                newName: "IX_PlotPart_PlotPartClassId");

            migrationBuilder.RenameIndex(
                name: "IX_PlotParts_PlotId",
                table: "PlotPart",
                newName: "IX_PlotPart_PlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlotPart",
                table: "PlotPart",
                column: "PlotPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlotPart_PlotPartClasses_PlotPartClassId",
                table: "PlotPart",
                column: "PlotPartClassId",
                principalTable: "PlotPartClasses",
                principalColumn: "PlotPartClassId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotPart_PlotPartFormOfOwnerships_PlotPartFormOfOwnershipId",
                table: "PlotPart",
                column: "PlotPartFormOfOwnershipId",
                principalTable: "PlotPartFormOfOwnerships",
                principalColumn: "PlotPartFormOfOwnershipId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotPart_PlotPartProtectedZones_PlotPartProtectedZoneId",
                table: "PlotPart",
                column: "PlotPartProtectedZoneId",
                principalTable: "PlotPartProtectedZones",
                principalColumn: "PlotPartProtectedZoneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlotPart_Plots_PlotId",
                table: "PlotPart",
                column: "PlotId",
                principalTable: "Plots",
                principalColumn: "PlotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
