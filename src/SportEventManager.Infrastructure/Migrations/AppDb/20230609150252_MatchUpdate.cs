using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class MatchUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FbTeamMatchStats_HomeTeamStatsId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HomeTeamStatsId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "HomeTeamStatsId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestTeamStatsId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamStatsId",
                table: "Matches",
                column: "HomeTeamStatsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId",
                principalTable: "FbTeamMatchStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_HomeTeamStatsId",
                table: "Matches",
                column: "HomeTeamStatsId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FbTeamMatchStats_HomeTeamStatsId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HomeTeamStatsId",
                table: "Matches");

            migrationBuilder.AlterColumn<int>(
                name: "HomeTeamStatsId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GuestTeamStatsId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamStatsId",
                table: "Matches",
                column: "HomeTeamStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                table: "Matches",
                column: "GuestTeamStatsId",
                principalTable: "FbTeamMatchStats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FbTeamMatchStats_HomeTeamStatsId",
                table: "Matches",
                column: "HomeTeamStatsId",
                principalTable: "FbTeamMatchStats",
                principalColumn: "Id");
        }
    }
}
