using Islands.Models;
using Islands.Models.Context;
using Islands.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Islands.Repositories.BuildingRepository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext context;

        public BuildingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddBuildingAsync(Building building)
        {
            await context.Buildings.AddAsync(building);
            await context.SaveChangesAsync();
        }

        public async Task<List<Building>> GetAllBuildingAsync(string username)
        {
            return await context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .Where(building => building.Player.User.Username == username)
                .ToListAsync();
        }

        public async Task UpdateBuilding(Building building)
        {
            context.Entry(building).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckBuildingIsExist(string username, BuildingType buildingType)
        {
            Building? building = await context.Buildings
                .Include(building => building.Player)
                    .ThenInclude(player => player.User)
                .FirstOrDefaultAsync(building => building.Player.User.Username == username && building.Type == buildingType);

            if (building == null)
            {
                return false;
            }

            return true;
        } 
    }
}
