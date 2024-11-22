using AutoMapper;
using POS_System.Business.Dtos;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace POS_System.Business.Services.Interfaces
{
    public class CartService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICartService
    {
        public async Task<IEnumerable<CartResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var carts = await _unitOfWork.CartRepository.GetAllAsync(cancellationToken);
            var mappedCarts = _mapper.Map<IEnumerable<CartResponse>>(carts);
            return mappedCarts;
        }

        public async Task<CartResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id, cancellationToken);
            // TODO: add actual exception
            if (cart == null)
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
                IsCompleted = false,
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
            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }
            if (cart.IsCompleted)
            {
                throw new Exception("Cannot delete completed cart.");
            }
            _unitOfWork.CartRepository.Delete(cart);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
