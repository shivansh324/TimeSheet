using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpNavPropInSubmissionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubmissionLogs_EmployeeId",
                table: "SubmissionLogs",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionLogs_Employees_EmployeeId",
                table: "SubmissionLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionLogs_Employees_EmployeeId",
                table: "SubmissionLogs");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionLogs_EmployeeId",
                table: "SubmissionLogs");
        }
    }
}
