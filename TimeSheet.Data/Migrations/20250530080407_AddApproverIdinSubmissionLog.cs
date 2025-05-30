using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApproverIdinSubmissionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "SubmissionLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionLogs_ApproverId",
                table: "SubmissionLogs",
                column: "ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionLogs_Employees_ApproverId",
                table: "SubmissionLogs",
                column: "ApproverId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionLogs_Employees_ApproverId",
                table: "SubmissionLogs");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionLogs_ApproverId",
                table: "SubmissionLogs");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "SubmissionLogs");
        }
    }
}
