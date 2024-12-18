import { apiBaseUrl } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { ItemDiscount } from '@/types/models'
import { encodeDateToUrlString, fetch, getAuthorizedHeaders, sanitizeData } from '@/utils/fetch'

export default class ItemDiscountApi {
    static async getCurrentDiscountsByProductId(productId: number): Promise<FetchResponse<ItemDiscount[]>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/item/${productId}?isProduct=true&timeStamp=${encodeDateToUrlString(new Date())}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getCurrentDiscountByServiceId(serviceId: number): Promise<FetchResponse<ItemDiscount[]>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/item/${serviceId}?isProduct=true&timeStamp=${encodeDateToUrlString(new Date())}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getAllDiscounts(pageNumber: number): Promise<FetchResponse<PagedResponse<ItemDiscount>>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount?pageNum=${pageNumber}&onlyActive=true`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async updateDiscount(dto: UpdateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        const sanitizedDto = sanitizeData(dto)
        return fetch({
            url: `${apiBaseUrl}/item-discount/${dto.id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(sanitizedDto)
        })
    }

    static async createDiscount(dto: CreateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        const sanitizedDto = sanitizeData(dto)
        return fetch({
            url: `${apiBaseUrl}/item-discount`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(sanitizedDto)
        })
    }

    static async deleteDiscount(id: number): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${id}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders()
        })
    }

    static async addProductsToDiscount(discountId: number, productIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${discountId}/link?itemsAreProducts=true`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(productIds)
        })
    }

    static async addServicesToDiscount(discountId: number, serviceIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${discountId}/link?itemsAreProducts=false`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(serviceIds)
        })
    }

    static async removeProductsFromDiscount(discountId: number, productIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${discountId}/unlink?itemsAreProducts=true`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(productIds)
        })
    }

    static async removeServicesFromDiscount(discountId: number, serviceIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${discountId}/unlink?itemsAreProducts=false`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(serviceIds)
        })
    }
}

type CreateDiscountRequest = Omit<ItemDiscount, 'id'>
type UpdateDiscountRequest = ItemDiscount