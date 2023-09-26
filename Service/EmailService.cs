using AirsoftManager_server.Interface;
using AirsoftManager_server.Utils;
using System.Net;
using System.Net.Mail;

namespace AirsoftManager_server.Service
{
    public class EmailService : IEmail
    {
        public EmailService()
        {
        }

        public SmtpClient ConfigureSMTP(AWS_SES aws_ses_conf)
        {
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = aws_ses_conf.SES_SMTP;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential
            {
                UserName = aws_ses_conf.SES_USERNAME,
                Password = aws_ses_conf.SES_PASSWORD,
            };

            return smtpClient;
        }
        public async Task SendEmailAsync(string recipientEmail, string base64qr, SmtpClient smtpClient)
        {
           
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("sylvain.grousset1@gmail.com"),
                Subject = "test",
                Body = "<img src='" + base64qr + "' />",
                IsBodyHtml = true
            };
            mailMessage.To.Add(recipientEmail);

            await smtpClient.SendMailAsync(mailMessage);

            smtpClient.Dispose();

        }

    }
}
