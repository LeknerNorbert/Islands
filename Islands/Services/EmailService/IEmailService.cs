using Islands.DTOs;

namespace Islands.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);
    }
}
