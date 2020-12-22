using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoGalary.Migrations
{
    public partial class changedPhotoClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Photos",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "PhotoData",
                table: "Photos",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PhotoData",
                table: "Photos");
        }
    }
}
