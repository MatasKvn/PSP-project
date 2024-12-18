import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { CartItem } from '@/types/models'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'
import { apiBaseUrl } from '@/constants/api'

export type CartItemResponse = {
    isProduct: true
    id: number
    cartId: number
    quantity: number
} & ( { productVersionId: number } | { serviceVersionId: number } )

type CreateCartItemRequest = CreateProductCartItemRequest | CreateServiceCartItemRequest
type CreateProductCartItemRequest = {
    cartId: number
    type: 'product'
    quantity: number
    productVersionId: number
    variationIds: number[]
}

type CreateServiceCartItemRequest = {
    cartId: number
    type: 'service'
    quantity: number
    serviceVersionId: number
}

type CreateCartItemRequestMapped = {
    cartId: number
    isProduct: boolean
    quantity: number
    productVersionId: number
    serviceVersionId: number
}

const mapCreateCartItemRequest = (request: CreateCartItemRequest): CreateCartItemRequestMapped => {
    return {
        cartId: request.cartId,
        isProduct: request.type === 'product',
        quantity: request.quantity,
        productVersionId: (request as CreateProductCartItemRequest).productVersionId,
        serviceVersionId: (request as CreateServiceCartItemRequest).serviceVersionId
    }
}

export default class CartItemApi {
    static async getCartItems(cartId: number, pageNum: number): Promise<FetchResponse<PagedResponse<CartItemResponse>>> {
        return fetch({
            url: `${apiBaseUrl}/carts/${cartId}/items?pageNum=${pageNum}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async createCartItem(dto: CreateCartItemRequest): Promise<FetchResponse<CartItem>> {
        const cartItemResponse = await fetch({
            url: `${apiBaseUrl}/carts/${dto.cartId}/items`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(mapCreateCartItemRequest(dto))
        })
        if (!cartItemResponse.result) {
            return cartItemResponse
        }
        const cartItem = cartItemResponse.result
        if (dto.type === 'product') {
            const res = await fetch({
                url: `${apiBaseUrl}/carts/${dto.cartId}/items/${cartItem.id}/link`,
                method: HTTPMethod.PUT,
                headers: getAuthorizedHeaders(),
                body: JSON.stringify(dto.variationIds)
            })
        }

        return cartItemResponse
    }

    static async deleteCartItem(cartId: number, itemId: number): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/carts/${cartId}/items/${itemId}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders()
        })
    }
}

