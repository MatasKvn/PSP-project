import { FetchResponse } from '@/types/fetch'
import { Service } from './../types/models'
export default class ServiceApi {
    static async getById(id: number): Promise<FetchResponse<Service>> {
        return Promise.resolve({
            result: {
                id,
                name: 'Service ' + id,
                description: 'Service ' + id,
                duration: 60,
                price: 10,
                imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png'
            }
        })
    }
}