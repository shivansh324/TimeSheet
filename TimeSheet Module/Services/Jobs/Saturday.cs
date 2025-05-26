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
        private async Task SendReminderMail()
        {
            EmailSender emailSender = new EmailSender();
            List<Employee> employees = await _db.Employees.ToListAsync();

            string subject = "Gentle Reminder: Please Complete Your Timesheet";
            foreach (Employee emp in employees)
            {
                string body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Timesheet Reminder</title>
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
                            <p>We hope you're doing well. This is just a friendly reminder to complete your timesheet for the this week from {DateOnly.FromDateTime(DateTime.Now.AddDays(-5))} to {DateOnly.FromDateTime(DateTime.Now.AddDays(-1))} if you haven't already.</p>
                            <p>Having an updated timesheet helps us maintain accurate records. Please take a moment to fill it out when you have the chance.</p>
                            <p>If you need any assistance or have questions, feel free to reach out to the HR or Payroll department.</p>
                            <p>Thank you for your attention to this! Your cooperation is greatly appreciated.</p>
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
                string reciever = emp.Email;
                await emailSender.SendEmailAsync(reciever, subject, body);
            }
        }
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Running job: SundayJob");
            //SendReminderMail();
            throw new NotImplementedException();
        }
    }
}
