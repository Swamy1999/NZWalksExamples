using Microsoft.AspNetCore.Mvc;
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

        public async  Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await db.AddAsync(region);
            await db.SaveChangesAsync();
            return region;
            
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var regions = await db.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (regions == null)
            {
                return regions;
            }
            else
            {
                db.Regions.Remove(regions);
                await db.SaveChangesAsync();
                return regions;
            }
           
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await db.Regions.ToListAsync();
        }


        public async Task<Region> GetAsync(Guid id)
        {
           var regions  = await db.Regions.FindAsync(id);
            return regions;
        }

        public async  Task<Region> UpdateAsync(Region region, Guid id)

        {
            var regions = await db.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regions == null)
            {
                return regions;
            }
            else
            {
                regions.Code = region.Code;
                regions.Name = region.Name;
                regions.Area = region.Area;
                regions.Lat = region.Lat;
                regions.Long = region.Long;
                regions.Population = region.Population;

                await db.SaveChangesAsync();
                return regions;
            }

        }
    }
}
