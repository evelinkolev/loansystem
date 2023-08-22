using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePayersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Deposit = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    RoutingNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payers_UserId",
                table: "Payers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payers");
        }
    }
}
