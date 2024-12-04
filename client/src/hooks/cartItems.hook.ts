import { useState, useEffect } from 'react'
import { CartItem, ProductCartItem, ServiceCartItem } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import CartItemApi from '@/api/cartItem.api'
import CartItemMapper from '@/mappers/cartItem.mapper'
import ProductApi from '@/api/product.api'
import ProductModificationApi from '@/api/productModification.api'
import ServiceApi from '@/api/service.api'
import ServiceReservationApi from '@/api/serviceReservation.api'
import TimeSlotApi from '@/api/timeSlot.api'

async function getProductCartItemSubItems(cartItem: ProductCartItem): Promise<string | ProductCartItem> {
    const product = await ProductApi.getProductById(cartItem.productId)
    if (!product.result) return product.error || 'Failed to get product'
    const productModificationsResponse = await ProductModificationApi.getByCartItemId(cartItem.id, 0)
    if (!productModificationsResponse.result) return productModificationsResponse.error || 'Failed to get product modifications'
    const productModifications = PagedResponseMapper.fromPageResponse(productModificationsResponse.result)
    return {
        ...cartItem,
        product: product.result,
        productModifications
    }
}

// TODO
async function getServiceCartItemSubItems(cartItem: ServiceCartItem): Promise<string | CartItem> {
    const serviceResponse = await ServiceApi.getById(cartItem.serviceId)
    if (!serviceResponse.result) return serviceResponse.error || 'Failed to get service'
    const service = serviceResponse.result
    const reservationResponse = await ServiceReservationApi.getReservationById(cartItem.serviceReservationId)
    if (!reservationResponse.result) return reservationResponse.error || 'Failed to get reservation'
    const reservation = reservationResponse.result
    const timeSlotResponse = await TimeSlotApi.getTimeSlotById(reservation.timeSlotId)
    if (!timeSlotResponse.result) return timeSlotResponse.error || 'Failed to get time slot'
    const timeSlot = timeSlotResponse.result

    return {
        ...cartItem,
        service,
        serviceReservation: reservation,
        timeSlot
    }
}

async function getCartItemSubItems(cartItem: CartItem): Promise<string | CartItem> {
    if (cartItem.type === 'product') {
        return getProductCartItemSubItems(cartItem)
    }
    return getServiceCartItemSubItems(cartItem)
}

function isCartItems(items: (string | CartItem)[]): items is CartItem[] {
    return items.every((item) => typeof item !== 'string')
}


export const useCartItems = (cartId: number, pageNumber: number) => {
    const [cartItems, setCartItems] = useState<CartItem[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [error, setError] = useState<string>('')

    useEffect(() => {
        const fail = (errMsg: string) => {
            setError(errMsg)
            setIsLoading(false)
        }
        const handleFetch = async () => {
            const response = await CartItemApi.getCartItems(cartId, pageNumber)
            if (!response.result) {
                fail(response.error || 'Failed to get cart items')
                return
            }
            const cartItems = PagedResponseMapper.fromPageResponse(response.result)

            const mappedCarts = cartItems.map((cartItem) => CartItemMapper.fromReponse(cartItem))
            const cartsExtended = await Promise.all(mappedCarts.map(async (cartItem) => await getCartItemSubItems(cartItem)))

            const allSucceeded = isCartItems(cartsExtended)
            const failedItem = cartsExtended.find((cartItem) => typeof cartItem === 'string')
            if (!allSucceeded) {
                fail(failedItem || 'Failed to get cart items')
                return
            }
            setCartItems(cartsExtended as CartItem[])
            setIsLoading(false)
        }
        handleFetch()
    }, [cartId, pageNumber])

    return { cartItems, setCartItems, isLoading, errorMsg: error }
}