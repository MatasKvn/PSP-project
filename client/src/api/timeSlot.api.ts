import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse} from '@/types/fetch'
import { fetch, getAuthorizedHeaders } from '@/utils/fetch'
import { TimeSlot } from '@/types/models'

export default class TimeSlotApi {

    static async getTimeSlotById(id: number): Promise<FetchResponse<TimeSlot>> {
        return await fetch({
            url: `${apiBaseUrl}/time-slot/${id}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getAllTimeSlots(pageNumber: number): Promise<FetchResponse<PagedResponse<TimeSlot>>> {
        return await fetch({
            url: `${apiBaseUrl}/time-slot?pageNum=${pageNumber}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async getAllTimeSlotsByEmployeeIdAndAvailability(employeeId: number, isAvailable: boolean): Promise<FetchResponse<TimeSlot[]>> {
        const response = await this.getAllTimeSlots(0);
    
        if (!response.result) {
            return Promise.resolve({ result: [] });
        }

        const values = response.result.results.filter(
            (timeSlot: TimeSlot) => timeSlot.employeeVersionId === employeeId && timeSlot.isAvailable === isAvailable
        );
    
        return Promise.resolve({ result: values });
    }
    static async create(timeSlotDto: CreateTimeSlotRequest): Promise<FetchResponse<TimeSlot>> {
        return await fetch({
            url: `${apiBaseUrl}/time-slot`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(timeSlotDto)
        })
    }

    static async update(timeSlotDto: TimeSlot): Promise<FetchResponse<TimeSlot>> {
        return await fetch({
            url: `${apiBaseUrl}/time-slot/${timeSlotDto.id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(timeSlotDto)
        })
    }

    static async getTimeSlotStartTime(timeSlotId: number): Promise<FetchResponse<Date>> {
        const response = await TimeSlotApi.getTimeSlotById(timeSlotId);
    
        if (!response || !response.result) {
            console.error('Failed to retrieve time slot:', response?.error || 'Unknown error')
            return Promise.resolve({ result: undefined })
        }
    
        const timeSlot = response.result
    
        if (!timeSlot || !timeSlot.startTime) {
            console.error('Time slot or start time is undefined:', timeSlot)
            return Promise.resolve({ result: undefined });
        }
    
        return Promise.resolve({ result: new Date(timeSlot.startTime) })
    }
}

export type CreateTimeSlotRequest = Omit<TimeSlot, 'id'>