using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.API_Models;
using TimeSheet_Module.Services.Implementations;


namespace TimeSheet_Module.Controllers
{
    //[Authorize]
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationDbContext _db = db;
        private readonly HttpClient _httpClientZING = httpClientFactory.CreateClient("Zing_HR");
        private readonly HttpClient _httpClientBC = httpClientFactory.CreateClient("BC_Client");

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region  Get Employees
        //[AllowAnonymous]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    var requestData = new
        //    {
        //        Token = "d8dfad60195149f3b85deed326ae67ab",
        //        SubscriptionName = "REPL",
        //        Actionfromdate = "28-05-2025",
        //        Actiontodate = "28-05-2025"
        //    };
        //    var jsonContent = new StringContent(
        //        JsonSerializer.Serialize(requestData),
        //        Encoding.UTF8,
        //        "application/json"
        //    );
        //    try
        //    {
        //        HttpResponseMessage api_response = await _httpClientZING.PostAsync("https://portal.zinghr.com/2015/route/EmployeeDetails/GetEmployeeMasterDetails", jsonContent);
        //        EmployeeMaster? employeeMaster = JsonSerializer.Deserialize<EmployeeMaster>(api_response.Content.ReadAsStringAsync().Result);
        //        foreach (var emp in employeeMaster.Employees)
        //        {
        //            Employee employee = new()
        //            {
        //                EmployeeCode = emp.EmployeeCode,
        //                EmployeeID = emp.EmployeeID,
        //                Name = emp.EmployeeName,
        //                Email = emp.Email
        //                //ApproverId = _db.Employees.FirstOrDefault(x => x.EmployeeCode == emp.ApproverDetails.ApproverEmployeeCode)?.Id
        //            };
        //            _db.Employees.Add(employee);
        //        }
        //        _db.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}
        #endregion
        #region Set Projects
        //[AllowAnonymous]
        //public async Task<IActionResult> SetProjects()
        //{
        //    List<string> left_code = new List<string>();
        //    string api_url = $"http://192.168.100.26:9148/BC230/ODataV4/Company('REPL')/TimesheetData?$filter=TDate%20ge%202025-05-01%20and%20TDate%20le%202025-05-30";
        //    List<BCProject> projects_list = new List<BCProject>();
        //    while (!string.IsNullOrEmpty(api_url))
        //    {
        //        HttpResponseMessage api_response = await _httpClientBC.GetAsync(api_url);
        //        if (!api_response.IsSuccessStatusCode)
        //        {
        //            Console.WriteLine($"API call failed: {api_response.StatusCode} - {api_response.ReasonPhrase}");
        //            return Json("Error");
        //        }
        //        BCProjectModel timesheetData = JsonSerializer.Deserialize<BCProjectModel>(api_response.Content.ReadAsStringAsync().Result);
        //        projects_list.AddRange(timesheetData.value);
        //        if (timesheetData.ODataNextLink != null)
        //        {
        //            api_url = timesheetData.ODataNextLink;
        //        }
        //        else
        //        {
        //            api_url = null;
        //        }
        //    }
        //    foreach (var data in projects_list)
        //    {
        //        Employee emp = _db.Employees.FirstOrDefault(x => x.EmployeeCode == data.employeeCode);
        //        if (emp == null)
        //        {
        //            left_code.Add(data.employeeCode);
        //            continue;
        //        }
        //        Project? project = _db.Projects.FirstOrDefault(x => x.ProjectCode == data.projectCode && x.EmployeeId == emp.Id);
        //        if (project == null)
        //        {
        //            project = new()
        //            {
        //                ProjectCode = data.projectCode,
        //                ProjectDescription = data.projectName,
        //                StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectStartDate != "" ? data.ProjectStartDate : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
        //                EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectEndDate != "" ? data.ProjectEndDate : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
        //                EmployeeId = emp.Id
        //            };
        //            _db.Projects.Add(project);
        //            _db.SaveChanges();
        //        }
        //        else
        //        {
        //            bool isModified = false;
        //            if (!string.IsNullOrWhiteSpace(data.projectName))
        //            {
        //                project.ProjectDescription = data.projectName;
        //                isModified = true;
        //            }
        //            if (!string.IsNullOrWhiteSpace(data.ProjectStartDate))
        //            {
        //                project.StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectStartDate, "MM/dd/yy", CultureInfo.InvariantCulture));
        //                isModified = true;
        //            }
        //            if (!string.IsNullOrWhiteSpace(data.ProjectEndDate))
        //            {
        //                project.EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectEndDate, "MM/dd/yy", CultureInfo.InvariantCulture));
        //                isModified = true;
        //            }
        //            if (isModified)
        //            {
        //                _db.Update(project);
        //                _db.SaveChanges();
        //            }
        //        }
        //        ProjectMilestone? milestone = _db.ProjectMilestones.FirstOrDefault(x => x.ProjectId == project.Id && x.MilestoneCode == data.milestone);
        //        if (milestone == null)
        //        {
        //            milestone = new()
        //            {
        //                ProjectId = project.Id,
        //                TimeSheetNumber = data.Time_Sheet_No,
        //                TimeSheetLineNumber = data.Time_Sheet_Line_No,
        //                WbsId = data.wbsId,
        //                MilestoneCode = data.milestone,
        //                MilestoneDescription = data.milestoneDescription,
        //                TaskCode = data.taskCode,
        //                TaskDescription = data.taskDescription,
        //                AssignedHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks,
        //                PendingWorkingHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks,
        //                StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonStartDt != "" ? data.MileStonStartDt : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
        //                EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonEndDt != "" ? data.MileStonEndDt : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture))
        //            };
        //            _db.ProjectMilestones.Add(milestone);
        //            _db.SaveChanges();
        //        }
        //        else if (milestone != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(data.milestoneDescription))
        //            {
        //                milestone.MilestoneDescription = data.milestoneDescription;
        //            }
        //            if (!string.IsNullOrWhiteSpace(data.taskDescription))
        //            {
        //                milestone.TaskDescription = data.taskDescription;
        //            }
        //            if (!string.IsNullOrWhiteSpace(data.MileStonStartDt))
        //            {
        //                milestone.StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonStartDt, "MM/dd/yy", CultureInfo.InvariantCulture));
        //            }
        //            if (!string.IsNullOrWhiteSpace(data.MileStonEndDt))
        //            {
        //                milestone.EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonEndDt, "MM/dd/yy", CultureInfo.InvariantCulture));
        //            }
        //            milestone.AssignedHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks;
        //            milestone.PendingWorkingHours = (TimeSpan.FromDays(data.totalDaysAssigned) - TimeSpan.FromTicks(milestone.TotalWorkingHours)).Ticks;
        //            _db.ProjectMilestones.Update(milestone);
        //            _db.SaveChanges();
        //        }
        //    }
        //    return Json(new { Done = "Done", left_code });
        //}

        #endregion

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {

                _logger.LogError(exceptionFeature.Error, "Unhandled exception occurred");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
