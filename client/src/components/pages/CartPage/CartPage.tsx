'use client'

import { useCart } from "@/hooks/carts.hook"

type Props = {
    cartId: number
}

const CartPage = (props: Props) => {
    const { cartId } = props

    const { cart, isLoading, isError } = useCart(cartId)
    if (isError && !isLoading) return <div>Cart not found</div>

    return (
        <div>
            {
                isLoading ? (
                    <div>...Loading</div>
                ) : (
                    <div>{`Viewing cart: ${cart?.id}`}</div>
                )
            }
        </div>
    )
}

export default CartPage
