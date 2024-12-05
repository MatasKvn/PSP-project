using AutoMapper;
using POS_System.Business.Dtos.Request;
using POS_System.Data.Identity;
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
            CreateMap<GiftCardRequest, GiftCard>();
            CreateMap<GiftCard, GiftCardResponse>();

            // ProductModification
            CreateMap<ProductModification, ProductModificationResponse>();
            CreateMap<ProductModificationResponse, ProductModification>();
            CreateMap<ProductModification, ProductModificationRequest>();
            CreateMap<ProductModificationRequest, ProductModification>();

            // Product
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();

            // Employees
            CreateMap<ApplicationUser, EmployeeResponse>();
            CreateMap<EmployeeRequest, ApplicationUser>();

            // TimeSlot
            CreateMap<TimeSlot, TimeSlotResponse>();
            CreateMap<TimeSlotResponse, TimeSlot>();
            CreateMap<TimeSlot, TimeSlotRequest>();
            CreateMap<TimeSlotRequest, TimeSlot>();

            // Tax
            CreateMap<Tax, TaxResponse>();
            CreateMap<TaxRequest, Tax>();
            
            //Gift card
            CreateMap<GiftCardRequest, GiftCard>();
            CreateMap<GiftCard, GiftCardResponse>();

            // ServiceReservation
            CreateMap<ServiceReservation, ServiceReservationResponse>();
            CreateMap<ServiceReservation, ServiceReservationRequest>();
            CreateMap<ServiceReservationResponse, ServiceReservation>();
            CreateMap<ServiceReservationRequest, ServiceReservation>();

            //Business details
            CreateMap<BusinessDetailsRequest, BusinessDetails>();
            CreateMap<BusinessDetails, BusinessDetailsResponse>();

            //Service
            CreateMap<ServiceRequest, Service>();
            CreateMap<Service, ServiceResponse>();

            //Cart item
            CreateMap<CartItemRequest, CartItem>();
            CreateMap<CartItem, CartItemResponse>();

            //Item discount
            CreateMap<ItemDiscountRequest, ItemDiscount>();
            CreateMap<ItemDiscount, ItemDiscountResponse>();
            
            //Cart discount
            CreateMap<CartDiscountRequest, CartDiscount>();
            CreateMap<CartDiscount, CartDiscountResponse>();
        }
    }
}
