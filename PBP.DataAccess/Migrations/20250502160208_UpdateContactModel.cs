using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeGroupId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfBirthId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfSocialNetworkId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AgeGroupId",
                table: "Contact",
                column: "AgeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_BloodTypeId",
                table: "Contact",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PlaceOfBirthId",
                table: "Contact",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_TypeOfSocialNetworkId",
                table: "Contact",
                column: "TypeOfSocialNetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DynamicListItem_AgeGroupId",
                table: "Contact",
                column: "AgeGroupId",
                principalTable: "DynamicListItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DynamicListItem_BloodTypeId",
                table: "Contact",
                column: "BloodTypeId",
                principalTable: "DynamicListItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DynamicListItem_PlaceOfBirthId",
                table: "Contact",
                column: "PlaceOfBirthId",
                principalTable: "DynamicListItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_DynamicListItem_TypeOfSocialNetworkId",
                table: "Contact",
                column: "TypeOfSocialNetworkId",
                principalTable: "DynamicListItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DynamicListItem_AgeGroupId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DynamicListItem_BloodTypeId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DynamicListItem_PlaceOfBirthId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_DynamicListItem_TypeOfSocialNetworkId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_AgeGroupId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_BloodTypeId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_PlaceOfBirthId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_TypeOfSocialNetworkId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AgeGroupId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "BloodTypeId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirthId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TypeOfSocialNetworkId",
                table: "Contact");
        }
    }
}
