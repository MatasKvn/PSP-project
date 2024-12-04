import React from 'react'
import CartPage from "@/components/pages/CartPage"

type Props = {
    params: {
        cartId: number
    }
}

const Page = async ({ params }: Props) => {
    const { cartId } = await params

    return <CartPage cartId={cartId} />
}

export default Page
