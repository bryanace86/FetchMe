using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class shortDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortDescription",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "SortDescription",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
