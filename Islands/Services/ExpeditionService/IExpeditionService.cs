using Islands.Models.DTOs;

namespace Islands.Services.ExpeditionService
{
    public interface IExpeditionService
    {
        Task<ExpeditionResultDto> GetExpeditionResultAsync(int id, int difficultyId);
    }
}
