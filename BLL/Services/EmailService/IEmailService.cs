using DAL.DTOs;

namespace BLL.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);
    }
}
