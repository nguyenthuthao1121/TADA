using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;

namespace TADA.Service.Implement
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "shopTADA21@gmail.com";
            var pwd = "drmudtifljijdeke";
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = mail,
                    Password = pwd
                }
            };
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(mail);
            mailMsg.To.Add(email);
            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = message;

            return client.SendMailAsync(mailMsg);

            //return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
        }
    }
}
