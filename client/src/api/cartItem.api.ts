import CartItemMapper from '@/mappers/cartItem.mapper'
import { TimeSlot, ServiceCartItem } from './../types/models'
import { FetchResponse, PagedResponse } from '@/types/fetch'
import { CartItem } from '@/types/models'

export type CartItemResponse = ProductCartItemResposne | ServiceCartItemResponse
export type ProductCartItemResposne = {
    isProduct: true
    id: number
    cartId: number
    quantity: number
    productVersionId: number
}
export type ServiceCartItemResponse = {
    isProduct: false
    id: number
    cartId: number
    quantity: number
    serviceVersionId: number
    serviceReservationId: number
    timeSlotId: number
}

let cartItems: CartItemResponse[] = [
    {
        id: 1,
        cartId: 1,
        quantity: 13,
        isProduct: true,
        productVersionId: 1
    },
    {
        id: 2,
        cartId: 1,
        quantity: 1,
        isProduct: false,
        serviceVersionId: 1,
        serviceReservationId: 1,
        timeSlotId: 1
    }
]

export default class CartItemApi {
    static async getCartItems(cartId: number, pageNum: number): Promise<FetchResponse<PagedResponse<CartItemResponse>>> {
        const results = cartItems.filter((cartItem) => cartItem.cartId == cartId)
        const result = {
            pageNum,
            pageSize: 35,
            totalCount: results.length,
            results: results
        }
        return Promise.resolve({ result })
    }

    static async createCartItem(cartId: number, dto: CreateCartItemDto): Promise<FetchResponse<CartItem>> {
        const maxId = Math.max(...cartItems.map((cartItem) => cartItem.id))

        // @ts-expect-error
        let cartItem: CartItemFetch= {
            id: maxId + 1,
            cartId,
            quantity: dto.quantity
        }

        if (dto.type === 'product' && dto.productVersionId) {
            cartItem = {
                ...cartItem,
                isProduct: true,
                productVersionId: dto.productVersionId
            }
        } else if (dto.type === 'service' && dto.serviceVersionId) {
            cartItem = {
                ...cartItem,
                isProduct: false,
                serviceVersionId: dto.serviceVersionId
            }
        }

        cartItems.push(cartItem)
        return Promise.resolve({ result: cartItem })
    }

    static async deleteCartItem(id: number): Promise<FetchResponse<any>> {
        const cartItemToDelete = cartItems.find((cartItem) => cartItem.id === id)
        if (!cartItemToDelete) return Promise.resolve({ error: 'Cart item not found' })
        const filteredCartItems = cartItems.filter((cartItem) => cartItem.id !== id)
        cartItems = filteredCartItems
        return Promise.resolve({ result: cartItemToDelete })
    }
}

type CreateCartItemDto = {
    type: 'product'
    quantity: number
    productVersionId: number
    variationIds: number[]
} | {
    type: 'service'
    quantity: number
    serviceVersionId: number
}