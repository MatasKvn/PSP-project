import { useState, useEffect } from 'react'
import { BusinessDetails } from '@/types/models'
import BusinessDetailsApi from '@/api/businessDetails.api'

export const useBusinessDetails = () => {
    const [businessDetails, setBusinessDetails] = useState<BusinessDetails | null>(null)
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            const response = await BusinessDetailsApi.getBusinessDetails()
            if (!response.result) {
                setErrorMsg(response.error || 'Failed to get business details')
                setIsLoading(false)
                return
            }
            setBusinessDetails(response.result)
            setIsLoading(false)
        }

        handleFetch()
    }, [])

    return { businessDetails, setBusinessDetails, isLoading, errorMsg }
}