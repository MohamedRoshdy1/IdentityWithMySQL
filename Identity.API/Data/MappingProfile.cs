using AutoMapper;
using IdenittyWithMySql;
using Identity.API.DTOs;

namespace Identity.API.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, userDTO>().ReverseMap();

        }
    }
}