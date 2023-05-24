using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

  /// <inheritdoc />
  public partial class TeamPlayerUpdate : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<DateTime>(
              name: "JoinOn",
              table: "TeamPlayer",
              type: "datetime2",
              nullable: false,
              defaultValueSql: "GETUTCDATE()");

          migrationBuilder.AddColumn<DateTime>(
              name: "LeaveOn",
              table: "TeamPlayer",
              type: "datetime2",
              nullable: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "JoinOn",
              table: "TeamPlayer");

          migrationBuilder.DropColumn(
              name: "LeaveOn",
              table: "TeamPlayer");
      }
  }
