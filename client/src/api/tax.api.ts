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

    static async createTax(taxRequest: CreateTaxRequest): Promise<FetchResponse<Tax>> {
        return Promise.resolve({
            result: {
                ...taxRequest,
                id: 1,
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
        if (id !== 1) return Promise.resolve({ error: 'Tax not found' })
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