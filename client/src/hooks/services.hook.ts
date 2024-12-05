import { useState, useEffect } from 'react'
import { Service } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import ServiceApi from '@/api/service.api'

export const useServices = (pageNumber: number) => {
    const [services, setServices] = useState<Service[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            const response = await ServiceApi.getAllServices(pageNumber)
            if (!response.result) {
                setErrorMsg(response.error!)
                setIsLoading(false)
                return
            }
            const services = PagedResponseMapper.fromPageResponse(response.result!)
            setServices(services)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber])

    return { services, setServices, isLoading, errorMsg }
}