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
                Status = CartStatusEnum.PENDING,
                IsDeleted = false,
                CartDiscountId = null,
                DateCreated = DateTime.Now
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
            if (cart.Status != CartStatusEnum.PENDING)
            {
                throw new Exception("Cannot delete a non-pending cart.");
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
    }
}
