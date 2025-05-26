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
                Employee? emp = _db.Employees.FirstOrDefault(x => x.EmployeeCode == temp.Employeecode);
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
                if(_db.WorkingHours.Any(x => x.EmployeeId == emp.Id && x.Date == date))
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
