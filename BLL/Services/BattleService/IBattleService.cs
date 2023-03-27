using DAL.DTOs;

namespace BLL.Services.BattleService
{
    public interface IBattleService
    {
        Task<BattleReportDto> GetBattleReportAsync(string username, string enemyUsername);
        Task<List<EnemyDto>> GetAllEnemiesAsync(string username);
    }
}
