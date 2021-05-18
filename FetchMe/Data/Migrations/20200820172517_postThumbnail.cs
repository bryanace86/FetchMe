using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class postThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SortDescription",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailURL",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortDescription",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThumbnailURL",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");
        }
    }
}
