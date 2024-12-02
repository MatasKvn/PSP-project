import ProductsPage from '@/components/pages/ProductsPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}

const Page = async ({ params }: Props) => {
    const { pageNumber } = await params

    return (
        <ProductsPage
            pageNumber={pageNumber}
        />
    )
}

export default Page
