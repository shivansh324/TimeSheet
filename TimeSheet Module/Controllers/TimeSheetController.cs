using System.Diagnostics.Contracts;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.API_Models;
using TimeSheet.Models.Dto;
using TimeSheet.Models.ViewModel;

namespace TimeSheet_Module.Controllers
{
    [Authorize]
    public class TimeSheetController(ApplicationDbContext db, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        #region Static Methods
        private static DateTime GetStartOfWeek(DateTime date, int offset)
        {
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            return date.AddDays(-1 * diff + 7 * offset).Date;
        }
        #endregion

        public IActionResult Index(int offset = 0)
        {
            ViewBag.Offset = offset;
            return View();
        }

        #region Setting Test Data
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
        #endregion

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
                SubmissionLog? submissionLog = _db.SubmissionLogs.Where(x => x.EmployeeId == id && x.TimesheetDate == startDate).OrderByDescending(x => x.SubmissionDate).FirstOrDefault();
                string status = submissionLog != null ? submissionLog.Status : "Open";

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
                    milestone.Timesheets,
                    status
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
                SubmissionLog? submissionLog = _db.SubmissionLogs.Where(x => x.EmployeeId == id && x.TimesheetDate == startDate).OrderByDescending(x => x.SubmissionDate).FirstOrDefault();
                string status = submissionLog != null ? submissionLog.Status : "Open";

                var milestone = _db.Milestones.Where(x => x.Status == "Active").Where(x => x.DepartmentId == employee.DepartmentId).Include(x => x.Timesheets).ToList();
                var milestoneDtos = milestone
                    .Select(m => _mapper.Map<MilestoneDto>(m, opts =>
                    {
                        opts.Items["startDate"] = startDate;
                        opts.Items["endDate"] = endDate;
                        opts.Items["EmployeeId"] = id;
                        opts.Items["Status"] = status;
                    })).ToList();
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
        public IActionResult GetWorkingHours([FromBody] DataTableRequest request) //Get Working Hours and Week's Status
        {
            try
            {
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
                int offset = request.WeekOffset;
                DateOnly startDate = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Now, offset));
                DateOnly endDate = startDate.AddDays(6);
                List<WorkingHours> workingHours = _db.Employees.Where(x => x.Id == id).Include(x => x.WorkingHours).SelectMany(x => x.WorkingHours.Where(y => y.Date >= startDate && y.Date <= endDate)).ToList();
                List<WorkingHoursInfo> hours = workingHours.Select(x => new WorkingHoursInfo { Hours = x.Hours, Date = x.Date }).ToList();
                List<WorkingHoursInfo> hoursLeft = workingHours.Select(x => new WorkingHoursInfo { Hours = x.HoursLeft, Date = x.Date }).ToList();
                SubmissionLog? submissionLog = _db.SubmissionLogs.Where(x => x.EmployeeId == id && x.TimesheetDate == startDate).OrderByDescending(x => x.SubmissionDate).FirstOrDefault();
                var data = new[] { new { type = "Total Working Hours",hours = hours, submissionLog = submissionLog },
                    new {type = "Pending Working Hours", hours = hoursLeft, submissionLog = submissionLog } };
                return Json(new
                {
                    data = data
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult SetTimesheet(int MilestoneId, int TimesheetId, string Date, TimeSpan Hours, string Remarks, bool IsBillable, bool IsProject)
        {
            try
            {
                var temp_date = DateOnly.Parse(Date);
                Milestone? milestone = null;
                ProjectMilestone? projectMilestone = null;
                Timesheet? timesheet = null;
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);//Employee ID
                if (IsProject)
                {
                    projectMilestone = _db.Find<ProjectMilestone>(MilestoneId);
                    if (temp_date < projectMilestone.StartDate)
                    {
                        return StatusCode(400, new { error = $"You cannot add timesheet before the starting date of Project's Milestone i.e {projectMilestone.StartDate}" });
                    }
                    else if (temp_date > projectMilestone.EndDate)
                    {
                        return StatusCode(400, new { error = $"You cannot add timesheet after the end date of Project's Milestone i.e {projectMilestone.EndDate}" });
                    }
                }
                else
                {
                    milestone = _db.Find<Milestone>(MilestoneId);
                }
                if (TimesheetId != 0)
                {
                    timesheet = _db.Find<Timesheet>(TimesheetId);
                }
                WorkingHours? workingHours = _db.WorkingHours.FirstOrDefault(x => x.EmployeeId == id && x.Date == DateOnly.Parse(Date));
                if (workingHours == null)
                {
                    return StatusCode(404, new { error = "Working Hours not found, please try again later" });
                }
                if (timesheet != null && _db.SubmissionLogs.Any(x => x.EmployeeId == id && x.TimesheetDate == DateOnly.FromDateTime(GetStartOfWeek(DateTime.Parse(Date), 0)) && (x.Status == "Pending" || x.Status == "Approved")))
                {
                    return StatusCode(400, new { error = "Timesheet already submitted. Can't update it." });
                }
                //if (timesheet != null && timesheet.Status == "Pending")
                //{
                //    return StatusCode(400, new { error = "Timesheet already submitted. Can't update it." });
                //}
                if (projectMilestone != null && Hours.Ticks >= projectMilestone.PendingWorkingHours)
                {
                    return StatusCode(400, new { error = "You cannot add more than Pending working hours" });
                }

                if (timesheet != null)
                {
                    long temp_hours = timesheet.Hours;
                    if (Hours.Ticks > workingHours.HoursLeft + temp_hours)
                    {
                        return StatusCode(400, new { error = "You cannot add more than Actual working hours" });
                    }
                    workingHours.HoursLeft -= (Hours.Ticks - temp_hours);
                    timesheet.Hours = Hours.Ticks;
                    timesheet.Remarks = Remarks;
                    timesheet.IsBillable = IsBillable;
                    if (projectMilestone != null)
                    {
                        projectMilestone.PendingWorkingHours -= (Hours.Ticks - temp_hours);
                        projectMilestone.TotalWorkingHours += (Hours.Ticks - temp_hours);
                        _db.ProjectMilestones.Update(projectMilestone);
                    }
                    _db.Timesheets.Update(timesheet);
                    _db.WorkingHours.Update(workingHours);
                    _db.SaveChanges();
                }
                else
                {
                    if (Hours.Ticks > workingHours.Hours)
                    {
                        return StatusCode(400, new { error = "You cannot add more than Actual working hours" });
                    }
                    Timesheet newTimesheet = new Timesheet
                    {
                        EmployeeId = id,
                        MilestoneId = IsProject ? null : MilestoneId,
                        ProjectMilestoneId = IsProject ? MilestoneId : null,
                        Date = DateOnly.Parse(Date),
                        Hours = Hours.Ticks,
                        Remarks = Remarks,
                        IsBillable = IsBillable
                    };
                    if (projectMilestone != null)
                    {
                        projectMilestone.PendingWorkingHours -= Hours.Ticks;
                        projectMilestone.TotalWorkingHours += Hours.Ticks;
                        _db.ProjectMilestones.Update(projectMilestone);
                    }
                    _db.Timesheets.Add(newTimesheet);
                    workingHours.HoursLeft -= Hours.Ticks;
                    _db.WorkingHours.Update(workingHours);
                    _db.SaveChanges();
                }
                return Json(new { success = "true", message = "Done" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Submit(string date)
        {
            try
            {
                int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);//Employee ID
                DateOnly startDate = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Parse(date), 0));
                if (startDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    return StatusCode(400, new { error = "You cannot submit timesheet for future dates." });
                }
                long sum = _db.Employees.Where(x => x.Id == id).Include(x => x.WorkingHours)
                    .SelectMany(x => x.WorkingHours)
                    .Where(y => y.Date >= startDate && y.Date <= startDate.AddDays(6))
                    .Sum(y => y.HoursLeft);
                if (sum != 0)
                {
                    return StatusCode(400, new { error = "You have unallocated working hours, please allocate them first." });
                }
                long totalHours = _db.Timesheets.Where(x => x.EmployeeId == id && x.Date >= startDate && x.Date <= startDate.AddDays(6)).Sum(x => x.Hours);
                if (_db.SubmissionLogs.Any(x => x.EmployeeId == id && x.TimesheetDate == startDate && (x.Status == "Pending" || x.Status == "Approved")))
                {
                    return StatusCode(400, new { error = "Timesheet already submitted for this week." });
                }
                SubmissionLog? existingLog = _db.SubmissionLogs.Where(x => x.EmployeeId == id && x.TimesheetDate == startDate).OrderByDescending(x => x.SubmissionDate).FirstOrDefault();
                if (existingLog != null)
                {
                    existingLog.IsClosed = true;
                    _db.SubmissionLogs.Update(existingLog);
                    _db.SaveChanges();
                }
                SubmissionLog submissionLog = new SubmissionLog
                {
                    EmployeeId = id,
                    TimesheetDate = startDate,
                    Hours = totalHours,
                    Status = "Pending"
                };
                _db.SubmissionLogs.Add(submissionLog);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
        #endregion
    }
}
