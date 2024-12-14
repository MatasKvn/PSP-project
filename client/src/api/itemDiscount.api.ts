import { FetchResponse, PagedResponse } from '@/types/fetch'
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

    static async getAllDiscounts(pageNumber: number): Promise<FetchResponse<PagedResponse<ItemDiscount>>> {
        return { result: { pageNum: pageNumber, pageSize: 10, totalCount: discounts.length, results: discounts } }
    }

    static async updateDiscount(dto: UpdateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        const discount = discounts.find(d => d.id === dto.id)
        if (!discount) return { error: 'Discount not found' }
        return { result: { ...discount, ...dto } }
    }

    static async createDiscount(dto: CreateDiscountRequest): Promise<FetchResponse<ItemDiscount>> {
        const discount = {
            ...dto,
            id: Math.max(...discounts.map((discount) => discount.id))
        }
        discounts.push(discount)
        return { result: discount }
    }

    static async deleteDiscount(id: number): Promise<FetchResponse<any>> {
        const discount = discounts.find(d => d.id === id)
        if (!discount) return { error: 'Discount not found' }
        discounts.splice(discounts.indexOf(discount), 1)
        return { result: discount }
    }
}

type CreateDiscountRequest = Omit<ItemDiscount, 'id'>
type UpdateDiscountRequest = ItemDiscount