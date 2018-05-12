using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class qwerty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageMin",
                table: "Image");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "FaceBook",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Person_Id",
                table: "FaceBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageMin",
                table: "Carousel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProfilePhoto = table.Column<string>(nullable: true),
                    ReferenceFB = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaceBook_PersonId",
                table: "FaceBook",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceBook_Person_PersonId",
                table: "FaceBook",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceBook_Person_PersonId",
                table: "FaceBook");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_FaceBook_PersonId",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "Person_Id",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "ImageMin",
                table: "Carousel");

            migrationBuilder.AddColumn<string>(
                name: "ImageMin",
                table: "Image",
                nullable: true);
        }
    }
}
