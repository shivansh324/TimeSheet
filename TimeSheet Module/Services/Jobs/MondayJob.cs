using Microsoft.EntityFrameworkCore;
using Quartz;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.API_Models;
using TimeSheet_Module.Services.Implementations;

namespace TimeSheet_Module.Services.Jobs
{
    public class MondayJob : IJob
    {
        private readonly ApplicationDbContext _db;
        private readonly PostData _postData;
        public MondayJob(ApplicationDbContext db, PostData postData)
        {
            _db = db;
            _postData = postData;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var triggerName = context.Trigger.Key.Name;
            if (triggerName == "MondayMorning-trigger")
            {
                Console.WriteLine("Running job: MondayMorningWeeksJob");
                //AddWeek();
                //ReminderHoursMail();
            }
            else if (triggerName == "MondayNight-trigger")
            {
                Console.WriteLine("Running job: MondayNightJob");
                //ResetWorkingHours();
                //AutoSubmit();
            }

            return Task.CompletedTask;
        }
    }
}
