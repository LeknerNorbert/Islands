using Islands.Models;

namespace Islands.Repositories.ClassifiedAdRepository
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
