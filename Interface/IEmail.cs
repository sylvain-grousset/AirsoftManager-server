using AirsoftManager_server.Utils;
using System.Net.Mail;

namespace AirsoftManager_server.Interface
{
    public interface IEmail
    {
        SmtpClient ConfigureSMTP(AWS_SES aws_ses_conf);
        Task SendEmailAsync(string recipientEmail, string base64qr, SmtpClient smtpClient);
    }
}
