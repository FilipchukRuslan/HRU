using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaceBook_Person_PersonId",
                table: "FaceBook");

            migrationBuilder.DropTable(
                name: "AboutUs");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.RenameColumn(
                name: "Person_Id",
                table: "FaceBook",
                newName: "Image_Id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "FaceBook",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_FaceBook_PersonId",
                table: "FaceBook",
                newName: "IX_FaceBook_ImageId");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "AboutUnion",
                newName: "Union");

            migrationBuilder.AddColumn<string>(
                name: "PersonLink",
                table: "FaceBook",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "FaceBook",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Goals",
                table: "AboutUnion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mission",
                table: "AboutUnion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnersId",
                table: "AboutUnion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Partners_Id",
                table: "AboutUnion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AboutUnion_PartnersId",
                table: "AboutUnion",
                column: "PartnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUnion_Partners_PartnersId",
                table: "AboutUnion",
                column: "PartnersId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaceBook_Image_ImageId",
                table: "FaceBook",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUnion_Partners_PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropForeignKey(
                name: "FK_FaceBook_Image_ImageId",
                table: "FaceBook");

            migrationBuilder.DropIndex(
                name: "IX_AboutUnion_PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "PersonLink",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "FaceBook");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "Mission",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "Partners_Id",
                table: "AboutUnion");

            migrationBuilder.RenameColumn(
                name: "Image_Id",
                table: "FaceBook",
                newName: "Person_Id");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "FaceBook",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_FaceBook_ImageId",
                table: "FaceBook",
                newName: "IX_FaceBook_PersonId");

            migrationBuilder.RenameColumn(
                name: "Union",
                table: "AboutUnion",
                newName: "Text");

            migrationBuilder.CreateTable(
                name: "AboutUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: true),
                    Image_Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutUs_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_AboutUs_ImageId",
                table: "AboutUs",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaceBook_Person_PersonId",
                table: "FaceBook",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
