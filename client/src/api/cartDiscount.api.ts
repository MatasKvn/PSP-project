import { FetchResponse, HTTPMethod } from '@/types/fetch'
import { CartDiscount, CartDiscountBody } from './../types/models'
import { apiBaseUrl, defaultHeaders } from '@/constants/api';
import { fetch } from '@/utils/fetch';

export default class CartDiscountApi {
    static async applyCartDiscount(cartId: number, body: CartDiscountBody): Promise<FetchResponse<CartDiscount>> {
        return await fetch({
            url: `${apiBaseUrl}/carts/${cartId}/discount`,
            method: HTTPMethod.PATCH,
            headers: defaultHeaders,
            body: JSON.stringify(body)
        });
    }

    static async getCartDiscount(cartId: number): Promise<FetchResponse<CartDiscount>> {
        return await fetch({
            url: `${apiBaseUrl}/carts/${cartId}/discount`,
            method: HTTPMethod.GET,
            headers: defaultHeaders,
        });
    }
}