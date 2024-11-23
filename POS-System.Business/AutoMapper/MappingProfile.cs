using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.TimeSlotDtos;
using POS_System.Domain.Entities;

namespace POS_System.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tax, TaxDto>();
            CreateMap<TaxDto, Tax>();

            // TimeSlot
            CreateMap<TimeSlot, GetTimeSlotDto>();
            CreateMap<GetTimeSlotDto, TimeSlot>();
            CreateMap<TimeSlot, CreateTimeSlotDto>();
            CreateMap<CreateTimeSlotDto, TimeSlot>();
        }
    }
}
