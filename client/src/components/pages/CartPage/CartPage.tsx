'use client'

import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from "@/hooks/carts.hook"
import { TableColumnData } from '@/types/components/table'
import { ProductCartItem, ServiceCartItem } from '@/types/models'
import { useEffect, useMemo } from 'react'

type Props = {
    cartId: number
}

const CartPage = (props: Props) => {
    const { cartId } = props

    // const { cart, isLoading, isError } = useCart(cartId)
    const {
        cartItems,
        errorMsg,
        isLoading,
        setCartItems
    } = useCartItems(cartId, 0)

    const productsTable = () => {
        const productItems: ProductCartItem[] = cartItems.filter((cartItem) => cartItem.type === 'product')
        const productsColumns: TableColumnData[] = [
            { name: 'Product', key: 'product' },
            { name: 'Quantity', key: 'quantity' },
            { name: 'Price', key: 'price' },
            { name: 'Total', key: 'total' },
        ]
        const productsRows = [
            ...productItems.map((productItem) => ({
                product: productItem.product?.name,
                quantity: productItem.quantity,
                price: productItem.product?.price,
                total: productItem.quantity * (productItem.product?.price ? productItem.product.price : 0),
            })),
            {
                product: 'Total',
                quantity: '...',
                // @ts-ignore
                price: '...',
                // @ts-ignore
                total: productItems.reduce((acc, item) => acc + item.quantity * (item.product?.price ? item.product.price : 0), 0),
            }
        ]

        return (
            <Table
                isLoading={isLoading}
                errorMsg={errorMsg}
                columns={productsColumns}
                rows={productsRows}
                lastRowHighlight
            />
        )
    }

    const servicesTable = () => {
        const serviceItems: ServiceCartItem[] = cartItems.filter((cartItem) => cartItem.type === 'service')
        const serviceColumns: TableColumnData[] = [
                { name: 'Product', key: 'product' },
                { name: 'Time', key: 'time' },
                { name: 'Price', key: 'price' },
                { name: 'Total', key: 'total' },
            ]
        const serviceRows = [
            ...serviceItems.map((serviceItem) => ({
                product: serviceItem.service?.name,
                time: serviceItem.timeSlot?.startTime?.toLocaleString(),
                price: serviceItem.service?.price,
                total: serviceItem.quantity * (serviceItem.service?.price ? serviceItem.service.price : 0),
            })),
            {
                product: 'Total',
                time: '...',
                // @ts-ignore
                price: '...',
                total: serviceItems.reduce((total, item) => total + item.quantity * (item.service?.price ? item.service.price : 0), 0)
            }
        ]
        return (
            <Table
                isLoading={isLoading}
                errorMsg={errorMsg}
                columns={serviceColumns}
                rows={serviceRows}
                lastRowHighlight
            />
        )
    }



    return (
        <div>
            <h1>{`Items of cart: ${cartId}`}</h1>
            <div>
                <h4>Products</h4>
                {productsTable()}
            </div>
            <div>
                <h4>Serivces</h4>
                {servicesTable()}
            </div>
        </div>
    )
}

export default CartPage
