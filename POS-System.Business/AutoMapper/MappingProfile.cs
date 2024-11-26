using AutoMapper;
using POS_System.Business.Dtos.Tax;
using POS_System.Domain.Entities;

namespace POS_System.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tax, TaxResponseDto>();
            CreateMap<TaxRequestDto, Tax>();
        }
    }
}
