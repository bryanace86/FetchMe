using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SupportTicketStatus",
                keyColumn: "Id",
                keyValue: new Guid("5be86e69-0ac0-4322-ae59-d9e4eaabfc9b"));

            migrationBuilder.DeleteData(
                table: "SupportTicketStatus",
                keyColumn: "Id",
                keyValue: new Guid("8c108fb0-e003-4028-a9dc-48c5ee46936b"));

            migrationBuilder.DeleteData(
                table: "SupportTicketStatus",
                keyColumn: "Id",
                keyValue: new Guid("d4919e2f-d00a-45e1-bff3-78e34ac98ea3"));

            migrationBuilder.DeleteData(
                table: "SupportTicketStatus",
                keyColumn: "Id",
                keyValue: new Guid("eea30f4e-1d22-4aa0-94c2-6f2be2633bd0"));

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    BlogId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_OwnerId",
                table: "Blogs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.InsertData(
                table: "SupportTicketStatus",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { new Guid("5be86e69-0ac0-4322-ae59-d9e4eaabfc9b"), "New" },
                    { new Guid("eea30f4e-1d22-4aa0-94c2-6f2be2633bd0"), "Pending User Response" },
                    { new Guid("d4919e2f-d00a-45e1-bff3-78e34ac98ea3"), "Pending Support Response" },
                    { new Guid("8c108fb0-e003-4028-a9dc-48c5ee46936b"), "Closed" }
                });
        }
    }
}
