import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { Cart } from '@/types/models'
import { fetch } from '@/utils/fetch'
import { HTTPMethod, FetchResponse, PagedResponse } from '@/types/fetch'

export default class CartApi {
    static async getAllCarts(pageNumber: number): Promise<FetchResponse<PagedResponse<Cart>>> {
        return await fetch({
            url: `${apiBaseUrl}/carts?pageNum=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        })
    }

    static async getCartById(id: number): Promise<FetchResponse<Cart>> {
        return await fetch({
            url: `${apiBaseUrl}/carts/${id}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        })
    }

    static async createCart(cart: CreateCartDto): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/carts`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(cart)
        })
    }

    static async deleteCart(id: number): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/carts/${id}`,
            method: HTTPMethod.DELETE,
            headers: defaultHeaders
        })
    }
}

type CreateCartDto = Pick<Cart, 'employeeVersionId'>