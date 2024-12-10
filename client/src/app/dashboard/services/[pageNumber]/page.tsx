import ServicesPage from '@/components/pages/ServicesPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}
const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <ServicesPage pageNumber={pageNumber} />
    )
}

export default Page
