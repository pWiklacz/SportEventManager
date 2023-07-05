using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

  /// <inheritdoc />
  public partial class EventPropertiesUpdate : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<bool>(
              name: "IsEnded",
              table: "Events",
              type: "bit",
              nullable: false,
              defaultValue: false);

          migrationBuilder.AddColumn<int>(
              name: "MatchDurationMinutes",
              table: "Events",
              type: "int",
              nullable: false,
              defaultValue: 0);

          migrationBuilder.AddColumn<int>(
              name: "MinPlayersQuantityPerTeam",
              table: "Events",
              type: "int",
              nullable: false,
              defaultValue: 0);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "IsEnded",
              table: "Events");

          migrationBuilder.DropColumn(
              name: "MatchDurationMinutes",
              table: "Events");

          migrationBuilder.DropColumn(
              name: "MinPlayersQuantityPerTeam",
              table: "Events");
      }
  }
