import { FetchResponse } from '@/types/fetch'
import { ServiceReservation } from '@/types/models'

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
}