using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "Events",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                  Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                  EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                  IsInprogress = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Events", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Players",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                  Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Players", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Stadiums",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                  City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Stadiums", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Stats",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  Goals = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Assists = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  RedCards = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  YellowCards = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Stats", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "Teams",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                  Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  NumberOfPlayers = table.Column<int>(type: "int", nullable: false),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Teams", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "EventStadium",
              columns: table => new
              {
                  EventsId = table.Column<int>(type: "int", nullable: false),
                  StadiumsId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_EventStadium", x => new { x.EventsId, x.StadiumsId });
                  table.ForeignKey(
                      name: "FK_EventStadium_Events_EventsId",
                      column: x => x.EventsId,
                      principalTable: "Events",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_EventStadium_Stadiums_StadiumsId",
                      column: x => x.StadiumsId,
                      principalTable: "Stadiums",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "FbTeamMatchStats",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false),
                  Shoots = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  ShootsOnTarget = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Fouls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Passes = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_FbTeamMatchStats", x => x.Id);
                  table.ForeignKey(
                      name: "FK_FbTeamMatchStats_Stats_Id",
                      column: x => x.Id,
                      principalTable: "Stats",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "PlayerStats",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false),
                  PlayerId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PlayerStats", x => x.Id);
                  table.ForeignKey(
                      name: "FK_PlayerStats_Players_PlayerId",
                      column: x => x.PlayerId,
                      principalTable: "Players",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_PlayerStats_Stats_Id",
                      column: x => x.Id,
                      principalTable: "Stats",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "EventTeam",
              columns: table => new
              {
                  EventsId = table.Column<int>(type: "int", nullable: false),
                  TeamsId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_EventTeam", x => new { x.EventsId, x.TeamsId });
                  table.ForeignKey(
                      name: "FK_EventTeam_Events_EventsId",
                      column: x => x.EventsId,
                      principalTable: "Events",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_EventTeam_Teams_TeamsId",
                      column: x => x.TeamsId,
                      principalTable: "Teams",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "TeamPlayer",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  TeamId = table.Column<int>(type: "int", nullable: false),
                  PlayerId = table.Column<int>(type: "int", nullable: false),
                  Number = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TeamPlayer", x => x.Id);
                  table.ForeignKey(
                      name: "FK_TeamPlayer_Players_PlayerId",
                      column: x => x.PlayerId,
                      principalTable: "Players",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_TeamPlayer_Teams_TeamId",
                      column: x => x.TeamId,
                      principalTable: "Teams",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.CreateTable(
              name: "TeamStats",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false),
                  Wins = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Losses = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Draws = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Shoots = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  ShootsOnTarget = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Fouls = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  Passes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                  TeamId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_TeamStats", x => x.Id);
                  table.ForeignKey(
                      name: "FK_TeamStats_Stats_Id",
                      column: x => x.Id,
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

          migrationBuilder.CreateTable(
              name: "Matches",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                  EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                  WinnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                  IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                  IsEnded = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                  StadiumId = table.Column<int>(type: "int", nullable: false),
                  HomeTeamId = table.Column<int>(type: "int", nullable: false),
                  GuestTeamId = table.Column<int>(type: "int", nullable: false),
                  EventId = table.Column<int>(type: "int", nullable: false),
                  HomeTeamStatsId = table.Column<int>(type: "int", nullable: true),
                  GuestTeamStatsId = table.Column<int>(type: "int", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Matches", x => x.Id);
                  table.ForeignKey(
                      name: "FK_Matches_Events_EventId",
                      column: x => x.EventId,
                      principalTable: "Events",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_Matches_FbTeamMatchStats_GuestTeamStatsId",
                      column: x => x.GuestTeamStatsId,
                      principalTable: "FbTeamMatchStats",
                      principalColumn: "ID");
                  table.ForeignKey(
                      name: "FK_Matches_FbTeamMatchStats_HomeTeamStatsId",
                      column: x => x.HomeTeamStatsId,
                      principalTable: "FbTeamMatchStats",
                      principalColumn: "ID");
                  table.ForeignKey(
                      name: "FK_Matches_Stadiums_StadiumId",
                      column: x => x.StadiumId,
                      principalTable: "Stadiums",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
                  table.ForeignKey(
                      name: "FK_Matches_Teams_GuestTeamId",
                      column: x => x.GuestTeamId,
                      principalTable: "Teams",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Restrict);
                  table.ForeignKey(
                      name: "FK_Matches_Teams_HomeTeamId",
                      column: x => x.HomeTeamId,
                      principalTable: "Teams",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Restrict);
              });

          migrationBuilder.CreateIndex(
              name: "IX_EventStadium_StadiumsId",
              table: "EventStadium",
              column: "StadiumsId");

          migrationBuilder.CreateIndex(
              name: "IX_EventTeam_TeamsId",
              table: "EventTeam",
              column: "TeamsId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_EventId",
              table: "Matches",
              column: "EventId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_GuestTeamId",
              table: "Matches",
              column: "GuestTeamId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_GuestTeamStatsId",
              table: "Matches",
              column: "GuestTeamStatsId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_HomeTeamId",
              table: "Matches",
              column: "HomeTeamId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_HomeTeamStatsId",
              table: "Matches",
              column: "HomeTeamStatsId");

          migrationBuilder.CreateIndex(
              name: "IX_Matches_StadiumId",
              table: "Matches",
              column: "StadiumId");

          migrationBuilder.CreateIndex(
              name: "IX_PlayerStats_PlayerId",
              table: "PlayerStats",
              column: "PlayerId",
              unique: true,
              filter: "[PlayerId] IS NOT NULL");

          migrationBuilder.CreateIndex(
              name: "IX_TeamPlayer_PlayerId",
              table: "TeamPlayer",
              column: "PlayerId");

          migrationBuilder.CreateIndex(
              name: "IX_TeamPlayer_TeamId",
              table: "TeamPlayer",
              column: "TeamId");

          migrationBuilder.CreateIndex(
              name: "IX_TeamStats_TeamId",
              table: "TeamStats",
              column: "TeamId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "EventStadium");

          migrationBuilder.DropTable(
              name: "EventTeam");

          migrationBuilder.DropTable(
              name: "Matches");

          migrationBuilder.DropTable(
              name: "PlayerStats");

          migrationBuilder.DropTable(
              name: "TeamPlayer");

          migrationBuilder.DropTable(
              name: "TeamStats");

          migrationBuilder.DropTable(
              name: "Events");

          migrationBuilder.DropTable(
              name: "FbTeamMatchStats");

          migrationBuilder.DropTable(
              name: "Stadiums");

          migrationBuilder.DropTable(
              name: "Players");

          migrationBuilder.DropTable(
              name: "Teams");

          migrationBuilder.DropTable(
              name: "Stats");
      }
  }
