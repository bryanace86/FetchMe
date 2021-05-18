using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class userProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Galleries",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserBannerImageId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileImageId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserBannerImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageDescription = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    HideFromGallery = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    SWidth = table.Column<int>(nullable: false),
                    SHeight = table.Column<int>(nullable: false),
                    MWidth = table.Column<int>(nullable: false),
                    MHeight = table.Column<int>(nullable: false),
                    LWidth = table.Column<int>(nullable: false),
                    LHeight = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBannerImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBannerImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageDescription = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    HideFromGallery = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    SWidth = table.Column<int>(nullable: false),
                    SHeight = table.Column<int>(nullable: false),
                    MWidth = table.Column<int>(nullable: false),
                    MHeight = table.Column<int>(nullable: false),
                    LWidth = table.Column<int>(nullable: false),
                    LHeight = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileImage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_OwnerID",
                table: "Galleries",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBannerImage_UserId",
                table: "UserBannerImage",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileImage_UserId",
                table: "UserProfileImage",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_AspNetUsers_OwnerID",
                table: "Galleries",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_AspNetUsers_OwnerID",
                table: "Galleries");

            migrationBuilder.DropTable(
                name: "UserBannerImage");

            migrationBuilder.DropTable(
                name: "UserProfileImage");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_OwnerID",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserBannerImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
