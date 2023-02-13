using BLL.DTOs;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.PlayerInformationService
{
    public interface IPlayerService
    {
        public PlayerDto GetPlayer(string username);
        public void CreatePlayer(string username, IslandType island);
    }
}
