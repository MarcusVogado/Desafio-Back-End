using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Back_End.Repositories.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _emailSettings;
        public EmailServices(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendToEmail(string title, string body)
        {
            var sendMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.EmailFromAddress),
                Subject = title,
                Body = body,
                IsBodyHtml =false
            };
            sendMessage.To.Add(_emailSettings.EmailDestination);

            using (var smtpClient = new SmtpClient(_emailSettings.ServerSmtp,_emailSettings.Port))
            {   
                smtpClient.Credentials= new NetworkCredential(_emailSettings.UserName,_emailSettings.Password);
                smtpClient.EnableSsl = _emailSettings.EnableSsl;
                await smtpClient.SendMailAsync(sendMessage);
            }
        }
    }
}
