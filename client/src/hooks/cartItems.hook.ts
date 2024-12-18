import { useState, useEffect, useCallback } from 'react'
import { CartItem, RequiredCartItem, ProductCartItem, ServiceCartItem, RequiredProductCartItem, Tax } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import CartItemApi from '@/api/cartItem.api'
import CartItemMapper from '@/mappers/cartItem.mapper'
import ProductApi from '@/api/product.api'
import ProductModificationApi from '@/api/productModification.api'
import ServiceApi from '@/api/service.api'
import ServiceReservationApi from '@/api/serviceReservation.api'
import TimeSlotApi from '@/api/timeSlot.api'
import ItemDiscountApi from '@/api/itemDiscount.api'
import TaxApi from '@/api/tax.api'


const getProductCartItemSubItems = async (cartItem: ProductCartItem): Promise<string | RequiredProductCartItem> => {
    // FIXME: optmize
    const productResponse = await ProductApi.getProductById(cartItem.productId)
    if (!productResponse.result) return productResponse.error || 'Failed to get product'
    const product = productResponse.result
    const productModificationsResponse = await ProductModificationApi.getByCartItemId(cartItem.id)
    console.log(productModificationsResponse)
    if (!productModificationsResponse.result) return productModificationsResponse.error || 'Failed to get product modifications'
    const productModifications = productModificationsResponse.result
    const discountsReponse = await ItemDiscountApi.getCurrentDiscountsByProductId(cartItem.productId)
    if (!discountsReponse.result) return discountsReponse.error || 'Failed to get discounts'
    const discounts = discountsReponse.result
    const taxesResponse = await TaxApi.getTaxesByProductId(cartItem.productId)
    if (!taxesResponse.result) return taxesResponse.error || 'Failed to get taxes'
    const taxes = taxesResponse.result

    return {
        ...cartItem,
        product: product,
        productModifications,
        discounts,
        taxes
    }
}

const getServiceCartItemSubItems = async (cartItem: ServiceCartItem): Promise<string | RequiredCartItem> => {
    // FIXME: optmize
    const serviceResponse = await ServiceApi.getById(cartItem.serviceId)
    if (!serviceResponse.result) return serviceResponse.error || 'Failed to get service'
    const service = serviceResponse.result
    const reservationResponse = await ServiceReservationApi.getReservationById(cartItem.serviceReservationId)
    if (!reservationResponse.result) return reservationResponse.error || 'Failed to get reservation'
    const reservation = reservationResponse.result
    const timeSlotResponse = await TimeSlotApi.getTimeSlotById(cartItem.timeSlotId)
    if (!timeSlotResponse.result) return timeSlotResponse.error || 'Failed to get time slot'
    const timeSlot = timeSlotResponse.result
    const discountsResponse = await ItemDiscountApi.getCurrentDiscountByServiceId(cartItem.serviceId)
    if (!discountsResponse.result) return discountsResponse.error || 'Failed to get discounts'
    const discounts = discountsResponse.result
    const taxesResponse = await TaxApi.getTaxesByServiceId(cartItem.serviceId)
    if (!taxesResponse.result) return taxesResponse.error || 'Failed to get taxes'
    const taxes = taxesResponse.result

    return {
        ...cartItem,
        service,
        serviceReservation: reservation,
        timeSlot,
        discounts,
        taxes
    }
}

const getCartItemSubItems = async (cartItem: CartItem): Promise<string | RequiredCartItem> => {
    if (cartItem.type === 'product') {
        return getProductCartItemSubItems(cartItem)
    }
    return getServiceCartItemSubItems(cartItem)
}

const isCartItems = (items: (string | RequiredCartItem)[]): items is RequiredCartItem[] => {
    return items.every((item) => typeof item !== 'string')
}

export const useCartItems = (cartId: number, pageNumber: number) => {
    const [cartItems, setCartItems] = useState<RequiredCartItem[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [error, setError] = useState<string>('')


    const fail = (errMsg: string) => {
        setError(errMsg)
        setIsLoading(false)
    }

    const handleFetch = useCallback(async () => {
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
        setCartItems(cartsExtended as RequiredCartItem[])
        setIsLoading(false)
    }, [cartId, pageNumber])

    useEffect(() => {

        handleFetch()
    }, [cartId, pageNumber, handleFetch])

    return { cartItems, setCartItems, isLoading, errorMsg: error, refetchCartItems: handleFetch }
}