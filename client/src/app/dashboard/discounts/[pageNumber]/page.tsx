import DiscountsPage from '@/components/pages/DiscountsPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}

const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <DiscountsPage pageNumber={pageNumber} />
    )
}

export default Page