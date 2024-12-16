import { HTTPMethod, FetchResponse, PagedResponse } from '@/types/fetch'
import { Service } from './../types/models'
import { fetch } from '@/utils/fetch'
import { apiBaseUrl, defaultHeaders } from '../constants/api'

export default class ServiceApi {
    static async getAllServices(pageNumber: number): Promise<FetchResponse<PagedResponse<Service>>> {
        return await fetch({
            url: `${apiBaseUrl}/services?pageNum=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        })
    }

    static async getById(id: number): Promise<FetchResponse<Service>> {
        return await fetch({
            url: `${apiBaseUrl}/services/${id}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        })
    }

    static async create(serviceDto: Service): Promise<FetchResponse<Service>> {
        return await fetch({
            url: `${apiBaseUrl}/services`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(serviceDto)
        })
    }

    static async update(serviceDto: Service): Promise<FetchResponse<Service>> {
        return await fetch({
            url: `${apiBaseUrl}/services/${serviceDto.id}`,
            method: HTTPMethod.PUT,
            headers: defaultHeaders,
            body: JSON.stringify(serviceDto)
        })
    }

    static async deleteById(id: number): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/services/${id}`,
            method: HTTPMethod.DELETE,
            headers: defaultHeaders
        })
    }
}
