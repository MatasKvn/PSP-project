using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Enums;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Dtos.Request;
using POS_System.Common.Exceptions;
using POS_System.Common.Constants;
using POS_System.Business.Services.Interfaces;
using Stripe;

namespace POS_System.Business.Services.Services
{
    public class CartService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICartService
    {
        public async Task<PagedResponse<CartResponse>> GetAllAsync(CancellationToken cancellationToken, int pageNum, int pageSize)
        {
            var (carts, cartCount) = await _unitOfWork.CartRepository.GetAllWithIncludesAndPaginationAsync(pageSize, pageNum, cancellationToken);
            var mappedCarts = _mapper.Map<IEnumerable<CartResponse>>(carts);
            return new PagedResponse<CartResponse>(cartCount, pageSize, pageNum, mappedCarts);
        }

        public async Task<CartResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken);
            // TODO: add actual exception
            if (cart is null)
            {
                throw new Exception("Cart not found.");
            }
            var mappedCart = _mapper.Map<CartResponse>(cart);
            return mappedCart;
        }

        public async Task<CartResponse> CreateCartAsync(CartRequest cartDto, CancellationToken cancellationToken)
        {
            var cart = new Cart {
                EmployeeVersionId = cartDto.EmployeeVersionId,
                Status = CartStatusEnum.IN_PROGRESS,
                IsDeleted = false,
                CartDiscountId = null,
                DateCreated = DateTime.UtcNow
            };

            await _unitOfWork.CartRepository.CreateAsync(cart, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            var responseCartDto = _mapper.Map<CartResponse>(cart);
            return responseCartDto;
        }

        public async Task DeleteCartAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken);
            // TODO: add actual exception
            if (cart is null)
            {
                throw new Exception("Cart not found.");
            }
            if (cart.Status != CartStatusEnum.IN_PROGRESS)
            {
                throw new Exception("Cannot delete a not in progress cart.");
            }
            _unitOfWork.CartRepository.Delete(cart);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCartStatusAsync(int id, CartStatusEnum status, CancellationToken cancellationToken = default)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            
            cart.Status = status;
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CartDiscountResponse> ApplyDiscountForCartAsync(int id, ApplyDiscountRequest discountRequest, CancellationToken cancellationToken)
        {
            var cartTask = _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken);
            
            var couponService = new CouponService();
            var coupon = await couponService.GetAsync(discountRequest.DiscountCode, cancellationToken:cancellationToken)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            if (!coupon.Valid)
                throw new BadRequestException(ApplicationMessages.EXPIRED_DISCOUNT);

            var cart = await cartTask ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            if (cart.Status != CartStatusEnum.IN_PROGRESS)
                throw new BadRequestException(ApplicationMessages.CART_NOT_IN_PROGRESS);

            cart.CartDiscountId = discountRequest.DiscountCode;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CartDiscountResponse
            {
                Id = coupon.Id,
                Value = (int)(coupon.PercentOff is null ? coupon.AmountOff : coupon.PercentOff)!,
                IsPercentage = coupon.AmountOff is null,
            };
        }

        public async Task<CartDiscountResponse?> GetCartDiscountAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken);
            
            if (cart is not null && cart.CartDiscountId is not null)
            {
                var cartDiscount = await _unitOfWork.CartDiscountRepository.GetByIdStringAsync(cart.CartDiscountId, cancellationToken);

                if (cartDiscount is not null)
                    return new CartDiscountResponse
                    {
                        Id = cartDiscount.Id,
                        Value = cartDiscount.Value,
                        IsPercentage = cartDiscount.IsPercentage
                    };
            }

            return null;
        }
    }
}
