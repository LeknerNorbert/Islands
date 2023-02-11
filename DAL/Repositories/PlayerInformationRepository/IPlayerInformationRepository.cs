using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.PlayerInformationRepository
{
    public interface IPlayerInformationRepository
    {
        public PlayerInformation GetPlayerInformation(string username);
        public void UpdatePlayerInformation(PlayerInformation playerInformation);
    }
}
