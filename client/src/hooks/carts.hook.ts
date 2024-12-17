import { useState, useEffect } from 'react'
import { Cart, CartStatusEnum } from '@/types/models'
import CartApi from '@/api/cart.api'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'

export const useCarts = (pageNumber: number) => {
    const [carts, setCarts] = useState<Cart[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [isError, setIsError] = useState<boolean>(false)

    useEffect(() => {
        const handleFetch = async () => {
            const response = await CartApi.getAllCarts(pageNumber)
            if (response.result) {
                const carts = PagedResponseMapper.fromPageResponse(response.result!)
                setCarts(carts)
                setIsLoading(false)
                return
            }
            setIsError(true)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber])

    return { carts, setCarts, isLoading, isError }
}

export const useCart = (cartId: number) => {
    const [cart, setCart] = useState<Cart | undefined>(undefined)
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [isError, setIsError] = useState<boolean>(false)
    const [isCartOpen, setIsCartOpen] = useState<boolean>(false);

    useEffect(() => {
        const handleFetch = async () => {
            const response = await CartApi.getCartById(cartId)
            if (response.result) {
                setCart(response.result!)
                setIsLoading(false)
                setIsCartOpen(response.result.status === CartStatusEnum.IN_PROGRESS)
                return
            }
            
            setIsError(true)
            setIsLoading(false)
        }
        handleFetch()
    }, [cartId])

    return { cart, setCart, isLoading, isError, isCartOpen, setIsCartOpen }
}