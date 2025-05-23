using Microsoft.EntityFrameworkCore;
using Quartz;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet_Module.Services.Implementations;

namespace TimeSheet_Module.Services.Jobs
{
    public class Saturday : IJob
    {
        private readonly ApplicationDbContext _db;
        public Saturday(ApplicationDbContext db)
        {
            _db = db;

        }
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Running job: SundayJob");
            //SendReminderMail();
            throw new NotImplementedException();
        }
    }
}
