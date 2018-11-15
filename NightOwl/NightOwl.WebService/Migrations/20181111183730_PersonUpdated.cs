using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NightOwl.WebService.Migrations
{
    public partial class PersonUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorID",
                table: "Persons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MissingDate",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatorID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MissingDate",
                table: "Persons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
