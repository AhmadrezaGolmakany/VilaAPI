using AutoMapper;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Models;
using Vila_WebAPI.Utility;

namespace Vila_WebAPI.Mapper
{
    public class MapperDTO : Profile
    {
        public MapperDTO()
        {
            CreateMap<Vila, VilaDTOs>()
                .ForMember(x => x.MadeDate, d => d.MapFrom(res => res.MadeDate.ToPersainDate()))
                .ReverseMap()
                .ForMember(x=>x.MadeDate , d=>d.MapFrom(res=>res.MadeDate.ToEnglishDateTime()));
                

        }
    }
}
