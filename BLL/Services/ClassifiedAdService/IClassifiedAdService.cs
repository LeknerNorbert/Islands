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
        public List<ClassifiedAdDto> GetClassifiedAdsByUser(string username);
        public void CreateClassifiedAd(string creatorUsername, CreateClassifiedAdDto createClassifiedAd);
        public void DeleteClassifiedAd(int id);
        public void PurchaseClassifiedAd(string customerUsername, int id);
    }
}
