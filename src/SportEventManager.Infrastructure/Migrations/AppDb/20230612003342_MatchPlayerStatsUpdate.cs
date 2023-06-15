using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MatchPlayerStatsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStats_PlayerId",
                table: "PlayerMatchStats",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerMatchStats_Players_PlayerId",
                table: "PlayerMatchStats",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerMatchStats_Players_PlayerId",
                table: "PlayerMatchStats");

            migrationBuilder.DropIndex(
                name: "IX_PlayerMatchStats_PlayerId",
                table: "PlayerMatchStats");
        }
    }
}
