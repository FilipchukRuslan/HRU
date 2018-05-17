using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class carouselAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Carousel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Carousel");
        }
    }
}
