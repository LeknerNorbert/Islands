using Islands.DTOs;

namespace Islands.Services.ClassifiedAdService
{
    public interface IClassifiedAdService
    {
        public List<ClassifiedAdDTO> GetClassifiedAds();
        public List<ClassifiedAdDTO> GetClassifiedAdsByUser(string username);
        public void CreateClassifiedAd(string creatorUsername, CreateClassifiedAdDTO createClassifiedAd);
        public void DeleteClassifiedAd(int id);
        public void PurchaseClassifiedAd(string customerUsername, int id);
    }
}
