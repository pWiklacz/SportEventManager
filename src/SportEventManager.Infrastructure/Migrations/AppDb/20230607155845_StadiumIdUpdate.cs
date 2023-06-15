using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

/// <inheritdoc />
public partial class StadiumIdUpdate : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropColumn(
      name: "Id",
      table: "Stadiums");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<int>(
        name: "Id",
        table: "Stadiums",
        type: "int",
        nullable: false,
        defaultValue: 0)
      .Annotation("SqlServer:Identity", "1, 1");
  }
}
