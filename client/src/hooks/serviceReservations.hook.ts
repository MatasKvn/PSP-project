import { useState, useEffect, useCallback } from 'react'
import { ServiceReservation } from '@/types/models'
import ServiceReservationApi from '@/api/serviceReservation.api'

export const useServiceReservations = (pageNumber: number) => {
    const [serviceReservations, setServiceReservations] = useState<ServiceReservation[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    const fetchReservations = useCallback(async () => {
        try {
            setIsLoading(true);
            const response = await ServiceReservationApi.getAllReservations(pageNumber);
            if (!response.result) {
                setErrorMsg('Failed to get reservations');
                setIsLoading(false)
                return;
            }
            setServiceReservations(response.result.results)
        } catch (error: any) {
            setErrorMsg(error.message || 'An error occurred')
        } finally {
            setIsLoading(false);
        }
    }, [pageNumber])

    useEffect(() => {
        fetchReservations()
    }, [fetchReservations])

    return {
        serviceReservations,
        setServiceReservations,
        isLoading,
        errorMsg,
        refetch: fetchReservations,
    }
}
