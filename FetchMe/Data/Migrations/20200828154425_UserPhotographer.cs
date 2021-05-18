using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class UserPhotographer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Photographers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_OwnerID",
                table: "Photographers",
                column: "OwnerID",
                unique: true,
                filter: "[OwnerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_AspNetUsers_OwnerID",
                table: "Photographers",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_AspNetUsers_OwnerID",
                table: "Photographers");

            migrationBuilder.DropIndex(
                name: "IX_Photographers_OwnerID",
                table: "Photographers");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Photographers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
