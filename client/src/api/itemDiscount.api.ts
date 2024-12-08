import { FetchResponse } from '@/types/fetch'
import { ItemDiscount } from '@/types/models'

const discounts: ItemDiscount[] = [
    {
        id: 1,
        value: 10,
        isPercentage: true,
        description: '10% off',
        startDate: new Date('2022-01-01 00:00:00'),
        endDate: new Date('2025-12-31 23:59:59')
    },
    {
        id: 2,
        value: 20,
        isPercentage: true,
        description: '20% off',
        startDate: new Date('2022-01-01 00:00:00'),
        endDate: new Date('2022-12-31 23:59:59')
    }
]

export default class ItemDiscountApi {
    static async getCurrentDiscountsByProductId(productId: number): Promise<FetchResponse<ItemDiscount[]>> {
        const result = discounts.filter(discount => discount.startDate <= new Date() && discount.endDate >= new Date() && discount.id === productId)
        return Promise.resolve({ result })
    }

    static async getCurrentDiscountByServiceId(serviceId: number): Promise<FetchResponse<ItemDiscount[]>> {
        const result = discounts.filter(discount => discount.startDate <= new Date() && discount.endDate >= new Date() && discount.id === serviceId)
        return Promise.resolve({ result })
    }
}