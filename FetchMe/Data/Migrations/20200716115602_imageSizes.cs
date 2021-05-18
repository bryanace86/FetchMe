using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class imageSizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LHeight",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LWidth",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MHeight",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MWidth",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SHeight",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SWidth",
                table: "Photographs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LHeight",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "LWidth",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "MHeight",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "MWidth",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "SHeight",
                table: "Photographs");

            migrationBuilder.DropColumn(
                name: "SWidth",
                table: "Photographs");
        }
    }
}
