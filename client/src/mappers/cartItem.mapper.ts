import { CartItemResponse } from '@/api/cartItem.api'
import { CartItem } from './../types/models'
export default class CartItemMapper {
    static fromReponse(responseDto: CartItemResponse): CartItem {

        const isProduct = responseDto.isProduct

        if (isProduct) {
            return {
                type: 'product',
                id: responseDto.id,
                cartId: responseDto.cartId,
                quantity: responseDto.quantity,
                productId: responseDto.productVersionId,
            }
        }

        return {
            type: 'service',
            id: responseDto.id,
            cartId: responseDto.cartId,
            quantity: responseDto.quantity,
            serviceId: responseDto.serviceVersionId,
            serviceReservationId: responseDto.serviceReservationId,
            timeSlotId: responseDto.timeSlotId,

        }
    }
}