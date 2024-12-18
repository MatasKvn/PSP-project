import GiftCardsPage from '@/components/pages/GiftCardsPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}

const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <GiftCardsPage
            pageNumber={pageNumber}
        />
    )
}

export default Page