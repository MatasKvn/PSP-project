import { apiBaseUrl } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { Product, Service, Tax } from '@/types/models'
import { fetch, getAuthorizedHeaders, encodeDateToUrlString } from '@/utils/fetch'

export default class TaxApi {
    static async getAllTaxes(pageNumber: number): Promise<FetchResponse<PagedResponse<Tax>>> {
        return fetch({
                url: `${apiBaseUrl}/tax?pageNum=${pageNumber}`,
                method: HTTPMethod.GET,
                headers: getAuthorizedHeaders()
            }
        )
    }

    static async createTax(taxRequest: CreateTaxRequest): Promise<FetchResponse<Tax>> {
        return fetch({
            url: `${apiBaseUrl}/tax`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(taxRequest)
        })
    }

    static async updateTax(taxRequest: UpdateTaxRequest): Promise<FetchResponse<Tax>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${taxRequest.id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(taxRequest)
        })
    }

    static async deleteTax(id: number): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${id}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders()
        })
    }

    static async addProductsToTax(taxId: number, productIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${taxId}/link?itemsAreProducts=true`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(productIds)
        })
    }

    static async addServicesToTax(taxId: number, serviceIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${taxId}/link?itemsAreProducts=false`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(serviceIds)
        })
    }

    static async removeProductsFromTax(taxId: number, productIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${taxId}/unlink?itemsAreProducts=true`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(productIds)
        })
    }

    static async removeServicesFromTax(taxId: number, serviceIds: number[]): Promise<FetchResponse<any>> {
        return fetch({
            url: `${apiBaseUrl}/tax/${taxId}/unlink?itemsAreProducts=false`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(serviceIds)
        })
    }

    static async getTaxesByProductId(productId: number): Promise<FetchResponse<Tax[]>> {
        return fetch({
            url: `${apiBaseUrl}/tax/item/${productId}?isProduct=true`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getTaxesByServiceId(serviceId: number): Promise<FetchResponse<Tax[]>> {
        return fetch({
            url: `${apiBaseUrl}/tax/item/${serviceId}?isProduct=true`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }
}

type CreateTaxRequest = Omit<Tax, 'id' | 'dateModified'>
type UpdateTaxRequest = Omit<Tax, 'dateModified'>