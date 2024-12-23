import { useState, useEffect } from 'react'
import { Service } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import ServiceApi from '@/api/service.api'

export const useServices = (pageNumber: number | undefined, comparedServices?: (s1: Service, s2: Service) => number) => {
    const [services, setServices] = useState<Service[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            if (pageNumber === undefined) {
                setIsLoading(false)
                return
            }
            const response = await ServiceApi.getAllServices(pageNumber)
            if (!response.result) {
                setErrorMsg(response.error!)
                setIsLoading(false)
                return
            }
            const services = PagedResponseMapper.fromPageResponse(response.result!)
            if (comparedServices) services.sort(comparedServices)
            setServices(services)
            setIsLoading(false)
        }
        handleFetch()
    }, [pageNumber, comparedServices])

    return { services, setServices, isLoading, errorMsg }
}