import { FetchResponse, PagedResponse } from '@/types/fetch'
import { Service } from './../types/models'

let services: Service[] = [
    {
        id: 1,
        name: 'Haircut',
        description: 'Haircut description',
        duration: 30,
        price: 100,
        imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png',
        employeeId: 1
    },
    {
        id: 2,
        name: 'Pendicure',
        description: 'Pendicure description',
        duration: 60,
        price: 150,
        imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png',
        employeeId: 1
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

    static async create(serviceDto: CreateServiceRequest): Promise<FetchResponse<Service>> {
        const maxId = Math.max(...services.map((service) => service.id))
        const result = {
            ...serviceDto,
            id: maxId + 1
        }
        services = [...services, result]
        return Promise.resolve({
            result
        })
    }

    static async udpate(serviceDto: EditServiceRequest): Promise<FetchResponse<Service>> {
        const service = services.find((service) => service.id === serviceDto.id)
        if (!service) return Promise.resolve({ error: 'Service not found' })
        Object.assign(service, serviceDto)
        return Promise.resolve({ result: service })
    }

    static async deleteById(id: number): Promise<FetchResponse<any>> {
        const service = services.find((service) => service.id === id)
        if (!service) return Promise.resolve({ error: 'Service not found' })
        const filteredServices = services.filter((service) => service.id !== id)
        services = filteredServices
        return Promise.resolve({ result: service })
    }
}

export type CreateServiceRequest = Omit<Service, 'id'>
export type EditServiceRequest = Service
