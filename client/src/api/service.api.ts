import { FetchResponse, PagedResponse } from '@/types/fetch'
import { Service, Service } from './../types/models'


const services: Service[] = [
    {
        id: 1,
        name: 'Haircut',
        description: 'Haircut description',
        duration: 30,
        price: 100,
        imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png'
    },
    {
        id: 2,
        name: 'Pendicure',
        description: 'Pendicure description',
        duration: 60,
        price: 150,
        imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png'
    }
]

export default class ServiceApi {
    static async getAllServices(pageNumber: number): Promise<FetchResponse<PagedResponse<Service>>> {
        return Promise.resolve({
            result: {
                totalCount: services.length,
                pageSize: 35,
                pageNum: pageNumber,
                results: services
            }
        })
    }

    static async getById(id: number): Promise<FetchResponse<Service>> {
        const service = services.find((service) => service.id === id)
        if (!service) return Promise.resolve({ error: 'Service not found' })
        return Promise.resolve({
            result: service
        })
    }
}