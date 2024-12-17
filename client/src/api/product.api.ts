import { Product } from './../types/models'
import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'
export default class ProductApi {
    static async getAllProducts(pageNumber: number, onlyActive?: boolean): Promise<FetchResponse<PagedResponse<Product>>> {
        return await fetch({
             url: `${apiBaseUrl}/product?pageNum=${pageNumber}&onlyActive=${onlyActive}`,
             method: HTTPMethod.GET,
             headers: getAuthorizedHeaders()
        })
    }

    static async getProductById(productId: number): Promise<FetchResponse<Product>> {
        return await fetch({
             url: `${apiBaseUrl}/product/${productId}`,
             method: HTTPMethod.GET,
             headers: getAuthorizedHeaders()
        })
    }

    static async createProduct(product: Product): Promise<FetchResponse<Product>> {
        return await fetch({
             url: `${apiBaseUrl}/product`,
             method: HTTPMethod.POST,
             headers: getAuthorizedHeaders(),
             body: JSON.stringify(product)
        })
    }

    static async deleteProductById(productId: number): Promise<FetchResponse<any>> {
        return await fetch({
             url: `${apiBaseUrl}/product/${productId}`,
             method: HTTPMethod.DELETE,
             headers: getAuthorizedHeaders()
        })
    }

    static async updateProductById(productId: number, product: Product): Promise<FetchResponse<Product>> {
        return await fetch({
             url: `${apiBaseUrl}/product/${productId}`,
             method: HTTPMethod.PUT,
             headers: getAuthorizedHeaders(),
             body: JSON.stringify(product)
        })
    }
}