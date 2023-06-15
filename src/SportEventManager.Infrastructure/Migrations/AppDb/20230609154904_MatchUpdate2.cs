using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MatchUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GuestTeamStatsId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId",
                principalTable: "FbTeamMatchStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GuestTeamStatsId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId",
                principalTable: "FbTeamMatchStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
