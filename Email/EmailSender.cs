using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Email
{
    public class EmailSender : IEmailSender
    {
        public string SmtpServer = "";
        public int SmtpPort = 0;
        public string SmtpUser = "";
        public string SmtpPassword = "";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {

                var from = new MailAddress(SmtpUser);
                var to = new MailAddress(email);
                var message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using var smtp = new SmtpClient(SmtpServer, SmtpPort)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SmtpUser, SmtpPassword)
                };

                await smtp.SendMailAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
