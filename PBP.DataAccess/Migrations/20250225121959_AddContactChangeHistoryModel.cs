using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.DataAccess.Migrations;

/// <inheritdoc />
public partial class AddContactChangeHistoryModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ContactChangeHistory",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ContactId = table.Column<int>(type: "int", nullable: false),
                FieldName = table.Column<int>(type: "int", nullable: false),
                OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                OldImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                NewImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                ChangedDate = table.Column<DateTime>(type: "Date", nullable: false),
                ChangedTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ContactChangeHistory", x => x.Id);
                table.ForeignKey(
                    name: "FK_ContactChangeHistory_Contact_ContactId",
                    column: x => x.ContactId,
                    principalTable: "Contact",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ContactChangeHistory_ContactId",
            table: "ContactChangeHistory",
            column: "ContactId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ContactChangeHistory");
    }
}