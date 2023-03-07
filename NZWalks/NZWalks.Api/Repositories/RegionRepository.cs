using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext db;

        public RegionRepository(NZWalksDbContext db) { 
            this.db= db;
        }
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await db.Regions.ToListAsync();
        }
    }
}
