import { useState, useEffect } from 'react'
import { TimeSlot } from '@/types/models'
import PagedResponseMapper from '@/mappers/pagedResponse.mapper'
import TimeSlotApi from '@/api/timeSlot.api'

export const useTimeSlots = (pageNumber: number) => {
    const [timeSlots, setTimeSlots] = useState<TimeSlot[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    useEffect(() => {
        const handleFetch = async () => {
            try {
                setIsLoading(true);
                const response = await TimeSlotApi.getAllTimeSlots(pageNumber);
                if (!response.result) {
                    setErrorMsg('Failed to get time slots')
                    setIsLoading(false);
                    return;
                }

                const currentDate = new Date();

                const filteredTimeSlots = response.result.results.filter((timeSlot: TimeSlot) => {
                    const timeSlotDate = new Date(timeSlot.startTime);
                    return timeSlotDate > currentDate;
                });
                setTimeSlots(filteredTimeSlots);
            } catch (error: any) {
                setErrorMsg(error.message || 'An error occurred');
            } finally {
                setIsLoading(false);
            }
        };
        handleFetch();
    }, [pageNumber]);

    return { timeSlots, setTimeSlots, isLoading, errorMsg }
}