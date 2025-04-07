using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLogModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "IdentityUser<string>");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IdentityUser<string>",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUser<string>",
                table: "IdentityUser<string>",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_IdentityUser<string>_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser<string>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_UserId",
                table: "ActivityLog",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUser<string>",
                table: "IdentityUser<string>");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IdentityUser<string>");

            migrationBuilder.RenameTable(
                name: "IdentityUser<string>",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
