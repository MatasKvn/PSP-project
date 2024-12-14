import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse} from '@/types/fetch'
import { fetch } from '@/utils/fetch'
import { TimeSlot } from '@/types/models'

let timeSlots: TimeSlot[] = [
    {
        id: 1,
        startTime: new Date(),
        isAvailable: true,
        employeeId: 1
    },
    {
        id: 2,
        startTime: new Date(),
        isAvailable: true,
        employeeId: 1
    },
    {
        id: 3,
        startTime: new Date(),
        isAvailable: true,
        employeeId: 2
    }
]

export default class TimeSlotApi {
    static async getTimeSlotById(id: number): Promise<FetchResponse<TimeSlot>> {
        return {
            result: {
                id,
                startTime: new Date(),
                isAvailable: true,
                employeeId: 1
            }
        }
    }

    static async getAllTimeSlots(pageNumber: number): Promise<FetchResponse<PagedResponse<TimeSlot>>> {
        return Promise.resolve({
            result: {
                totalCount: timeSlots.length,
                pageSize: 35,
                pageNum: pageNumber,
                results: timeSlots
            }
        })
    }

    // static async getAllTimeSlots(pageNumber: number): Promise<FetchResponse<PagedResponse<TimeSlot>>> {
    //     return await fetch({
    //         url: `${apiBaseUrl}/timeSlots?pageNum=${pageNumber}`,
    //         method: HTTPMethod.GET,
    //         headers: defaultHeaders
    //     })
    // }

    static async create(timeSlotDto: CreateTimeSlotRequest): Promise<FetchResponse<TimeSlot>> {
        const maxId = Math.max(...timeSlots.map((timeSlot) => timeSlot.id))
        const result = {
            ...timeSlotDto,
            id: maxId + 1
        }
        timeSlots = [...timeSlots, result]
        return Promise.resolve({ result })
    }

    static async update(timeSlotDto: TimeSlot): Promise<FetchResponse<TimeSlot>> {
        const timeSlot = timeSlots.find((timeSlot) => timeSlot.id === timeSlotDto.id)
        if (!timeSlot) return Promise.resolve({ error: 'TimeSlot not found' })
        Object.assign(timeSlot, timeSlotDto)
        return Promise.resolve({ result: timeSlot })
    }
}

export type CreateTimeSlotRequest = Omit<TimeSlot, 'id'>