using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.ClassifiedAdRepository
{
    public interface IClassifiedAdRepository
    {
        public ClassifiedAd GetClassifiedAd(int id);
        public List<ClassifiedAd> GetClassifiedAds();
        public List<ClassifiedAd> GetClassifiedAdsByUsername(string username);
        public void CreateClassifiedAd(ClassifiedAd classifiedAd);
        public void DeleteClassifiedAd(ClassifiedAd classifiedAd);
    }
}
