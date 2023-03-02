using DAL.DTOs;

namespace BLL.Services.ExpeditionService
{
    public interface IExpeditionService
    {
        Task<ExpeditionReportDto> GetExpeditionReportAsync(string username, int difficultyId);
    }
}
