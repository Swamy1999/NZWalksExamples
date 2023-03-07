using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [ApiController]
    [Route("Regions")] //[controller] it can also be written
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        public IMapper mapper { get; }
        public RegionsController(IRegionRepository regionRepository,IMapper mapper) { 
           this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("getRegions")]
        public async  Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAll();
            
            //instead of using domain models we are using dto's


            //AutoMapper
            //Auotmapper.dependenc injection
           var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(domainRegions => {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = domainRegions.Id,
                    Code= domainRegions.Code,
                    Name= domainRegions.Name,
                    Area= domainRegions.Area,
                    Lat= domainRegions.Lat,
                    Long= domainRegions.Long,
                    Population= domainRegions.Population,
                };

                regionsDTO.Add(regionDTO);

            });
          //above code can be done in one line if we use automapper and by doing some stuff in profiles
        // var regionsDTO =  mapper.Map<Models.DTO.Region>(regions);
            return Ok(regionsDTO);
        }
    }
}
