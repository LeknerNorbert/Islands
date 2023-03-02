using DAL.DTOs;

namespace BLL.Services.BattleService
{
    public interface IBattleService
    {
        Task<BattleReportDto> GetBattleReportAsync(string username, int enemyId);
        Task<List<EnemyDto>> GetAllEnemyAsync(string username);
    }
}
