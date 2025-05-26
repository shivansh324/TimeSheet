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
        private static DateTime GetStartOfWeek(DateTime date)
        {
            int offset = -1;
            var diff = date.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            return date.AddDays(-1 * diff + 7 * offset).Date;
        }
        private async Task ReminderHoursMail()
        {
            DateTime presentDate = DateTime.Now;
            DateOnly startOfWeek = DateOnly.FromDateTime(GetStartOfWeek(presentDate));//ge previous week start date
            EmailSender emailSender = new EmailSender();
            List<Employee> employees = await _db.Employees.ToListAsync();



            string subject = "Action Required: Complete Your Timesheet for the Previous Week";

            foreach (Employee emp in employees)
            {
                long sum = _db.Employees.Where(x => x.Id == emp.Id).Include(x => x.WorkingHours).SelectMany(x => x.WorkingHours).Where(y => y.Date >= startOfWeek && y.Date <= startOfWeek.AddDays(6)).Sum(y => y.Hours);
                if (sum == 0) continue;
                //TimeSpan timeSpan = TimeSpan.FromTicks(sum);
                List<WorkingHours> workingHours = _db.WorkingHours.Where(x => x.EmployeeId == emp.Id).ToList();
                var daysHours = new Dictionary<DateOnly, TimeSpan>();
                foreach(var temp in workingHours)
                {
                    if (temp.Hours == 0) continue;
                    daysHours[temp.Date] = TimeSpan.FromHours(temp.Hours);
                }
                string body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Timesheet Completion Reminder</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 20px;
                            background-color: #f4f4f4;
                        }}
                        .email-container {{
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                        }}
                        .email-body {{
                            margin-top: 20px;
                            font-size: 14px;
                            color: #555;
                        }}
                        .email-body ul {{
                            margin-top: 10px;
                        }}
                        .email-body li {{
                            margin-bottom: 5px;
                        }}
                        .footer {{
                            margin-top: 30px;
                            font-size: 12px;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='email-body'>
                            <p>Dear {emp.Name},</p>
                            <p>This is an automated reminder that your timesheet for the previous week is not fully completed. There are still hours remaining to allocate from your attendance on the following days:</p>
            
                            <ul>";
                foreach (var day in daysHours)
                {
                    if (day.Value.TotalHours > 0)
                    {
                        body += $"<li><strong>{day.Key}</strong>: {day.Value} hours</li>";
                    }
                }
                body += @"
                            </ul>
                            <p>Please ensure that your timesheet is updated by <strong>end of day (EOD)</strong> today to ensure accurate allocation of your worked hours.</p>
                            <p>If you encounter any issues or have questions, please feel free to reach out to the HR or Payroll department for assistance.</p>
                            <p>Thank you for your prompt attention to this matter.</p>
                        </div>
                        <div class='footer'>
                            <p>Best regards,</p>
                            <p>[Your Company Name]</p>
                            <p>[HR/Payroll Team]</p>
                            <p>[Contact Information]</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
                await emailSender.SendEmailAsync(emp.Email, subject, body);
            }
        }
        public Task Execute(IJobExecutionContext context)
        {
            var triggerName = context.Trigger.Key.Name;
            if (triggerName == "MondayMorning-trigger")
            {
                Console.WriteLine("Running job: MondayMorningWeeksJob");
                //ReminderHoursMail();
            }
            else if (triggerName == "MondayNight-trigger")
            {
                Console.WriteLine("Running job: MondayNightJob");
                //AutoSubmit();
            }

            return Task.CompletedTask;
        }
    }
}
