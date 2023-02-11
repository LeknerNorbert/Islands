using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.ClassifiedAdService
{
    public interface IClassifiedAdService
    {
        public List<ClassifiedAdDto> GetClassifiedAds();
        public List<ClassifiedAdDto> GetMyClassifiedAds(string username);
        public void CreateClassifiedAd(string username, CreateClassifiedAdDto createClassifiedAd);
        public void DeleteClassifiedAd(int id);
    }
}
