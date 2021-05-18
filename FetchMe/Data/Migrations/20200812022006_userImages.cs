using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class userImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBannerImage_AspNetUsers_UserId",
                table: "UserBannerImage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileImage_AspNetUsers_UserId",
                table: "UserProfileImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileImage",
                table: "UserProfileImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBannerImage",
                table: "UserBannerImage");

            migrationBuilder.RenameTable(
                name: "UserProfileImage",
                newName: "UserProfileImages");

            migrationBuilder.RenameTable(
                name: "UserBannerImage",
                newName: "UserBannerImages");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileImage_UserId",
                table: "UserProfileImages",
                newName: "IX_UserProfileImages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBannerImage_UserId",
                table: "UserBannerImages",
                newName: "IX_UserBannerImages_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileImages",
                table: "UserProfileImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBannerImages",
                table: "UserBannerImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBannerImages_AspNetUsers_UserId",
                table: "UserBannerImages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileImages_AspNetUsers_UserId",
                table: "UserProfileImages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBannerImages_AspNetUsers_UserId",
                table: "UserBannerImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileImages_AspNetUsers_UserId",
                table: "UserProfileImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfileImages",
                table: "UserProfileImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBannerImages",
                table: "UserBannerImages");

            migrationBuilder.RenameTable(
                name: "UserProfileImages",
                newName: "UserProfileImage");

            migrationBuilder.RenameTable(
                name: "UserBannerImages",
                newName: "UserBannerImage");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfileImages_UserId",
                table: "UserProfileImage",
                newName: "IX_UserProfileImage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBannerImages_UserId",
                table: "UserBannerImage",
                newName: "IX_UserBannerImage_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfileImage",
                table: "UserProfileImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBannerImage",
                table: "UserBannerImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBannerImage_AspNetUsers_UserId",
                table: "UserBannerImage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfileImage_AspNetUsers_UserId",
                table: "UserProfileImage",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
