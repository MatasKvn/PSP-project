import { apiBaseUrl } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { ServiceReservation } from '@/types/models'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'

export default class ServiceReservationApi {
    static async getReservationById(id: number): Promise<FetchResponse<ServiceReservation>> {
        return await fetch({
            url: `${apiBaseUrl}/service-reservation/${id}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getAllReservations(pageNumber: number): Promise<FetchResponse<PagedResponse<ServiceReservation>>> {
        return await fetch({
            url: `${apiBaseUrl}/service-reservation?pageNumber=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async create(reservationDto: CreateServiceReservationRequest): Promise<FetchResponse<ServiceReservation>> {
        return await fetch({
            url: `${apiBaseUrl}/service-reservation`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(reservationDto)
        })
    }

    static async update(reservationDto: ServiceReservation): Promise<FetchResponse<ServiceReservation>> {
        return await fetch({
            url: `${apiBaseUrl}/service-reservation/${reservationDto.id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(reservationDto)
        })
    }
}

export type CreateServiceReservationRequest = Omit<ServiceReservation, 'id'>