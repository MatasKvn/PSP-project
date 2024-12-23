import { Product } from './../types/models'
import { apiBaseUrl } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'
export default class ProductApi {
    static async getAllProducts(pageNumber: number, onlyActive?: boolean): Promise<FetchResponse<PagedResponse<Product>>> {
        return await fetch({
            url: `${apiBaseUrl}/product?pageNumber=${pageNumber}&onlyActive=${onlyActive}`,
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

    static async createProduct(product: CreateProductRequest): Promise<FetchResponse<Product>> {
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

    static async updateProductById(dto: UpdateProductRequest): Promise<FetchResponse<Product>> {
        return await fetch({
             url: `${apiBaseUrl}/product/${dto.id}`,
             method: HTTPMethod.PUT,
             headers: getAuthorizedHeaders(),
             body: JSON.stringify(dto)
        })
    }

    static async getProductsByTaxId(taxId: number): Promise<FetchResponse<Product[]>> {
        return fetch({
            url: `${apiBaseUrl}/product/tax/${taxId}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getProductsByDiscountId(discountId: number): Promise<FetchResponse<Product[]>> {
        return fetch({
            url: `${apiBaseUrl}/product/item-discount/${discountId}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }
}

type CreateProductRequest = Omit<Product, 'id' | 'dateModified'>
type UpdateProductRequest = Omit<Product, 'dateModified'>