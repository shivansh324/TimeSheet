using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.API_Models;

namespace TimeSheet_Module.Services.Jobs
{
    public class BatchJob(ApplicationDbContext db, IHttpClientFactory httpClientFactory) : IJob
    {
        private readonly ApplicationDbContext _db = db;
        private readonly HttpClient _httpClientZING = httpClientFactory.CreateClient("Zing_HR");
        private readonly HttpClient _httpClientBC = httpClientFactory.CreateClient("BC_Client");

        private async Task<Attendance> GetHours()
        {
            var requestData = new
            {
                Token = "d8dfad60195149f3b85deed326ae67ab",
                SubscriptionName = "REPL",
                Actionfromdate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy"),
                Actiontodate = DateOnly.FromDateTime(DateTime.Now).ToString("dd-MM-yyyy")
            };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestData),
                Encoding.UTF8,
                "application/json"
            );
            try
            {
                HttpResponseMessage api_response = await _httpClientZING.PostAsync("https://portal.zinghr.com/2015/route/TNA/Employeeattendancedetails", jsonContent);
                Attendance attendance = JsonSerializer.Deserialize<Attendance>(api_response.Content.ReadAsStringAsync().Result);
                return attendance;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task GetWorkingHours()
        {
            Attendance attendance = await GetHours();
            foreach (var temp in attendance.AttendanceDetails)
            {
                DateOnly date = DateOnly.Parse(temp.Shiftdate);
                Employee? emp = _db.Employees.Where(x => x.Status == "Active").FirstOrDefault(x => x.EmployeeCode == temp.Employeecode);
                if (emp == null)
                {
                    continue;
                }
                DateTime inTime = DateTime.Parse(temp.Actualintime);
                DateTime outTime = DateTime.Parse(temp.Actualouttime);
                if (temp.Actualouttime == "" || temp.Actualintime == "")
                {
                    continue;
                }
                if (inTime > outTime)
                {
                    continue;
                }
                TimeSpan hours = (outTime - inTime);
                if (_db.WorkingHours.Any(x => x.EmployeeId == emp.Id && x.Date == date))
                {
                    continue;
                }
                WorkingHours workingHours = new()
                {
                    EmployeeId = emp.Id,
                    Date = date,
                    Hours = hours.Ticks,
                };
                _db.WorkingHours.Add(workingHours);
                _db.SaveChanges();
            }
        }

        public async Task GetProjects()
        {
            List<string> left_code = new List<string>();
            string api_url = $"http://192.168.100.26:9148/BC230/ODataV4/Company('REPL')/TimesheetData?$filter=TDate%20ge%20{DateOnly.FromDateTime(DateTime.Now)}%20and%20TDate%20le%20{DateOnly.FromDateTime(DateTime.Now)}";
            List<BCProject> projects_list = new List<BCProject>();
            while (!string.IsNullOrEmpty(api_url))
            {
                HttpResponseMessage api_response = await _httpClientBC.GetAsync(api_url);
                if (!api_response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API call failed: {api_response.StatusCode} - {api_response.ReasonPhrase}");
                }
                BCProjectModel timesheetData = JsonSerializer.Deserialize<BCProjectModel>(api_response.Content.ReadAsStringAsync().Result);
                projects_list.AddRange(timesheetData.value);
                if (timesheetData.ODataNextLink != null)
                {
                    api_url = timesheetData.ODataNextLink;
                }
                else
                {
                    api_url = null;
                }
            }
            foreach (var data in projects_list)
            {
                Employee? emp = _db.Employees.Where(x => x.Status == "Active").FirstOrDefault(x => x.EmployeeCode == data.employeeCode);
                if (emp == null)
                {
                    left_code.Add(data.employeeCode);
                    continue;
                }
                Project? project = _db.Projects.FirstOrDefault(x => x.ProjectCode == data.projectCode && x.EmployeeId == emp.Id);
                if (project == null)
                {
                    project = new()
                    {
                        ProjectCode = data.projectCode,
                        ProjectDescription = data.projectName,
                        StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectStartDate != "" ? data.ProjectStartDate : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
                        EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectEndDate != "" ? data.ProjectEndDate : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
                        EmployeeId = emp.Id
                    };
                    await _db.Projects.AddAsync(project);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    bool isModified = false;
                    if (!string.IsNullOrWhiteSpace(data.projectName))
                    {
                        project.ProjectDescription = data.projectName;
                        isModified = true;
                    }
                    if (!string.IsNullOrWhiteSpace(data.ProjectStartDate))
                    {
                        project.StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectStartDate, "MM/dd/yy", CultureInfo.InvariantCulture));
                        isModified = true;
                    }
                    if (!string.IsNullOrWhiteSpace(data.ProjectEndDate))
                    {
                        project.EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.ProjectEndDate, "MM/dd/yy", CultureInfo.InvariantCulture));
                        isModified = true;
                    }
                    if (isModified)
                    {
                        _db.Update(project);
                        await _db.SaveChangesAsync();
                    }
                }
                ProjectMilestone? milestone = _db.ProjectMilestones.FirstOrDefault(x => x.ProjectId == project.Id && x.MilestoneCode == data.milestone);
                if (milestone == null)
                {
                    milestone = new()
                    {
                        ProjectId = project.Id,
                        TimeSheetNumber = data.Time_Sheet_No,
                        TimeSheetLineNumber = data.Time_Sheet_Line_No,
                        WbsId = data.wbsId,
                        MilestoneCode = data.milestone,
                        MilestoneDescription = data.milestoneDescription,
                        TaskCode = data.taskCode,
                        TaskDescription = data.taskDescription,
                        AssignedHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks,
                        PendingWorkingHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks,
                        StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonStartDt != "" ? data.MileStonStartDt : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture)),
                        EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonEndDt != "" ? data.MileStonEndDt : "01/01/01", "MM/dd/yy", CultureInfo.InvariantCulture))
                    };
                    await _db.ProjectMilestones.AddAsync(milestone);
                    await _db.SaveChangesAsync();
                }
                else if (milestone != null)
                {
                    if (!string.IsNullOrWhiteSpace(data.milestoneDescription))
                    {
                        milestone.MilestoneDescription = data.milestoneDescription;
                    }
                    if (!string.IsNullOrWhiteSpace(data.taskDescription))
                    {
                        milestone.TaskDescription = data.taskDescription;
                    }
                    if (!string.IsNullOrWhiteSpace(data.MileStonStartDt))
                    {
                        milestone.StartDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonStartDt, "MM/dd/yy", CultureInfo.InvariantCulture));
                    }
                    if (!string.IsNullOrWhiteSpace(data.MileStonEndDt))
                    {
                        milestone.EndDate = DateOnly.FromDateTime(DateTime.ParseExact(data.MileStonEndDt, "MM/dd/yy", CultureInfo.InvariantCulture));
                    }
                    milestone.AssignedHours = TimeSpan.FromDays(data.totalDaysAssigned).Ticks;
                    milestone.PendingWorkingHours = (TimeSpan.FromDays(data.totalDaysAssigned) - TimeSpan.FromTicks(milestone.TotalWorkingHours)).Ticks;
                    _db.ProjectMilestones.Update(milestone);
                    await _db.SaveChangesAsync();
                }
            }            
        }
        public Task Execute(IJobExecutionContext context)
        {

            var triggerName = context.Trigger.Key.Name;
            if (triggerName == "BatchJob-trigger")
            {
                Console.WriteLine("Running job: 30MinProjectBatchJob");
                //GetProjects();
            }
            else if (triggerName == "EveningJob-trigger")
            {
                Console.WriteLine("Running job: EveningAttendanceJob");
                //GetWorkingHours();
            }
            return Task.CompletedTask;
        }
    }
}
