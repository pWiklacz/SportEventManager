using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

  /// <inheritdoc />
  public partial class StatsUpdate : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropForeignKey(
              name: "FK_EventStadium_Stadiums_StadiumsId",
              table: "EventStadium");

          migrationBuilder.DropForeignKey(
              name: "FK_FbTeamMatchStats_Stats_ID",
              table: "FbTeamMatchStats");

          migrationBuilder.DropForeignKey(
              name: "FK_Matches_Stadiums_StadiumId",
              table: "Matches");

          migrationBuilder.DropTable(
              name: "PlayerStats");

          migrationBuilder.DropTable(
              name: "TeamStats");

          migrationBuilder.DropPrimaryKey(
              name: "PK_Stadiums",
              table: "Stadiums");

          migrationBuilder.DropPrimaryKey(
              name: "PK_EventStadium",
              table: "EventStadium");

          migrationBuilder.DropIndex(
              name: "IX_EventStadium_StadiumsId",
              table: "EventStadium");

          migrationBuilder.DropColumn(
              name: "StadiumsId",
              table: "EventStadium");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Teams",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "TeamPlayer",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Stats",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Stadiums",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Players",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Matches",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "FbTeamMatchStats",
              newName: "Id");

          migrationBuilder.RenameColumn(
              name: "ID",
              table: "Events",
              newName: "Id");

          migrationBuilder.AddColumn<string>(
              name: "newID",
              table: "Stadiums",
              type: "nvarchar(450)",
              nullable: false,
              defaultValue: "");

          migrationBuilder.AlterColumn<string>(
              name: "StadiumId",
              table: "Matches",
              type: "nvarchar(450)",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");

          migrationBuilder.AddColumn<bool>(
              name: "Draw",
              table: "FbTeamMatchStats",
              type: "bit",
              nullable: false,
              defaultValue: false);

          migrationBuilder.AddColumn<bool>(
              name: "Loss",
              table: "FbTeamMatchStats",
              type: "bit",
              nullable: false,
              defaultValue: false);

          migrationBuilder.AddColumn<int>(
              name: "TeamId",
              table: "FbTeamMatchStats",
              type: "int",
              nullable: false,
              defaultValue: 0);

          migrationBuilder.AddColumn<bool>(
              name: "Win",
              table: "FbTeamMatchStats",
              type: "bit",
              nullable: false,
              defaultValue: false);

          migrationBuilder.AddColumn<string>(
              name: "StadiumsnewID",
              table: "EventStadium",
              type: "nvarchar(450)",
              nullable: false,
              defaultValue: "");

          migrationBuilder.AddPrimaryKey(
              name: "PK_Stadiums",
              table: "Stadiums",
              column: "newID");

          migrationBuilder.AddPrimaryKey(
              name: "PK_EventStadium",
              table: "EventStadium",
              columns: new[] { "EventsId", "StadiumsnewID" });

          migrationBuilder.CreateTable(
              name: "PlayerMatchStats",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false),
                  PlayerId = table.Column<int>(type: "int", nullable: false),
                  MatchId = table.Column<int>(type: "int", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PlayerMatchStats", x => x.Id);
                  table.ForeignKey(
                      name: "FK_PlayerMatchStats_Matches_MatchId",
                      column: x => x.MatchId,
                      principalTable: "Matches",
                      principalColumn: "Id");
                  table.ForeignKey(
                      name: "FK_PlayerMatchStats_Stats_Id",
                      column: x => x.Id,
                      principalTable: "Stats",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateIndex(
              name: "IX_EventStadium_StadiumsnewID",
              table: "EventStadium",
              column: "StadiumsnewID");

          migrationBuilder.CreateIndex(
              name: "IX_PlayerMatchStats_MatchId",
              table: "PlayerMatchStats",
              column: "MatchId");

          migrationBuilder.AddForeignKey(
              name: "FK_EventStadium_Stadiums_StadiumsnewID",
              table: "EventStadium",
              column: "StadiumsnewID",
              principalTable: "Stadiums",
              principalColumn: "newID",
              onDelete: ReferentialAction.Cascade);

          migrationBuilder.AddForeignKey(
              name: "FK_FbTeamMatchStats_Stats_Id",
              table: "FbTeamMatchStats",
              column: "Id",
              principalTable: "Stats",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);

          migrationBuilder.AddForeignKey(
              name: "FK_Matches_Stadiums_StadiumId",
              table: "Matches",
              column: "StadiumId",
              principalTable: "Stadiums",
              principalColumn: "newID",
              onDelete: ReferentialAction.Cascade);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropForeignKey(
              name: "FK_EventStadium_Stadiums_StadiumsnewID",
              table: "EventStadium");

          migrationBuilder.DropForeignKey(
              name: "FK_FbTeamMatchStats_Stats_Id",
              table: "FbTeamMatchStats");

          migrationBuilder.DropForeignKey(
              name: "FK_Matches_Stadiums_StadiumId",
              table: "Matches");

          migrationBuilder.DropTable(
              name: "PlayerMatchStats");

          migrationBuilder.DropPrimaryKey(
              name: "PK_Stadiums",
              table: "Stadiums");

          migrationBuilder.DropPrimaryKey(
              name: "PK_EventStadium",
              table: "EventStadium");

          migrationBuilder.DropIndex(
              name: "IX_EventStadium_StadiumsnewID",
              table: "EventStadium");

          migrationBuilder.DropColumn(
              name: "newID",
              table: "Stadiums");

          migrationBuilder.DropColumn(
              name: "Draw",
              table: "FbTeamMatchStats");

          migrationBuilder.DropColumn(
              name: "Loss",
              table: "FbTeamMatchStats");

          migrationBuilder.DropColumn(
              name: "TeamId",
              table: "FbTeamMatchStats");

          migrationBuilder.DropColumn(
              name: "Win",
              table: "FbTeamMatchStats");

          migrationBuilder.DropColumn(
              name: "StadiumsnewID",
              table: "EventStadium");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Teams",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "TeamPlayer",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Stats",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Stadiums",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Players",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Matches",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "FbTeamMatchStats",
              newName: "ID");

          migrationBuilder.RenameColumn(
              name: "Id",
              table: "Events",
              newName: "ID");

          migrationBuilder.AlterColumn<int>(
              name: "StadiumId",
              table: "Matches",
              type: "int",
              nullable: false,
              oldClrType: typeof(string),
              oldType: "nvarchar(450)");

          migrationBuilder.AddColumn<int>(
              name: "StadiumsId",
              table: "EventStadium",
              type: "int",
              nullable: false,
              defaultValue: 0);

          migrationBuilder.AddPrimaryKey(
              name: "PK_Stadiums",
              table: "Stadiums",
              column: "ID");

          migrationBuilder.AddPrimaryKey(
              name: "PK_EventStadium",
              table: "EventStadium",
              columns: new[] { "EventsId", "StadiumsId" });

          migrationBuilder.CreateTable(
              name: "PlayerStats",
              columns: table => new
              {
                  ID = table.Column<int>(type: "int", nullable: false),
                  PlayerId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PlayerStats", x => x.ID);
                  table.ForeignKey(
                      name: "FK_PlayerStats_Players_PlayerId",
                      column: x => x.PlayerId,
                      principalTable: "Players",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_PlayerStats_Stats_ID",
                      column: x => x.ID,
                      principalTable: "Stats",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "TeamStats",
              columns: table => new
              {
                  ID = table.Column<int>(type: "int", nullable: false),
                  TeamId = table.Column<int>(type: "int", nullable: false),
                  Draws = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Fouls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Losses = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Passes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Shoots = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  ShootsOnTarget = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Wins = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TeamStats", x => x.ID);
                  table.ForeignKey(
                      name: "FK_TeamStats_Stats_ID",
                      column: x => x.ID,
                      principalTable: "Stats",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_TeamStats_Teams_TeamId",
                      column: x => x.TeamId,
                      principalTable: "Teams",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateIndex(
              name: "IX_EventStadium_StadiumsId",
              table: "EventStadium",
              column: "StadiumsId");

          migrationBuilder.CreateIndex(
              name: "IX_PlayerStats_PlayerId",
              table: "PlayerStats",
              column: "PlayerId",
              unique: true,
              filter: "[PlayerId] IS NOT NULL");

          migrationBuilder.CreateIndex(
              name: "IX_TeamStats_TeamId",
              table: "TeamStats",
              column: "TeamId");

          migrationBuilder.AddForeignKey(
              name: "FK_EventStadium_Stadiums_StadiumsId",
              table: "EventStadium",
              column: "StadiumsId",
              principalTable: "Stadiums",
              principalColumn: "ID",
              onDelete: ReferentialAction.Cascade);

          migrationBuilder.AddForeignKey(
              name: "FK_FbTeamMatchStats_Stats_ID",
              table: "FbTeamMatchStats",
              column: "ID",
              principalTable: "Stats",
              principalColumn: "ID",
              onDelete: ReferentialAction.Cascade);

          migrationBuilder.AddForeignKey(
              name: "FK_Matches_Stadiums_StadiumId",
              table: "Matches",
              column: "StadiumId",
              principalTable: "Stadiums",
              principalColumn: "ID",
              onDelete: ReferentialAction.Cascade);
      }
  }
