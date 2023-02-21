using Islands.Models.DTOs;

namespace Islands.Services.BattleService
{
    public interface IBattleService
    {
        Task<BattleResultDto> GetBattleResultAsync(int id, int enemyId);
    }
}
