using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class support : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportTicketStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicketStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportTickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedByName = table.Column<string>(nullable: true),
                    AssignedTo = table.Column<string>(nullable: true),
                    AssignedToName = table.Column<string>(nullable: true),
                    SupportTicketStatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTickets_SupportTicketStatus_SupportTicketStatusId",
                        column: x => x.SupportTicketStatusId,
                        principalTable: "SupportTicketStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicketResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Response = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedByName = table.Column<string>(nullable: true),
                    SupportTicketId = table.Column<Guid>(nullable: false),
                    Viewed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicketResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTicketResponses_SupportTickets_SupportTicketId",
                        column: x => x.SupportTicketId,
                        principalTable: "SupportTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicketResponses_SupportTicketId",
                table: "SupportTicketResponses",
                column: "SupportTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTickets_SupportTicketStatusId",
                table: "SupportTickets",
                column: "SupportTicketStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportTicketResponses");

            migrationBuilder.DropTable(
                name: "SupportTickets");

            migrationBuilder.DropTable(
                name: "SupportTicketStatus");
        }
    }
}
