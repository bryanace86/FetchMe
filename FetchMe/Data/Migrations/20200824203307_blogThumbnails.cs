using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class blogThumbnails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailURL",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThumbnailId",
                table: "Posts",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ThumbnailId",
                table: "Blogs",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Photographs_ThumbnailId",
                table: "Blogs",
                column: "ThumbnailId",
                principalTable: "Photographs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Photographs_ThumbnailId",
                table: "Posts",
                column: "ThumbnailId",
                principalTable: "Photographs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Photographs_ThumbnailId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Photographs_ThumbnailId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ThumbnailId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_ThumbnailId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailURL",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
