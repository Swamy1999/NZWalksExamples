using AutoMapper;

namespace NZWalks.Api.Profiles
{
    public class RegionProfile:Profile
    {
        //profile is from automapper dependency
        public RegionProfile() {
            //it dirctly converts/maps Domain to Dto classes
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
            //.ForMember(dest=> if they dont have same names for properties
              //      dest.Id,options=>options.MapFrom(src=>src.RegionId));
        }
    }
}
