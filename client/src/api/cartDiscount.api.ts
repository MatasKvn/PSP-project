import { FetchResponse } from '@/types/fetch'
import { Cart, CartStatusEnum } from './../types/models'
export default class CartDiscountApi {
    static async applyDiscount(cart: Cart, ammout: number): Promise<FetchResponse<Cart>> {
        if (cart.status !== CartStatusEnum.PENDING) {
            return Promise.resolve({
                error: 'Cannot apply discount to non-pending cart.'
            })
        }
        return Promise.resolve({
            result: {
                ...cart,
                discount: ammout
            }
        })
    }
}