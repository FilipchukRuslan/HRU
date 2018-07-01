using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class test8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUnion_Partners_PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropIndex(
                name: "IX_AboutUnion_PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "PartnersId",
                table: "AboutUnion");

            migrationBuilder.DropColumn(
                name: "Partners_Id",
                table: "AboutUnion");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Video",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Video",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Video",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AboutUnionId",
                table: "Partners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AboutUnion_Id",
                table: "Partners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isCrew",
                table: "Partners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Media",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Media",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Media",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Media",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AbstractInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Image_Id = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbstractInfo_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partners_AboutUnionId",
                table: "Partners",
                column: "AboutUnionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractInfo_ImageId",
                table: "AbstractInfo",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_AboutUnion_AboutUnionId",
                table: "Partners",
                column: "AboutUnionId",
                principalTable: "AboutUnion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_AboutUnion_AboutUnionId",
                table: "Partners");

            migrationBuilder.DropTable(
                name: "AbstractInfo");

            migrationBuilder.DropIndex(
                name: "IX_Partners_AboutUnionId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AboutUnionId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "AboutUnion_Id",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "isCrew",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Media");

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
        }
    }
}
