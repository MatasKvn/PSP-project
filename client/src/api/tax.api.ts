import { FetchResponse } from '@/types/fetch'
import { Tax } from '@/types/models'

const tax: Tax = {
    name: 'PVM',
    dateModified: new Date(),
    id: 1,
    isPercentage: true,
    rate: 13
}

export default class TaxApi {
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