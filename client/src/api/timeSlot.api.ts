import { FetchResponse } from '@/types/fetch'
import { TimeSlot } from '@/types/models'

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
}