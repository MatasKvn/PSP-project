import React from 'react'
import CartPage from "@/components/pages/CartPage"

type Props = {
    params: {
        cartId: number
    }
    searchParams: {
        pageNumber: number
    }
}

const Page = async ({ params, searchParams }: Props) => {
    const { cartId } = await params
    const { pageNumber } = await searchParams

    return <CartPage
        cartId={cartId}
        pageNumber={pageNumber}
    />
}

export default Page
