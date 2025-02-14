using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.Migrations;

/// <inheritdoc />
public partial class AddContactAndImageModel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Image",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Image", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Contact",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                BirthDate = table.Column<DateTime>(type: "Date", nullable: true),
                ImageId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Contact", x => x.Id);
                table.ForeignKey(
                    name: "FK_Contact_Image_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Image",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Contact_ImageId",
            table: "Contact",
            column: "ImageId");

        migrationBuilder.CreateIndex(
            name: "IX_Contact_PhoneNumber",
            table: "Contact",
            column: "PhoneNumber",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Contact");

        migrationBuilder.DropTable(
            name: "Image");
    }
}