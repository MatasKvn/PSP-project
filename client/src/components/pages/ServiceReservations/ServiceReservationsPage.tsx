'use client'

import Button from '@/components/shared/Button'
import React, { useEffect, useState } from 'react'
import { useServiceReservations } from '@/hooks/serviceReservations.hook'
import Table from '@/components/shared/Table'
import ServiceReservationApi from '@/api/serviceReservation.api'
import { ServiceReservation } from '@/types/models'
import TimeSlotApi from '@/api/timeSlot.api'
import { useRouter } from 'next/navigation'
import { GetPageUrl } from '@/constants/route'
import PageChanger from '@/components/shared/PageChanger'

type Props = {
    pageNumber: number
}

const ServiceReservationsPage = ({ pageNumber }: Props) => {
    const { serviceReservations, setServiceReservations, isLoading, errorMsg, refetch } = useServiceReservations(pageNumber)
    const [formattedReservations, setFormattedReservations] = useState<any[]>([])
    const router = useRouter()

    useEffect(() => {
        const fetchRows = async () => {
            const updatedReservations = await Promise.all(
                serviceReservations.map(async (reservation) => {
                    let timeSlot = 'N/A'
                    if (reservation.timeSlotId){
                        const response = await TimeSlotApi.getTimeSlotStartTime(reservation.timeSlotId)
                        if (!response.result) {
                            console.error(response.result)
                            return
                        }

                        timeSlot = response.result.toLocaleTimeString()
                    }

                    return {
                        ...reservation,
                        timeSlotId: timeSlot,
                        bookingTime: new Date(reservation.bookingTime).toLocaleString(),
                        isCancelled: reservation.isCancelled ? 'Yes' : 'No',
                        actions: reservation.isCancelled ? (
                            <span>Cancelled</span>
                        ) : (
                            <Button onClick={() => cancelReservation(reservation)}>
                                Cancel
                            </Button>
                        ),
                    }
                })
            )
            setFormattedReservations(updatedReservations)
        }

        if (serviceReservations.length > 0) {
            fetchRows()
        }
    }, [serviceReservations])

    const cancelReservation = async (selectedReservation: ServiceReservation) => {
        if (!selectedReservation) {
            console.error("No reservation selected")
            return
        }

        if (selectedReservation.isCancelled) {
            console.log("Reservation is already cancelled")
            return
        }

        console.log("Cancelling reservation: ", selectedReservation)

        const response = await ServiceReservationApi.update({
            id: selectedReservation.id,
            cartItemId: Number(selectedReservation.cartItemId),
            // @ts-ignore
            timeSlotId: null,
            customerName: selectedReservation.customerName,
            customerPhone: selectedReservation.customerPhone,
            bookingTime: selectedReservation.bookingTime,
            isCancelled: true,
        })
        if (!response.result) {
            console.error(response.error || 'Failed to update the reservation')
            return
        }

        enableTimeSlot(selectedReservation.timeSlotId)

        console.log("Reservation cancelled successfully")

        setServiceReservations([
            ...serviceReservations.filter((reservation) => reservation.id !== response.result?.id),
            response.result
        ])
    }

    const enableTimeSlot = async (timeSlotId: number) => {
        const response  = await TimeSlotApi.getTimeSlotById(timeSlotId)

        if (!response.result) {
            console.error(response.error || 'Failed to update time slot')
            return
        }

        const update = await TimeSlotApi.update({
            id: response.result.id,
            employeeVersionId: response.result.employeeVersionId,
            startTime: response.result.startTime,
            isAvailable: true
        })

        if (!update.result) {
            console.error(update.error || 'Failed to update time slot')
            return
        }
    }

    const columns = [
        { name: 'ID', key: 'id' },
        { name: 'Time Slot', key: 'timeSlotId' },
        { name: 'Booking Time', key: 'bookingTime' },
        { name: 'Customer Name', key: 'customerName' },
        { name: 'Customer Phone', key: 'customerPhone' },
        { name: 'Cancelled', key: 'isCancelled' },
        { name: 'Actions', key: 'actions' },
    ]

    return (
        <>
            <h1>Reservations</h1>
            <Table
                columns={columns}
                rows={formattedReservations}
                isLoading={isLoading}
                errorMsg={errorMsg}
            />
            <PageChanger
                onClickNext={() => router.push(GetPageUrl.serviceReservations(parseInt(pageNumber as unknown as string) + 1))}
                onClickPrevious={() => router.push(GetPageUrl.serviceReservations(pageNumber - 1))}
                disabledPrevious={pageNumber <= 0}
                pageNumber={pageNumber}
            />
        </>
    )
}

export default ServiceReservationsPage
