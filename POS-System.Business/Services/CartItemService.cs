using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services;

public class CartItemService(IUnitOfWork unitOfWork, IMapper mapper) : ICartItemService
{
    public async Task<PagedResponse<CartItemResponse>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize)
    {
        var (cartItems, totalCount) = await unitOfWork.CartItemRepository.GetAllByExpressionWithIncludesAndPaginationAsync(CartItem => CartItem.CartId == cartId,
            pageSize,
            pageNum,
            cancellationToken,
            c => c.ServiceReservation);

        var mappedCartItems = mapper.Map<IEnumerable<CartItemResponse>>(cartItems);

        return new PagedResponse<CartItemResponse>(totalCount, pageSize, pageNum, mappedCartItems);
    }

    public async Task<CartItemResponse?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var cartItem = await unitOfWork.CartItemRepository.GetByExpressionWithIncludesAsync(
            cartItem => cartItem.Id == id && cartItem.CartId == cartId,
            cancellationToken,
            c => c.ServiceReservation) 
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        var response = mapper.Map<CartItemResponse>(cartItem);

        return response;
    }

    public async Task<CartItemResponse> CreateCartItemAsync(CartItemRequest CartItemRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(CartItemRequest, nameof(CartItemRequest));

        var cartExists = await unitOfWork.CartRepository.GetByIdAsync(CartItemRequest.CartId, cancellationToken);
        if (cartExists is null)
        {
            throw new BadRequestException($"Cart with Id {CartItemRequest.CartId} does not exist, cannot add item to it.");
        }

        var newCartItem = mapper.Map<CartItem>(CartItemRequest);

        await unitOfWork.CartItemRepository.CreateAsync(newCartItem, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CartItemResponse>(newCartItem);
    }

    public async Task<CartItemResponse> UpdateCartItemAsync(int cartId, int id, CartItemRequest CartItemRequest, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);
        ArgumentNullException.ThrowIfNull(CartItemRequest, nameof(CartItemRequest));

        var cartItemToUpdate = await unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        mapper.Map(CartItemRequest, cartItemToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CartItemResponse>(cartItemToUpdate);
    }

    public async Task DeleteCartItemAsync(int cartId, int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var cartItemToDelete = await unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
           ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        unitOfWork.CartItemRepository.Delete(cartItemToDelete);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}