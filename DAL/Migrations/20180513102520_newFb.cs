using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class newFb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "FaceBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "FaceBook",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "FaceBook",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "FaceBook");
        }
    }
}
