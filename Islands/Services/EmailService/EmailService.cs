using Islands.DTOs;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Islands.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(EmailDto request)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("leknern@gmail.com"));
            message.To.Add(MailboxAddress.Parse(request.Email));
            message.Subject = request.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUsername").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
