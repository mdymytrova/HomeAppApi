using AutoMapper;
using HomeAppApi.Dto;
using HomeAppApi.Models;

namespace HomeAppApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                .ForMember(dest => dest.HouseCount, opt => opt.MapFrom(src => src.Houses.Count()));
            CreateMap<CityDto, City>();
            CreateMap<House, HouseDto>()
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<HouseDto, House>();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
        }
    }
}
