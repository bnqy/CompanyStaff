using AutoMapper;
using Entities.Models;
using Shared.DTOs;

namespace CompanyStaff
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Company, CompanyDTO>()
                .ForCtorParam("FullAddress",
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        }
    }
}
