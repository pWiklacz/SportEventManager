using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class StadiumIdUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventStadium_Stadiums_StadiumsnewID",
                table: "EventStadium");

            migrationBuilder.RenameColumn(
                name: "newID",
                table: "Stadiums",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StadiumsnewID",
                table: "EventStadium",
                newName: "StadiumsId");

            migrationBuilder.RenameIndex(
                name: "IX_EventStadium_StadiumsnewID",
                table: "EventStadium",
                newName: "IX_EventStadium_StadiumsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventStadium_Stadiums_StadiumsId",
                table: "EventStadium",
                column: "StadiumsId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventStadium_Stadiums_StadiumsId",
                table: "EventStadium");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stadiums",
                newName: "newID");

            migrationBuilder.RenameColumn(
                name: "StadiumsId",
                table: "EventStadium",
                newName: "StadiumsnewID");

            migrationBuilder.RenameIndex(
                name: "IX_EventStadium_StadiumsId",
                table: "EventStadium",
                newName: "IX_EventStadium_StadiumsnewID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventStadium_Stadiums_StadiumsnewID",
                table: "EventStadium",
                column: "StadiumsnewID",
                principalTable: "Stadiums",
                principalColumn: "newID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
