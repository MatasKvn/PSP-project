'use client'

import Button from '@/components/shared/Button'
import React, { useEffect, useRef, useState } from 'react'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import { useRouter } from 'next/navigation'

import styles from './ServiceReservationsPage.module.scss'
import { useServiceReservations } from '@/hooks/serviceReservations.hook'
import Table from '@/components/shared/Table'
import ServiceReservationApi from '@/api/serviceReservation.api'
import { ServiceReservation } from '@/types/models'
import TimeSlotApi from '@/api/timeSlot.api'

type Props = {
    pageNumber: number
}

const ServiceReservationsPage = ({ pageNumber }: Props) => {
    const { serviceReservations, setServiceReservations, isLoading, errorMsg, refetch } = useServiceReservations(pageNumber)
    const [formattedReservations, setFormattedReservations] = useState<any[]>([])

    useEffect(() => {
        const fetchRows = async () => {
            const updatedReservations = await Promise.all(
                serviceReservations.map(async (reservation) => {
                    const response = await TimeSlotApi.getTimeSlotStartTime(reservation.timeSlotId)

                    return {
                        ...reservation,
                        timeSlotId: response.result
                            ? new Date(response.result).toLocaleString()
                            : 'N/A',
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

        // Prevent redundant cancellation
        if (selectedReservation.isCancelled) {
            console.log("Reservation is already cancelled")
            return
        }

        console.log("Cancelling reservation: ", selectedReservation)

        const response = await ServiceReservationApi.update({
            id: selectedReservation.id,
            cartItemId: Number(selectedReservation.cartItemId),
            timeSlotId: selectedReservation.timeSlotId,
            customerName: selectedReservation.customerName,
            customerPhone: selectedReservation.customerPhone,
            bookingTime: selectedReservation.bookingTime,
            isCancelled: true,
        })

        if (!response.result) {
            console.error(response.error || 'Failed to update the reservation')
            return
        }

        console.log("Reservation cancelled successfully")

        refetch()
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
        </>
    )
}

export default ServiceReservationsPage

function setFormattedReservations(updatedReservations: { timeSlotId: string; bookingTime: string; id: number; cartItemId: number; timeSlot?: import("@/types/models").TimeSlot; customerPhone: string; customerName: string }[]) {
    throw new Error('Function not implemented.')
}
