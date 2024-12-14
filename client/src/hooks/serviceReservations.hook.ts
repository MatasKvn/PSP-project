import { useState, useEffect } from 'react'
import { ServiceReservation } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import ServiceReservationApi from '@/api/serviceReservation.api'

export const useServiceReservations = (pageNumber: number) => {
    const [serviceReservations, setServiceReservations] = useState<ServiceReservation[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            try {
                setIsLoading(true);
                const response = await ServiceReservationApi.getAllReservations(pageNumber);
                if (!response.result) {
                    setErrorMsg('Failed to get time slots')
                    setIsLoading(false);
                    return;
                }
                setServiceReservations(response.result.results);
            } catch (error: any) {
                setErrorMsg(error.message || 'An error occurred');
            } finally {
                setIsLoading(false);
            }
        };
        handleFetch();
    }, [pageNumber]);

    return { serviceReservations, setServiceReservations, isLoading, errorMsg }
}