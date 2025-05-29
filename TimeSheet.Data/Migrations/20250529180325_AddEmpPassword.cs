using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Milestones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 4,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 5,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 6,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 7,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 8,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 9,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 10,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 11,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 12,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 13,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 14,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 15,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 16,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 17,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 18,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 19,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 20,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 21,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 22,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 23,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 24,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 25,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 26,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 27,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 28,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 29,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 30,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 31,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 32,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 33,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 34,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 35,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 36,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 37,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 38,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 39,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 40,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 41,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 42,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 43,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 44,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 45,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 46,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 47,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 48,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 49,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 50,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 51,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 52,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 53,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 54,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 55,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 56,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 57,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 58,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 59,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 60,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 61,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 62,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 63,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 64,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 65,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 66,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 67,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 68,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 69,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 70,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 71,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 72,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 73,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 74,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 75,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 76,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 77,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 78,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 79,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 80,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 81,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 82,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 83,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 84,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 85,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 86,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 87,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 88,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 89,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 90,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 91,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 92,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 93,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 94,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 95,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 96,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 97,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 98,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 99,
                column: "Status",
                value: "Active");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 100,
                column: "Status",
                value: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Milestones");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
