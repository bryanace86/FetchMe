using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class WidthHeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XSHeight",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "XSWidth",
                table: "Photographs");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Photographs");

            migrationBuilder.AddColumn<decimal>(
                name: "XSHeight",
                table: "Photographs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "XSWidth",
                table: "Photographs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
