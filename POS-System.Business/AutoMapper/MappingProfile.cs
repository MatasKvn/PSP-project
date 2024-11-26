using AutoMapper;
using POS_System.Business.Dtos.Tax;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Dtos.ProductModificationDtos;
using POS_System.Business.Dtos.ProductDtos;
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

            CreateMap<GiftCardRequestDto, GiftCard>();
            CreateMap<GiftCard, GiftCardResponseDto>();

            // ProductModification
            CreateMap<ProductModification, GetProductModificationDto>();
            CreateMap<GetProductModificationDto, ProductModification>();
            CreateMap<ProductModification, CreateProductModificationDto>();
            CreateMap<CreateProductModificationDto, ProductModification>();

            // Product
            CreateMap<Product, GetProductDto>();
            CreateMap<GetProductDto, Product>();
            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();

            // TimeSlot
            CreateMap<TimeSlot, GetTimeSlotDto>();
            CreateMap<GetTimeSlotDto, TimeSlot>();
            CreateMap<TimeSlot, CreateTimeSlotDto>();
            CreateMap<CreateTimeSlotDto, TimeSlot>();

            // Tax
            CreateMap<Tax, TaxResponseDto>();
            CreateMap<TaxRequestDto, Tax>();
            
            //Gift card
            CreateMap<GiftCardRequestDto, GiftCard>();
            CreateMap<GiftCard, GiftCardResponseDto>();
        }
    }
}
