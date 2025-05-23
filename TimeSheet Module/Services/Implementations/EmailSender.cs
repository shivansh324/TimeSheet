using System.Net.Mail;
using System.Net;
using TimeSheet_Module.Services.Interfaces;

namespace TimeSheet_Module.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "support@vtsinfosoft.com";

            var pw = "lvtbmfjfjjnmzbrj";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);
            return client.SendMailAsync(mailMessage);
        }
    }
}
