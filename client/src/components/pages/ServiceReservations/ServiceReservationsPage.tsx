'use client'

import Button from '@/components/shared/Button'
import React, { useRef, useState } from 'react'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import { useRouter } from 'next/navigation'

import styles from './ServiceReservationsPage.module.scss'
import { useServiceReservations } from '@/hooks/serviceReservations.hook'
import Table from '@/components/shared/Table'
import ServiceReservationApi from '@/api/serviceReservation.api'
import { ServiceReservation } from '@/types/models'

type Props = {
    pageNumber: number
}

const ServiceReservationsPage = ({ pageNumber }: Props) => {
    const {serviceReservations, setServiceReservations, isLoading, errorMsg} = useServiceReservations(pageNumber)
    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    const [selectedReservation, setSelectedReservation] = useState<ServiceReservation | null>(null);
    type ActionType = 'create' | 'edit'
    const [actionType, setActionType] = useState<ActionType>()

    const handleReservationCreate = async (formPayload: DynamicFormPayload) => {

    }

    const handleReservationUpdate = async (formPayload: DynamicFormPayload) => {
        
    }

    const dummyReservationRow = {
        id: 0,
        cartItemId: 0,
        timeSlotId: 0,
        bookingTime: new Date(),
        customerName: '',
        customerPhone: ''
    }
    const rows = serviceReservations.map((reservation) => ({
        ...reservation,
        bookingTime: new Date(reservation.bookingTime).toLocaleString(),
    }))
    const columns = Object.keys(dummyReservationRow).map((key) => ({
        name: key,
        key
    }))

    return (
        <>
            <h1>Reservations</h1>
            <Table
                columns={columns}
                rows={rows}
                isLoading={isLoading}
                errorMsg={errorMsg}
            />
        </>
    )
}

export default ServiceReservationsPage