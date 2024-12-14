import { FetchResponse, PagedResponse } from '@/types/fetch'
import { ServiceReservation } from '@/types/models'

let reservations: ServiceReservation[] = [
    {
        id: 1,
        cartItemId: 1,
        timeSlotId: 1,
        bookingTime: new Date(),
        customerPhone: '1234567890',
        customerName: 'John Doe'
    },
    {
        id: 2,
        cartItemId: 2,
        timeSlotId: 2,
        bookingTime: new Date(),
        customerPhone: '1234567890',
        customerName: 'John Doe'
    },
    {
        id: 3,
        cartItemId: 3,
        timeSlotId: 3,
        bookingTime: new Date(),
        customerPhone: '1234567890',
        customerName: 'John Doe'
    },
]

export default class ServiceReservationApi {
    static async getReservationById(id: number): Promise<FetchResponse<ServiceReservation>> {
        return {
            result: {
                id,
                cartItemId: 1,
                timeSlotId: 1,
                bookingTime: new Date(),
                customerPhone: '1234567890',
                customerName: 'John Doe'
            }
        }
    }

    static async getAllReservations(pageNumber: number): Promise<FetchResponse<PagedResponse<ServiceReservation>>> {
        return Promise.resolve({
            result: {
                totalCount: reservations.length,
                pageSize: 35,
                pageNum: pageNumber,
                results: reservations
            }
        })
    }

    // static async create(timeSlotDto: CreateTimeSlotRequest): Promise<FetchResponse<TimeSlot>> {
    //     const maxId = Math.max(...timeSlots.map((timeSlot) => timeSlot.id))
    //     const result = {
    //         ...timeSlotDto,
    //         id: maxId + 1
    //     }
    //     timeSlots = [...timeSlots, result]
    //     return Promise.resolve({ result })
    // }

    // static async update(timeSlotDto: TimeSlot): Promise<FetchResponse<TimeSlot>> {
    //     const timeSlot = timeSlots.find((timeSlot) => timeSlot.id === timeSlotDto.id)
    //     if (!timeSlot) return Promise.resolve({ error: 'TimeSlot not found' })
    //     Object.assign(timeSlot, timeSlotDto)
    //     return Promise.resolve({ result: timeSlot })
    // }
}

export type CreateTimeSlotRequest = Omit<ServiceReservation, 'id'>