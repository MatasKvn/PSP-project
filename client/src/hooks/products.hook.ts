import ProductApi from '@/api/product.api'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import { Product } from '@/types/models'
import { useEffect, useState } from 'react'

export const useProducts = (pageNumber: number) => {
    const [products, setProducts] = useState<Product[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [isError, setIsError] = useState<boolean>(false)

    useEffect(() => {
        const handleFetch = async () => {
            const response = await ProductApi.getAllProducts(pageNumber)
            if (response.result) {
                const products = PagedResponseMapper.fromPageResponse(response.result!)
                setProducts(products)
                setIsLoading(false)
                return
            }
            setIsError(true)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber])
    
    return { products, setProducts, isLoading, isError }
}