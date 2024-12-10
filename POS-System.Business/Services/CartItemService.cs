using AutoMapper;
using POS_System.Business.Validators;
using POS_System.Business.Services.Interfaces;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using POS_System.Common.Exceptions;
using POS_System.Business.Dtos;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Data.Repositories;

namespace POS_System.Business.Services;

public class CartItemService(IUnitOfWork _unitOfWork, IManyToManyService<ProductModification, CartItem, ProductModificationOnCartItem> _productModificationOnCartItemService, IMapper _mapper) : ICartItemService
{
    public async Task<PagedResponse<CartItemResponse>> GetAllCartItemsAsync(int cartId, CancellationToken cancellationToken, int pageNum, int pageSize)
    {
        var (cartItems, totalCount) = await _unitOfWork.CartItemRepository.GetAllByExpressionWithPaginationAsync(CartItem => CartItem.CartId == cartId,
            pageSize,
            pageNum,
            cancellationToken
        );

        var mappedCartItems = _mapper.Map<IEnumerable<CartItemResponse>>(cartItems);
        return new PagedResponse<CartItemResponse>(totalCount, pageSize, pageNum, mappedCartItems);
    }

    public async Task<CartItemResponse?> GetCartItemByIdAndCartIdAsync(int cartId, int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var cartItem = await _unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        return _mapper.Map<CartItemResponse>(cartItem);
    }

    public async Task<CartItemResponse> CreateCartItemAsync(CartItemRequest CartItemRequest, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(CartItemRequest, nameof(CartItemRequest));

        var cartExists = await _unitOfWork.CartRepository.GetByIdAsync(CartItemRequest.CartId, cancellationToken);
        if (cartExists is null)
        {
            throw new BadRequestException($"Cart with Id {CartItemRequest.CartId} does not exist, cannot add item to it.");
        }

        var newCartItem = _mapper.Map<CartItem>(CartItemRequest);
        newCartItem.IsDeleted = false;

        await _unitOfWork.CartItemRepository.CreateAsync(newCartItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CartItemResponse>(newCartItem);
    }

    public async Task<CartItemResponse> UpdateCartItemAsync(int cartId, int id, CartItemRequest CartItemRequest, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);
        ArgumentNullException.ThrowIfNull(CartItemRequest, nameof(CartItemRequest));

        var cartItemToUpdate = await _unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
            ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        _mapper.Map(CartItemRequest, cartItemToUpdate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CartItemResponse>(cartItemToUpdate);
    }

    public async Task DeleteCartItemAsync(int cartId, int id, CancellationToken cancellationToken)
    {
        IdValidator.ValidateId(id);

        var cartItemToDelete = await _unitOfWork.CartItemRepository.GetByExpressionAsync(cartItem => cartItem.Id == id && cartItem.CartId == cartId, cancellationToken)
           ?? throw new NotFoundException($"Item with id {id} is not in cart with id {cartId}.");

        _unitOfWork.CartItemRepository.Delete(cartItemToDelete);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task LinkCartItemToProductModificationsAsync(int cartItemId, int[] productModificationIdList, CancellationToken cancellationToken)
    {
        await _productModificationOnCartItemService.LinkItemToItemsAsync(_unitOfWork.ProductModificationRepository, _unitOfWork.CartItemRepository, _unitOfWork.ProductModificationOnCartItemRepository, cartItemId, productModificationIdList, true, cancellationToken);
    }

    public async Task UnlinkCartItemFromProductModificationsAsync(int cartItemId, int[] productModificationIdList, CancellationToken cancellationToken)
    {
        await _productModificationOnCartItemService.UnlinkItemFromItemsAsync(_unitOfWork.ProductModificationRepository, _unitOfWork.CartItemRepository, _unitOfWork.ProductModificationOnCartItemRepository, cartItemId, productModificationIdList, true, cancellationToken);
    }
}