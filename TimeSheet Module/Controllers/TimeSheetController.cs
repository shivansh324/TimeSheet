using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.Dto;
using TimeSheet.Models.ViewModel;
using TimeSheet.Models.API_Models;
using TimeSheet_Module.Services.Implementations;

namespace TimeSheet_Module.Controllers
{
    [Authorize]
    public class TimeSheetController(ApplicationDbContext db, PostData postData, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly PostData _postData = postData;
        private readonly IMapper _mapper = mapper;

        #region Static Methods
        private static DateTime GetStartOfWeek(DateTime date, int offset)
        {
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            return date.AddDays(-1 * diff + 7 * offset).Date;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }


        //public IActionResult SetTestData()
        //{
        //    TimeSpan temp = new TimeSpan(170, 0, 0);
        //    TimeSpan temp1 = new TimeSpan(180, 0, 0);
        //    TimeSpan temp2 = new TimeSpan(190, 0, 0);
        //    TimeSpan temp3 = new TimeSpan(200, 0, 0);
        //    _db.ProjectMilestones.Add(new ProjectMilestone { ProjectId = 1, TimeSheetNumber = "TS-001", TimeSheetLineNumber = 1000, WbsId = "W-001", MilestoneCode = "M-001", MilestoneDescription = "Test1", TaskCode = "T-001", TaskDescription = "Test1", AssignedHours = temp.Ticks, TotalWorkingHours = 0, PendingWorkingHours = temp.Ticks, Status = "Open", StartDate = DateOnly.Parse("2025-05-01"), EndDate = DateOnly.Parse("2025-06-30") });
        //    _db.ProjectMilestones.Add(new ProjectMilestone { ProjectId = 1, TimeSheetNumber = "TS-001", TimeSheetLineNumber = 1000, WbsId = "W-001", MilestoneCode = "M-002", MilestoneDescription = "Test2", TaskCode = "T-002", TaskDescription = "Test2", AssignedHours = temp1.Ticks, TotalWorkingHours = 0, PendingWorkingHours = temp1.Ticks, Status = "Open", StartDate = DateOnly.Parse("2025-05-01"), EndDate = DateOnly.Parse("2025-06-30") });
        //    _db.ProjectMilestones.Add(new ProjectMilestone { ProjectId = 2, TimeSheetNumber = "TS-001", TimeSheetLineNumber = 1000, WbsId = "W-001", MilestoneCode = "M-001", MilestoneDescription = "Test3", TaskCode = "T-001", TaskDescription = "Test1", AssignedHours = temp2.Ticks, TotalWorkingHours = 0, PendingWorkingHours = temp2.Ticks, Status = "Open", StartDate = DateOnly.Parse("2025-05-01"), EndDate = DateOnly.Parse("2025-06-30") });
        //    _db.ProjectMilestones.Add(new ProjectMilestone { ProjectId = 2, TimeSheetNumber = "TS-001", TimeSheetLineNumber = 1000, WbsId = "W-001", MilestoneCode = "M-002", MilestoneDescription = "Test4", TaskCode = "T-002", TaskDescription = "Test2", AssignedHours = temp3.Ticks, TotalWorkingHours = 0, PendingWorkingHours = temp3.Ticks, Status = "Open", StartDate = DateOnly.Parse("2025-05-01"), EndDate = DateOnly.Parse("2025-06-30") });
        //    _db.SaveChanges();
        //    return Ok("done");
        //}

        //public IActionResult SetTestData()
        //{
        //    TimeSpan temp = new TimeSpan(6, 0, 0);
        //    TimeSpan temp1 = new TimeSpan(3, 0, 0);
        //    TimeSpan temp2 = new TimeSpan(4, 0, 0);
        //    TimeSpan temp3 = new TimeSpan(2, 0, 0);
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, ProjectMilestoneId = 1, Date = DateOnly.Parse("2025-05-19"), Hours = temp.Ticks, Remarks = "Test1", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, ProjectMilestoneId = 1, Date = DateOnly.Parse("2025-05-20"), Hours = temp1.Ticks, Remarks = "Test1", IsBillable = false });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, ProjectMilestoneId = 1, Date = DateOnly.Parse("2025-05-21"), Hours = temp2.Ticks, Remarks = "Test1", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, ProjectMilestoneId = 2, Date = DateOnly.Parse("2025-05-19"), Hours = temp3.Ticks, Remarks = "Test1", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, ProjectMilestoneId = 2, Date = DateOnly.Parse("2025-05-20"), Hours = temp1.Ticks, Remarks = "Test1", IsBillable = true });
        //    _db.SaveChanges();
        //    return Ok("done");
        //}

        //public IActionResult SetTestData()
        //{
        //    TimeSpan temp = new TimeSpan(6, 0, 0);
        //    TimeSpan temp1 = new TimeSpan(3, 0, 0);
        //    TimeSpan temp2 = new TimeSpan(4, 0, 0);
        //    TimeSpan temp3 = new TimeSpan(2, 0, 0);
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 1, Date = DateOnly.Parse("2025-05-19"), Hours = temp.Ticks, Remarks = "Test1", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 2, Date = DateOnly.Parse("2025-05-19"), Hours = temp1.Ticks, Remarks = "Test2", IsBillable = false });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 3, Date = DateOnly.Parse("2025-05-19"), Hours = temp2.Ticks, Remarks = "Test3", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 4, Date = DateOnly.Parse("2025-05-19"), Hours = temp3.Ticks, Remarks = "Test4", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 1, Date = DateOnly.Parse("2025-05-20"), Hours = temp1.Ticks, Remarks = "Test5", IsBillable = false });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 2, Date = DateOnly.Parse("2025-05-20"), Hours = temp2.Ticks, Remarks = "Test6", IsBillable = true });
        //    _db.Timesheets.Add(new Timesheet { EmployeeId = 1, MilestoneId = 1, Date = DateOnly.Parse("2025-05-21"), Hours = temp3.Ticks, Remarks = "Test7", IsBillable = true });
        //    _db.SaveChanges();
        //    return Ok("done");
        //}

        #region API Calls
        [HttpPost]
        public IActionResult GetProjectTimesheet([FromBody] DataTableRequest request)
        {
            try
            {
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
                int offset = request.WeekOffset;
                DateOnly startDate = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Now, offset));
                DateOnly endDate = startDate.AddDays(6);
                List<Timesheet> timesheets = [.. _db.Timesheets.Where(x => x.EmployeeId == id).Where(x => x.ProjectMilestoneId == null || x.ProjectMilestoneId == 0).Where(x => x.Date >= startDate && x.Date <= endDate)];

                var projects = _db.Projects
                                .Where(p => p.EmployeeId == id)
                                .Include(p => p.ProjectMilestones)
                                    .ThenInclude(m => m.Timesheets)
                                .ToList();

                var projectDtos = _mapper.Map<List<ProjectDto>>(projects, opts =>
                {
                    opts.Items["startDate"] = startDate;
                    opts.Items["endDate"] = endDate;
                });
                var flattened = projectDtos.SelectMany(project => project.ProjectMilestones.Select(milestone => new
                {
                    project.ProjectCode,
                    project.ProjectDescription,
                    milestone.Id,
                    milestone.MilestoneCode,
                    milestone.MilestoneDescription,
                    milestone.TaskCode,
                    milestone.TaskDescription,
                    milestone.AssignedHours,
                    milestone.TotalWorkingHours,
                    milestone.PendingWorkingHours,
                    milestone.Timesheets
                })).ToList();
                return Json(new
                {
                    data = flattened
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult GetTimesheet([FromBody] DataTableRequest request)
        {
            try
            {
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
                Employee? employee = _db.Employees.FirstOrDefault(x => x.Id == id);
                int offset = request.WeekOffset;
                DateOnly startDate = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Now, offset));
                DateOnly endDate = startDate.AddDays(6);

                var milestone = _db.Milestones.Where(x => x.DepartmentId == employee.DepartmentId).Include(x => x.Timesheets).ToList();

                var milestoneDtos = _mapper.Map<List<MilestoneDto>>(milestone, opts =>
                {
                    opts.Items["startDate"] = startDate;
                    opts.Items["endDate"] = endDate;
                    opts.Items["EmployeeId"] = id;
                });
                return Json(new
                {
                    data = milestoneDtos
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult GetWorkingHours([FromBody] DataTableRequest request)
        {
            try
            {
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
                int offset = request.WeekOffset;
                DateOnly startDate = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Now, offset));
                DateOnly endDate = startDate.AddDays(6);
                var workingHours = _db.Employees.Where(x => x.Id == id).Include(x => x.WorkingHours).SelectMany(x => x.WorkingHours.Where(y => y.Date >= startDate && y.Date <= endDate)).ToList();
                //List<WorkingHours> workingHours = _db.WorkingHours.Where(x => x.EmployeeId == id).Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                return Json(new
                {
                    data = workingHours
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult SetTimesheet()
        {
            return Ok();
        }


        #endregion
    }
}
