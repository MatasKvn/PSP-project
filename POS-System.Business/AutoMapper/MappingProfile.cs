using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tax, TaxDto>();
            CreateMap<TaxDto, Tax>();

            CreateMap<UserRegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BirtDate)));
        }
    }
}
