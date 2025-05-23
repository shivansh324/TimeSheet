using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSheet.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Hours = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectMilestones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    TimeSheetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSheetLineNumber = table.Column<int>(type: "int", nullable: false),
                    WbsId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MilestoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MilestoneDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedHours = table.Column<long>(type: "bigint", nullable: false),
                    TotalWorkingHours = table.Column<long>(type: "bigint", nullable: false),
                    PendingWorkingHours = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectionRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMilestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMilestones_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    MilesoneId = table.Column<int>(type: "int", nullable: true),
                    ProjectMilestoneId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Hours = table.Column<TimeSpan>(type: "time", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBillable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timesheets_Milestones_MilesoneId",
                        column: x => x.MilesoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Timesheets_ProjectMilestones_ProjectMilestoneId",
                        column: x => x.ProjectMilestoneId,
                        principalTable: "ProjectMilestones",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business Development" },
                    { 2, "Operations" },
                    { 3, "Finance & Payroll" },
                    { 4, "HR" },
                    { 5, "IT" },
                    { 6, "Marketing" },
                    { 7, "Legal" },
                    { 8, "Admin/Facilities" },
                    { 9, "Company Secretary" },
                    { 10, "ED" },
                    { 11, "Business Vertical Head" },
                    { 12, "Project Managers" },
                    { 13, "Bid Managers" },
                    { 14, "Project Coordinators" },
                    { 15, "Project Team Members" }
                });

            migrationBuilder.InsertData(
                table: "Milestones",
                columns: new[] { "Id", "DepartmentId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "CV search, CV preparation, formatting, fee sheet, A & M, EMD Preparation, Proposal compilation", "Bid Preparation" },
                    { 2, 1, "Tender scanning, meeting prospective clients, Networking with tenderers", "Pipeline creation" },
                    { 3, 1, "capdoc, brochure preparation, project Information sheet", "BD support work" },
                    { 4, 1, "", "Bid analysis / reporting" },
                    { 5, 1, "", "Learning and development" },
                    { 6, 1, "", "Audit & Compliances" },
                    { 7, 1, "", "Additional" },
                    { 8, 2, "", "Invoicing" },
                    { 9, 2, "", "Procurement" },
                    { 10, 2, "", "ERP" },
                    { 11, 2, "", "Contract Management" },
                    { 12, 2, "", "Project/ Business Performance Analysis and Reporting" },
                    { 13, 2, "", "ERP Customization" },
                    { 14, 2, "", "Additional" },
                    { 15, 3, "", "Financial Statements preparation and analysis" },
                    { 16, 3, "", "Payments" },
                    { 17, 3, "", "Taxation" },
                    { 18, 3, "", "Banking" },
                    { 19, 3, "", "Learning and Development" },
                    { 20, 3, "", "Audit & Compliances" },
                    { 21, 3, "", "Additional" },
                    { 22, 4, "", "Talent acquisition" },
                    { 23, 4, "", "HR Administration" },
                    { 24, 4, "", "Performance Management, R&R" },
                    { 25, 4, "", "Succession Planning/Leadership Development" },
                    { 26, 4, "", "Training & Development" },
                    { 27, 4, "", "Audit & Compliances" },
                    { 28, 4, "", "Additional" },
                    { 29, 5, "", "Hardware/Software procurement" },
                    { 30, 5, "", "IT Support/troubleshooting" },
                    { 31, 5, "", "Network/Server maintenance" },
                    { 32, 5, "", "Digital innovation support" },
                    { 33, 5, "", "Learning and Development" },
                    { 34, 5, "", "SLA/Policy Compliance" },
                    { 35, 5, "", "Additional" },
                    { 36, 6, "", "Brand/Corporate Communication" },
                    { 37, 6, "", "Marketing Material development" },
                    { 38, 6, "", "Digital Marketing" },
                    { 39, 6, "", "Association Membership and networking" },
                    { 40, 6, "", "Learning & Development" },
                    { 41, 6, "", "Audit/ Compliances" },
                    { 42, 6, "", "Additional" },
                    { 43, 7, "", "Contract drafting & Review" },
                    { 44, 7, "", "Handling legal issues and cases" },
                    { 45, 7, "", "REIT" },
                    { 46, 7, "", "Learning & Development" },
                    { 47, 7, "", "Audit/ Compliances" },
                    { 48, 7, "", "Additional" },
                    { 49, 8, "", "Hiring/ de-hiring of premises/ Guest Houses" },
                    { 50, 8, "Maintaining Asset Register and tracking asset utilisation", "Procurement/Disposal of furniture, fixtures, monthly consumables" },
                    { 51, 8, "Air/rail/Cab booking", "Travel Booking" },
                    { 52, 8, "", "Handling office boys, security" },
                    { 53, 8, "", "Learning & Development" },
                    { 54, 8, "", "Audit/ Compliances" },
                    { 55, 8, "", "Additional" },
                    { 56, 9, "", "Secretarial" },
                    { 57, 9, "", "Fund Raising" },
                    { 58, 9, "", "Acquisition" },
                    { 59, 9, "", "REIT" },
                    { 60, 9, "", "Learning & Development" },
                    { 61, 9, "", "Audit & Compliances" },
                    { 62, 9, "", "Additional" },
                    { 63, 10, "", "Business Development - Pipeline creation" },
                    { 64, 10, "", "Business Development - Bidding" },
                    { 65, 10, "", "New Product/service development" },
                    { 66, 10, "", "Business Performance support and Analysis" },
                    { 67, 10, "", "Learning & Development" },
                    { 68, 10, "", "Audit & Compliances" },
                    { 69, 10, "", "Additional" },
                    { 70, 11, "", "Business Development - Pipeline creation" },
                    { 71, 11, "", "Business Development - Bidding" },
                    { 72, 11, "", "Project Quality Delivery" },
                    { 73, 11, "", "Project and Business Administration" },
                    { 74, 11, "", "Learning & Development" },
                    { 75, 11, "", "Audit & Compliances" },
                    { 76, 11, "", "Additional" },
                    { 77, 12, "", "Business Development - Pipeline creation" },
                    { 78, 12, "", "Business Development - Bidding" },
                    { 79, 12, "", "Project Quality Delivery" },
                    { 80, 12, "", "Project Commercial success" },
                    { 81, 12, "", "Learning & Development" },
                    { 82, 12, "", "Audit & Compliances" },
                    { 83, 12, "", "Additional" },
                    { 84, 13, "", "Business Development - Pipeline creation" },
                    { 85, 13, "", "Business Development - Bidding" },
                    { 86, 13, "", "Bid Quality Delivery" },
                    { 87, 13, "", "Bid Commercial success" },
                    { 88, 13, "", "Learning & Development" },
                    { 89, 13, "", "Audit & Compliances" },
                    { 90, 13, "", "Additional" },
                    { 91, 14, "", "Business Development - Pipeline creation" },
                    { 92, 14, "", "Business Development - Bidding" },
                    { 93, 14, "", "Project Quality Delivery" },
                    { 94, 14, "", "Project Commercial success" },
                    { 95, 14, "", "Learning & Development" },
                    { 96, 14, "", "Audit & Compliances" },
                    { 97, 14, "", "Additional" },
                    { 98, 15, "", "Learning & Development" },
                    { 99, 15, "", "Audit & Compliances" },
                    { 100, 15, "", "Additional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApproverId",
                table: "Employees",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_DepartmentId",
                table: "Milestones",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestones_ProjectId",
                table: "ProjectMilestones",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployeeId",
                table: "Projects",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_EmployeeId",
                table: "Timesheets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_MilesoneId",
                table: "Timesheets",
                column: "MilesoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_ProjectMilestoneId",
                table: "Timesheets",
                column: "ProjectMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_EmployeeId",
                table: "WorkingHours",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "ProjectMilestones");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
