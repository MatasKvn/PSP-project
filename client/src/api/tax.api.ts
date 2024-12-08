import { FetchResponse, PagedResponse } from '@/types/fetch'
import { Tax } from '@/types/models'

const tax: Tax = {
    name: 'PVM',
    dateModified: new Date(),
    id: 1,
    isPercentage: true,
    rate: 13
}

export default class TaxApi {
    static async getAllTaxes(pageNumber: number): Promise<FetchResponse<PagedResponse<Tax>>> {
        return Promise.resolve({
            result: {
                pageNum: pageNumber,
                pageSize: 35,
                totalCount: 1,
                results: [tax]
            }
        })
    }

    static async addProductsToTax(taxId: number, productIds: number[]): Promise<FetchResponse<any>> {
        return Promise.resolve({ result: tax })
    }

    static async addServicesToTax(taxId: number, serviceIds: number[]): Promise<FetchResponse<any>> {
        return Promise.resolve({ result: tax })
    }

    static async addProductModificationsToTax(taxId: number, productModificationIds: number[]): Promise<FetchResponse<any>> {
        return Promise.resolve({ result: tax })
    }

    static async createTax(taxRequest: CreateTaxRequest): Promise<FetchResponse<Tax>> {
        return Promise.resolve({
            result: {
                ...taxRequest,
                id: new Date().getTime(),
                dateModified: new Date()
            }
        })
    }

    static async updateTax(taxRequest: UpdateTaxRequest): Promise<FetchResponse<Tax>> {
        return Promise.resolve({
            result: {
                ...taxRequest,
                dateModified: new Date()
            }
        })
    }

    static async deleteTax(id: number): Promise<FetchResponse<any>> {
        return Promise.resolve({ result: tax })
    }

    static async getTaxesByProductId(productId: number): Promise<FetchResponse<Tax[]>> {
        return Promise.resolve({
            result: [tax]
        })
    }
    static async getTaxesByServiceId(serviceId: number): Promise<FetchResponse<Tax[]>> {
        return Promise.resolve({
            result: [tax]
        })
    }
}

type CreateTaxRequest = Omit<Tax, 'id' | 'dateModified'>
type UpdateTaxRequest = Omit<Tax, 'dateModified'>