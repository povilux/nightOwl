using Microsoft.EntityFrameworkCore.Migrations;

namespace NightOwl.WebService.Migrations
{
    public partial class InitionCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Faces",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faces_OwnerId",
                table: "Faces",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faces_Persons_OwnerId",
                table: "Faces",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faces_Persons_OwnerId",
                table: "Faces");

            migrationBuilder.DropIndex(
                name: "IX_Faces_OwnerId",
                table: "Faces");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Faces");
        }
    }
}
