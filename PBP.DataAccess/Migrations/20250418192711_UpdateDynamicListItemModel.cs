using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDynamicListItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "DynamicListItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicListItem_Category",
                table: "DynamicListItem",
                column: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DynamicListItem_Category",
                table: "DynamicListItem");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "DynamicListItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
