using Islands.DTOs;
using Islands.Models.Enums;

namespace Islands.Services.PlayerInformationService
{
    public interface IPlayerService
    {
        public PlayerDTO GetPlayer(string username);
        public void CreatePlayer(string username, IslandType island);
        public void UpdateSkillPoints(SkillsDTO skills);
    }
}
