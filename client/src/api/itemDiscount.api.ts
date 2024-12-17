import { apiBaseUrl } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { ItemDiscount } from '@/types/models'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'

let discounts: ItemDiscount[] = [
    {
        id: 1,
        value: 10,
        isPercentage: true,
        description: '10% off',
        startDate: new Date('2022-01-01 00:00:00'),
        endDate: new Date('2025-12-31 23:59:59')
    },
    {
        id: 2,
        value: 20,
        isPercentage: true,
        description: '20% off',
        startDate: new Date('2022-01-01 00:00:00'),
        endDate: new Date('2022-12-31 23:59:59')
    }
]

export default class ItemDiscountApi {
    static async getCurrentDiscountsByProductId(productId: number): Promise<FetchResponse<ItemDiscount[]>> {
        const result = discounts.filter(discount => discount.startDate <= new Date() && discount.endDate >= new Date() && discount.id === productId)
        return Promise.resolve({ result })
    }

    static async getCurrentDiscountByServiceId(serviceId: number): Promise<FetchResponse<ItemDiscount[]>> {
        const result = discounts.filter(discount => discount.startDate <= new Date() && discount.endDate >= new Date() && discount.id === serviceId)
        return Promise.resolve({ result })
    }

    static async getAllDiscounts(pageNumber: number): Promise<FetchResponse<PagedResponse<ItemDiscount>>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount?pageNum=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async updateDiscount(dto: UpdateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount/${dto.id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(dto)
        })
    }

    static async createDiscount(dto: CreateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        return fetch({
            url: `${apiBaseUrl}/item-discount`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(dto)
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