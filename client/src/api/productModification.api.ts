import { Product, ProductModification } from './../types/models'
import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { encodeDateToUrlString, fetch, getAuthorizedHeaders } from '@/utils/fetch'

export default class ProductModificationApi {
    static async getAll(pageNumber: number, onlyActive?: boolean): Promise<FetchResponse<PagedResponse<ProductModification>>> {
        return await fetch({
            url: `${apiBaseUrl}/product-modification?pageNumber=${pageNumber}&onlyActive=${onlyActive}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getByProductId(productId: number, pageNumber: number): Promise<FetchResponse<PagedResponse<ProductModification>>> {
        return await fetch({
            url: `${apiBaseUrl}/product-modification/product/${productId}?pageNumber=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getByCartItemId(cartItemId: number): Promise<FetchResponse<ProductModification[]>> {
        return await fetch({
            url: `${apiBaseUrl}/product-modification/cart-item/${cartItemId}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async createProductModification(dto: ProductModification): Promise<FetchResponse<ProductModification>> {
        console.log(dto);

        return await fetch({
            url: `${apiBaseUrl}/product-modification`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(dto)
        })
    }

    static async updateProductModification(id: number, dto: ProductModification): Promise<FetchResponse<ProductModification>> {
        return await fetch({
            url: `${apiBaseUrl}/product-modification/${id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(dto)
        })
    }

    static async deleteProductModification(id: number): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/product-modification/${id}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders()
        })
    }
}