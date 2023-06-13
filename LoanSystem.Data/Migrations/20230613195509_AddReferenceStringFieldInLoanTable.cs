using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceStringFieldInLoanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Loan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 7,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 8,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 9,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 10,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 11,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 13,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 14,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 15,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.UpdateData(
                table: "Loan",
                keyColumn: "Id",
                keyValue: 16,
                column: "UserId",
                value: "3eb97628-c518-4b41-845e-9f3f68ac51c9");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId",
                table: "Loan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_AspNetUsers_UserId",
                table: "Loan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_AspNetUsers_UserId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_UserId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Loan");
        }
    }
}
