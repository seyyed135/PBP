using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddIndexToCCH : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "ChangedTime",
            table: "ContactChangeHistory",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateIndex(
            name: "IX_ContactChangeHistory_ChangedDate",
            table: "ContactChangeHistory",
            column: "ChangedDate");

        migrationBuilder.CreateIndex(
            name: "IX_ContactChangeHistory_ChangedTime",
            table: "ContactChangeHistory",
            column: "ChangedTime");

        migrationBuilder.CreateIndex(
            name: "IX_ContactChangeHistory_FieldName",
            table: "ContactChangeHistory",
            column: "FieldName");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_ContactChangeHistory_ChangedDate",
            table: "ContactChangeHistory");

        migrationBuilder.DropIndex(
            name: "IX_ContactChangeHistory_ChangedTime",
            table: "ContactChangeHistory");

        migrationBuilder.DropIndex(
            name: "IX_ContactChangeHistory_FieldName",
            table: "ContactChangeHistory");

        migrationBuilder.AlterColumn<string>(
            name: "ChangedTime",
            table: "ContactChangeHistory",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }
}