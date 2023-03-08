using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
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

        //get single
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionsAsync")]
        public async Task<IActionResult> GetRegionsAsync(Guid id)
        {
           var region =  await regionRepository.GetAsync(id);

            if (region != null)
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population,
                };
                return Ok(regionDTO);
            }
            else
            {
                return NotFound();
            }
         }


        //post
        [HttpPost]
        [Route("addRegions")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRequest)
        {
            var region = new Models.Domain.Region()
            {
                Code = addRequest.Code,
                Area = addRequest.Area,
                Name = addRequest.Name,
                Lat = addRequest.Lat,
                Long = addRequest.Long,
                Population = addRequest.Population,

            };
           region =  await regionRepository.AddAsync(region);

            //while we sending we send data as DTO object
            var regionDTO = new Models.DTO.Region
            {
                Id=region.Id,
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };
            //return Ok("added successfuly");
            return Ok(CreatedAtAction(nameof(GetRegionsAsync), new {id=region.Id}));
        }


        //delete
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return Ok("No record Found");
            }
            else
            {
                return Ok("Record deleted successfully");
            }
        }

        //update
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateRegion([FromRoute]Guid id,[FromBody] UpdateRegionRequest userRegion)
        {
            var region = new Models.Domain.Region {
                Id = id,
                Code = userRegion.Code,
                Name = userRegion.Name,
                Lat = userRegion.Lat,
                Long = userRegion.Long,
                Area = userRegion.Area,
                Population = userRegion.Population,
        };
            

            var result = await regionRepository.UpdateAsync(region,id);
            if (result == null)
            {
                
                return Ok("No record found");
            }
            else
            {
                return Ok("record updated successfully");
            }
        }
    }

}
