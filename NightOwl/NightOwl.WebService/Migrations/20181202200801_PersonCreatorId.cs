using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NightOwl.WebService.Migrations
{
    public partial class PersonCreatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Persons",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CreatorId",
                table: "Persons",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_CreatorId",
                table: "Persons",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_CreatorId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CreatorId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Persons");

          
        }
    }
}
