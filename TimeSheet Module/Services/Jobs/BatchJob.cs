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
    public class BatchJob : IJob
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClientZING;
        private readonly HttpClient _httpClientBC;

        
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
