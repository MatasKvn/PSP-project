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
    public async Task<PagedResponse<CartItemResponseDto>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize)
    {
        var (cartItems, totalCount) = await unitOfWork.CartItemRepository.GetAllByExpressionWithPaginationAsync(CartItem => CartItem.CartId == cartId,
            pageSize,
            pageNum,
            cancellationToken
        );

        var mappedCartItems = mapper.Map<IEnumerable<CartItemResponseDto>>(cartItems);
        return new PagedResponse<CartItemResponseDto>(totalCount, pageSize, pageNum, mappedCartItems);
    }

    public async Task<CartItemResponseDto?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var cartItem = await unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        return mapper.Map<CartItemResponseDto>(cartItem);
    }

    public async Task<CartItemResponseDto> CreateCartItemAsync(CartItemRequestDto cartItemRequestDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(cartItemRequestDto, nameof(cartItemRequestDto));

        var cartExists = await unitOfWork.CartRepository.GetByIdAsync(cartItemRequestDto.CartId, cancellationToken);
        if (cartExists is null)
        {
            throw new BadRequestException($"Cart with Id {cartItemRequestDto.CartId} does not exist, cannot add item to it.");
        }

        var newCartItem = mapper.Map<CartItem>(cartItemRequestDto);

        await unitOfWork.CartItemRepository.CreateAsync(newCartItem, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CartItemResponseDto>(newCartItem);
    }

    public async Task<CartItemResponseDto> UpdateCartItemAsync(int cartId, int id, CartItemRequestDto cartItemRequestDto, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);
        ArgumentNullException.ThrowIfNull(cartItemRequestDto, nameof(cartItemRequestDto));

        var cartItemToUpdate = await unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        mapper.Map(cartItemRequestDto, cartItemToUpdate);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CartItemResponseDto>(cartItemToUpdate);
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