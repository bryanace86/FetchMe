using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class xsimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "XSHeight",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "XSWidth",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XSHeight",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "XSWidth",
                table: "Photographs");
        }
    }
}
