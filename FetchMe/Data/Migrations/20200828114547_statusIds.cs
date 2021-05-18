using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FetchMe.Data.Migrations
{
    public partial class statusIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
