using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;

namespace TADA.Service.Implement
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "thoaivytruong3102003@gmail.com";
            var pwd = "chljossidjrfpzmu";
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
            return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
        }
    }
}
