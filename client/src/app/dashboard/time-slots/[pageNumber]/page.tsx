import TimeSlotsPage from '@/components/pages/TimeSlotsPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}
const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <TimeSlotsPage pageNumber={pageNumber} />
    )
}

export default Page
