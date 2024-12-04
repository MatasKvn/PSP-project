using AutoMapper;
using POS_System.Business.Dtos.Tax;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Data.Identity;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Dtos.ProductModificationDtos;
using POS_System.Business.Dtos.ProductDtos;
using POS_System.Business.Dtos.TimeSlotDtos;
using POS_System.Domain.Entities;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.BirtDate)));
            // Cart
            CreateMap<Cart, CartResponse>();
            CreateMap<CartResponse, Cart>();
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

            //Service
            CreateMap<ServiceRequestDto, Service>();
            CreateMap<ServiceUpdateRequestDto, Service>();
            CreateMap<Service, ServiceResponseDto>();

            //Cart item
            CreateMap<CartItemRequestDto, CartItem>();
            CreateMap<CartItem, CartItemResponseDto>();
        }
    }
}
