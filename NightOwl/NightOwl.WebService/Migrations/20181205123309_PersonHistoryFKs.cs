using Microsoft.EntityFrameworkCore.Migrations;

namespace NightOwl.WebService.Migrations
{
    public partial class PersonHistoryFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Persons_PersonId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Faces_SourceFaceId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_SourceFaceId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "SourceFaceId",
                table: "History");

            migrationBuilder.AlterColumn<string>(
                name: "SourceFaceUrl",
                table: "History",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "History",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Faces_BlobURI",
                table: "Faces",
                column: "BlobURI");

            migrationBuilder.CreateIndex(
                name: "IX_History_SourceFaceUrl",
                table: "History",
                column: "SourceFaceUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Persons_PersonId",
                table: "History",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Faces_SourceFaceUrl",
                table: "History",
                column: "SourceFaceUrl",
                principalTable: "Faces",
                principalColumn: "BlobURI",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Persons_PersonId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Faces_SourceFaceUrl",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_SourceFaceUrl",
                table: "History");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Faces_BlobURI",
                table: "Faces");

            migrationBuilder.AlterColumn<string>(
                name: "SourceFaceUrl",
                table: "History",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "History",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SourceFaceId",
                table: "History",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_SourceFaceId",
                table: "History",
                column: "SourceFaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Persons_PersonId",
                table: "History",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Faces_SourceFaceId",
                table: "History",
                column: "SourceFaceId",
                principalTable: "Faces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
