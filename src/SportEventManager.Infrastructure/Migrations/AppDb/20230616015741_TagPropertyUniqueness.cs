using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.AppDb;

  /// <inheritdoc />
  public partial class TagPropertyUniqueness : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateIndex(
              name: "IX_Teams_Tag",
              table: "Teams",
              column: "Tag",
              unique: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropIndex(
              name: "IX_Teams_Tag",
              table: "Teams");
      }
  }
