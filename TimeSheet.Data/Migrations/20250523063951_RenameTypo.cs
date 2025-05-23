using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Milestones_MilesoneId",
                table: "Timesheets");

            migrationBuilder.RenameColumn(
                name: "MilesoneId",
                table: "Timesheets",
                newName: "MilestoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheets_MilesoneId",
                table: "Timesheets",
                newName: "IX_Timesheets_MilestoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Milestones_MilestoneId",
                table: "Timesheets",
                column: "MilestoneId",
                principalTable: "Milestones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Milestones_MilestoneId",
                table: "Timesheets");

            migrationBuilder.RenameColumn(
                name: "MilestoneId",
                table: "Timesheets",
                newName: "MilesoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheets_MilestoneId",
                table: "Timesheets",
                newName: "IX_Timesheets_MilesoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Milestones_MilesoneId",
                table: "Timesheets",
                column: "MilesoneId",
                principalTable: "Milestones",
                principalColumn: "Id");
        }
    }
}
