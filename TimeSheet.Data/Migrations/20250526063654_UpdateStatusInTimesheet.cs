using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusInTimesheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectMilestones");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Timesheets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Timesheets");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProjectMilestones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
