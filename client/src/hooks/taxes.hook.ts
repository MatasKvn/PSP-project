import { useState, useEffect } from 'react'
import { Tax } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import TaxApi from '@/api/tax.api'

export const useTaxes = (pageNumber: number) => {
    const [taxes, setTaxes] = useState<Tax[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            const response = await TaxApi.getAllTaxes(pageNumber)
            if (!response.result) {
                setErrorMsg(response.error || 'Failed to get taxes')
                setIsLoading(false)
                return
            }
            const taxes = PagedResponseMapper.fromPageResponse(response.result!)
            setTaxes(taxes)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber])

    return { taxes, setTaxes, isLoading, errorMsg }
}