import ServiceReservationsPage from '@/components/pages/ServiceReservations'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}
const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <ServiceReservationsPage pageNumber={pageNumber} />
    )
}

export default Page
