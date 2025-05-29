using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescFromMilestone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Milestones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Milestones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "CV search, CV preparation, formatting, fee sheet, A & M, EMD Preparation, Proposal compilation");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Tender scanning, meeting prospective clients, Networking with tenderers");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "capdoc, brochure preparation, project Information sheet");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 10,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 11,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 12,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 19,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 20,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 21,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 22,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 23,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 24,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 25,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 26,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 27,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 28,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 29,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 30,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 31,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 32,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 33,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 34,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 35,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 36,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 37,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 38,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 39,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 40,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 41,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 42,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 43,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 44,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 45,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 46,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 47,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 48,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 49,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 50,
                column: "Description",
                value: "Maintaining Asset Register and tracking asset utilisation");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 51,
                column: "Description",
                value: "Air/rail/Cab booking");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 52,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 53,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 54,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 55,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 56,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 57,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 58,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 59,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 60,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 61,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 62,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 63,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 64,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 65,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 66,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 67,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 68,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 69,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 70,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 71,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 72,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 73,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 74,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 75,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 76,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 77,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 78,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 79,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 80,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 81,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 82,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 83,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 84,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 85,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 86,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 87,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 88,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 89,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 90,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 91,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 92,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 93,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 94,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 95,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 96,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 97,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 98,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 99,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Milestones",
                keyColumn: "Id",
                keyValue: 100,
                column: "Description",
                value: "");
        }
    }
}
