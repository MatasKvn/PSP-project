import { publicRoutes } from './../constants/route'
import { useState, useEffect } from 'react'
import { ItemDiscount, Product, Service } from '@/types/models'
import ItemDiscountApi from '@/api/itemDiscount.api'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import ProductApi from '@/api/product.api'
import ServiceApi from '@/api/service.api'

export const useDiscounts = (pageNumber: number, compareFn?: (a: ItemDiscount, b: ItemDiscount) => number) => {
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

export const useDiscountedItems = (discount: ItemDiscount | undefined) => {
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])
    const [selectedServices, setSelectedServices] = useState<Service[]>([])
    const [errorMsg, setErrorMsg] = useState<string>('')
    const [isLoading, setIsLoading] = useState<boolean>(true)

    useEffect(() => {
        if (!discount) return
        const handleFetch = async () => {
            const responses = await Promise.all([
                ProductApi.getProductsByDiscountId(discount.id),
                ServiceApi.getServicesByDiscountId(discount.id)
            ])
            const { result: products, error: productsErr } = responses[0]
            const { result: services, error: servicesErr } = responses[1]
            if (!products || !services) {
                setErrorMsg(productsErr || servicesErr || 'Failed to get discounted items')
                setIsLoading(false)
                return
            }
            setSelectedProducts(products)
            setSelectedServices(services)
            setIsLoading(false)
        }
        handleFetch()
    }, [discount])

    return {
        selectedProducts,
        setSelectedProducts,
        selectedServices,
        setSelectedServices,
        errorMsg,
        isLoading
    }
}