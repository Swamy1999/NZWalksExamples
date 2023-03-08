using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();

        Task<Region> GetAsync(Guid id);

        //add regions
        Task<Region>  AddAsync(Region region);

        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Region region,Guid id);
    }
}
