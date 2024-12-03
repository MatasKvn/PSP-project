import ProductModificationApi from '@/api/productModification.api'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import { ProductModification } from '@/types/models'
import { useEffect, useState } from 'react'

export const useProductModifications = (productId: number | undefined, pageNumber: number, compareFn?: (a: ProductModification, b: ProductModification) => number) => {
    const [productModifications, setProductModifications] = useState<ProductModification[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [isError, setIsError] = useState<boolean>(false)

    useEffect(() => {
        if (!productId) {
            setIsLoading(false)
            return
        }
        const handleFetch = async () => {
            const response = await ProductModificationApi.getByProductId(productId, pageNumber)
            console.log(response)
            if (response.result) {
                const productModifications = PagedResponseMapper.fromPageResponse(response.result!)
                if (compareFn) productModifications.sort(compareFn)
                setProductModifications(productModifications)
                setIsLoading(false)
                return
            }
            setIsError(true)
            setIsLoading(false)
        }
        handleFetch()
    }, [productId, pageNumber, compareFn])

    return {
        productModifications: productModifications,
        setProductModifications: setProductModifications,
        isLoading,
        isError
    }
}