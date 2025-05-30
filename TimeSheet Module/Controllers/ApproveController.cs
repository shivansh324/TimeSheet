using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.Dto;
using TimeSheet.Models.ViewModel;

namespace TimeSheet_Module.Controllers
{
    [Authorize]
    public class ApproveController(ApplicationDbContext db, IMapper mapper) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IMapper _mapper = mapper;

        private static DateTime GetStartOfWeek(DateTime date, int offset)
        {
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            return date.AddDays(-1 * diff + 7 * offset).Date;
        }

        public async Task<IActionResult> Index()
        {
            int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
            return View(await _db.SubmissionLogs.Include(x => x.Employee).Where(x => x.Employee.ApproverId == id).OrderByDescending(x => x.SubmissionDate).ToListAsync());
        }

        public async Task<IActionResult> History()
        {
            int id = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
            if (User.IsInRole("Admin"))
            {
                return View(await _db.SubmissionLogs.Include(x=>x.Approver).Include(x => x.Employee).OrderByDescending(x => x.SubmissionDate).ToListAsync());
            }
            return View(await _db.SubmissionLogs.Where(x => x.EmployeeId == id).Include(x=>x.Approver).Include(x => x.Employee).OrderByDescending(x => x.SubmissionDate).ToListAsync());
        }
        public async Task<IActionResult> Info(int id)
        {
            try
            {
                SubmissionLog? submissionLog = await _db.SubmissionLogs.FirstOrDefaultAsync(x => x.Id == id);
                if (submissionLog == null)
                {
                    return NotFound("Submission Log not found");
                }
                Employee? employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == submissionLog.EmployeeId);
                if (employee == null)
                {
                    return NotFound("Employee not found");

                }
                ViewBag.SubmissionLog = submissionLog;
                ViewBag.Employee = employee;
                DateOnly startOfWeek = DateOnly.FromDateTime(GetStartOfWeek(DateTime.Now, 0));
                int offset = (submissionLog.TimesheetDate.DayNumber - startOfWeek.DayNumber) / 7;
                ViewBag.Offset = offset;
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetData(int id)
        {
            try
            {

                SubmissionLog? submissionLog = await _db.SubmissionLogs.FirstOrDefaultAsync(x => x.Id == id);
                if (submissionLog == null)
                {
                    return NotFound();
                }
                Employee? employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == submissionLog.EmployeeId);
                if (employee == null)
                {
                    return NotFound();

                }
                DateOnly startDate = submissionLog.TimesheetDate;
                DateOnly endDate = startDate.AddDays(6);

                List<WorkingHours> workingHours = _db.Employees.Where(x => x.Id == employee.Id).Include(x => x.WorkingHours).SelectMany(x => x.WorkingHours.Where(y => y.Date >= startDate && y.Date <= endDate)).ToList();
                List<WorkingHoursInfo> hours = workingHours.Select(x => new WorkingHoursInfo { Hours = x.Hours, Date = x.Date }).ToList();
                List<WorkingHoursInfo> hoursLeft = workingHours.Select(x => new WorkingHoursInfo { Hours = x.HoursLeft, Date = x.Date }).ToList();
                var flattenedHours = new[] { new { type = "Total Working Hours",hours = hours },
                    new {type = "Pending Working Hours", hours = hoursLeft } };
                var projects = await _db.Projects
                                .Where(p => p.EmployeeId == employee.Id)
                                .Include(p => p.ProjectMilestones)
                                    .ThenInclude(m => m.Timesheets)
                                .ToListAsync();

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
                })).ToList();

                var milestone = await _db.Milestones.Where(x => x.Status == "Active").Where(x => x.DepartmentId == employee.DepartmentId).Include(x => x.Timesheets).ToListAsync();
                var milestoneDtos = milestone
                    .Select(m => _mapper.Map<MilestoneDto>(m, opts =>
                    {
                        opts.Items["startDate"] = startDate;
                        opts.Items["endDate"] = endDate;
                        opts.Items["EmployeeId"] = employee.Id;
                    })).ToList();

                return Json(new
                {
                    workingHours = flattenedHours,
                    projectMilestone = flattened,
                    milestone = milestoneDtos
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string id, string approvalStatus, string remarks)
        {
            try
            {
                SubmissionLog? submissionLog = await _db.SubmissionLogs.Where(x => x.Id == int.Parse(id)).OrderByDescending(x => x.SubmissionDate).FirstOrDefaultAsync();
                if (submissionLog == null)
                {
                    return NotFound("Submission Log not found");
                }
                submissionLog.ApproverId = int.Parse(User?.FindFirst(ClaimTypes.Name).Value);
                submissionLog.Status = approvalStatus;
                submissionLog.RejectionRemarks = remarks;
                submissionLog.ApprovedDate = DateTime.Now;
                _db.SubmissionLogs.Update(submissionLog);
                await _db.SaveChangesAsync();
                TempData["success"] = "Timesheet Updated Successfully";
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { error = "Internal Server Error" });
            }
        }
    }
}
