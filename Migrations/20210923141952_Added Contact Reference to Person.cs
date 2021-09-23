using Microsoft.EntityFrameworkCore.Migrations;

namespace EfConcurrencyTest.Migrations
{
    public partial class AddedContactReferencetoPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_ContactId",
                table: "People",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Contact_ContactId",
                table: "People",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Contact_ContactId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_ContactId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "People");
        }
    }
}
