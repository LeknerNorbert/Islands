using Islands.Models;
using Islands.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.ClassifiedAdRepository
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassifiedAdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ClassifiedAd GetClassifiedAd(int id)
        {
            return _context.Classifieds
                .Include(c => c.PlayerInformation)
                .First(c => c.Id == id);
        }

        public List<ClassifiedAd> GetClassifiedAds()
        {
            return _context.Classifieds.ToList();
        }

        public List<ClassifiedAd> GetClassifiedAdsByUsername(string username)
        {
            return _context.Classifieds
                .Include(c => c.PlayerInformation)
                    .ThenInclude(p => p.User)
                .Where(c => c.PlayerInformation.User.Username == username && c.PublishDate.AddDays(7) <= DateTime.Now)
                .OrderBy(c => c.PublishDate)
                .ToList();
        }

        public void CreateClassifiedAd(ClassifiedAd classifiedAd)
        {
            _context.Classifieds.Add(classifiedAd);
            _context.SaveChanges();
        }

        public void DeleteClassifiedAd(ClassifiedAd classifiedAd)
        {
            _context.Classifieds.Remove(classifiedAd);
            _context.SaveChanges();
        }
    }
}
