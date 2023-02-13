using BLL.DTOs;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IslandService
{
    public interface IIslandService
    {
        public SkillsDto GetDefaultSkills(IslandType island);
    }
}
