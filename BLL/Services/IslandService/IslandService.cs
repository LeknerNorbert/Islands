using BLL.DTOs;
using DAL.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IslandService
{
    public class IslandService : IIslandService
    {
        public SkillsDto GetDefaultSkills(IslandType island)
        {
            SkillsDto? skills = JsonConvert.DeserializeObject<SkillsDto>($"ConfigFiles/DefaultSkills/{island}DefaultSkillsConfig.json");
            if (skills == null)
            {
                throw new ArgumentNullException("Config file does not exist.");
            }
            return skills;
        }
    }
}
