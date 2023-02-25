using Islands.Models.DTOs;

namespace Islands.Services.BattleService
{
    public interface IBattleService
    {
        Task<BattleResultDto> GetBattleResultAsync(string username, int enemyId);
    }
}
