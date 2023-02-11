using DAL.Models;
using DAL.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.PlayerInformationRepository
{
    public class PlayerInformationRepository : IPlayerInformationRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerInformationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PlayerInformation GetPlayerInformation(string username)
        {
            return _context.PlayerInformations
                .Include(p => p.User)
                .First(p => p.User.Username == username);
        }

        public void UpdatePlayerInformation(PlayerInformation playerInformation)
        {
            _context.Update(playerInformation);
            _context.SaveChanges();
        }
    }
}
