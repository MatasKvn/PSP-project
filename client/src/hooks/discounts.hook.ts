import { useState, useEffect } from 'react'
import { ItemDiscount } from '@/types/models'
import ItemDiscountApi from '@/api/itemDiscount.api'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'

export const useCarts = (pageNumber: number, compareFn?: (a: ItemDiscount, b: ItemDiscount) => number) => {
    const [discounts, setDiscounts] = useState<ItemDiscount[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            const response = await ItemDiscountApi.getAllDiscounts(pageNumber)
            if (!response.result) {
                setErrorMsg(response.error || 'Failed to get discounts')
                setIsLoading(false)
                return
            }
            const carts = PagedResponseMapper.fromPageResponse(response.result)
            if (compareFn) carts.sort(compareFn)
            setDiscounts(carts)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber, compareFn])

    return { discounts, setDiscounts, isLoading, errorMsg }
}