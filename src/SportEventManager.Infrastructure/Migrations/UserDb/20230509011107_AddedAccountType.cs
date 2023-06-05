using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportEventManager.Infrastructure.Migrations.UserDb;

  /// <inheritdoc />
  public partial class AddedAccountType : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.AddColumn<int>(
              name: "AccountType",
              table: "AspNetUsers",
              type: "int",
              nullable: true);

          migrationBuilder.AddColumn<bool>(
              name: "IsArchived",
              table: "AspNetUsers",
              type: "bit",
              nullable: false,
              defaultValue: false);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropColumn(
              name: "AccountType",
              table: "AspNetUsers");

          migrationBuilder.DropColumn(
              name: "IsArchived",
              table: "AspNetUsers");
      }
  }
