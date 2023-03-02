using DAL.DTOs;
using DAL.Models;
using DAL.Models.Context;
using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.BuildingRepository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext _context;

        public BuildingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBuildingAsync(Building building)
        {
            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();
        }

        public async Task<Building> GetBuildingAsync(string username, BuildingType name)
        {
            return await _context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .FirstAsync(building => building.Player.User.Username == username && building.Type == name);
        }

        public async Task<List<Building>> GetAllBuildingByUsernameAsync(string username)
        {
            return await _context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .Where(building => building.Player.User.Username == username)
                .ToListAsync();
        }

        public async Task<List<CoordinateDto>> GetReservedCoordinates(string username)
        {
            return await _context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .Where(building => building.Player.User.Username == username)
                .Select(building => new CoordinateDto()
                {
                    XCoordinate = building.XCoordinate,
                    YCoordinate = building.YCoordinate
                })
                .ToListAsync();
        }

        public async Task UpdateBuildingAsync(Building building)
        {
            _context.Entry(building).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckBuildingIsExist(string username, BuildingType buildingType)
        {
            Building? building = await _context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .FirstOrDefaultAsync(building => building.Player.User.Username == username && building.Type == buildingType);

            return building != null;
        }
    }
}
