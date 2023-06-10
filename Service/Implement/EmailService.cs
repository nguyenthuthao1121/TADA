using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;
using System.Text;

namespace TADA.Service.Implement
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
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
            catch(Exception)
            {
                return Task.CompletedTask;
            }
        }
        public string EncryptEmail(string emailAddress)
        {
            byte[] data = Encoding.UTF8.GetBytes(emailAddress);
            string encryptedEmail = Convert.ToBase64String(data);
            return encryptedEmail;
        }
        public string DecryptEmail(string encryptedEmail)
        {
            byte[] data = Convert.FromBase64String(encryptedEmail);
            string decryptedEmail = Encoding.UTF8.GetString(data);
            return decryptedEmail;
        }
        public string ConvertHtmlToString(string html)
        {
            StreamReader sr = new(html); 
            string s = sr.ReadToEnd();
            s = s.Replace("\r\n", "\n");
            return s;
        }
        public string MessageEmailForActiveAccount(string convertedHtml, string email, string otp)
        {
            var s = convertedHtml;
            s = s.Replace("{email}", email);
            s = s.Replace("{otp}", otp);
            return s;
        }
    }
}
