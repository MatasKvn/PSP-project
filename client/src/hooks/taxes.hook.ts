import { useState, useEffect } from 'react'
import { Product, Service, Tax } from '@/types/models'
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

export const useTaxedItems = (selectedTax: Tax | undefined) => {
    const [appliedProducts, setAppliedProducts] = useState<Product[]>([])
    const [appliedServices, setAppliedServices] = useState<Service[]>([])
    const [errorMsg, setErrorMsg] = useState<string>('')
    const [isLoading, setIsLoading] = useState<boolean>(true)
    useEffect(() => {
        if (!selectedTax) return
        const getSelectedTaxItems = async () => {
            const responses = await Promise.all([
                TaxApi.getProductsByTaxId(selectedTax.id),
                TaxApi.getServicesByTaxId(selectedTax.id)
            ])
            const { result: products, error: productsErr } = responses[0]
            const { result: services, error: servicesErr } = responses[1]
            if (!products) {
                setErrorMsg(productsErr!)
                setIsLoading(false)
                return
            }
            if (!services) {
                setErrorMsg(servicesErr!)
                setIsLoading(false)
                return
            }
            setAppliedProducts(products)
            setAppliedServices(services)
        }
        getSelectedTaxItems()
    }, [selectedTax])

    return {
        appliedProducts,
        appliedServices,
        setAppliedProducts,
        setAppliedServices,
        isLoading,
        errorMsg
    }
}