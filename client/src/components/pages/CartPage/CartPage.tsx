'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from '@/hooks/carts.hook'
import { TableColumnData } from '@/types/components/table'
import { RequiredCartItem, CartStatusEnum, ProductCartItem, ServiceCartItem, ProductModification, RequiredProductCartItem } from '@/types/models'
import { useRef, useState } from 'react'
import CreateProductCartItemForm from '../../specialized/CreateProductCartItemForm.tsx/CreateProductCartItemForm'
import CreateServiceCartItemView from '@/components/specialized/CreateServiceCartItemForm'

type Props = {
    cartId: number
    pageNumber: number
}

const calculateProductModificationsValue = (cartItem: RequiredCartItem) => {
    if (cartItem.type === 'product') {
        return cartItem.productModifications.reduce((acc, modification) => acc + modification.price, 0)
    }
    return 0
}

const calculateDiscountsValue = (cartItem: RequiredCartItem) => {
    const value = cartItem.type === 'product' ? cartItem.product?.price : cartItem.service?.price
    const productModificaitonValue = calculateProductModificationsValue(cartItem)
    const netPrice = (value + productModificaitonValue) * cartItem.quantity
    return cartItem.discounts.reduce((acc, discount) => {
        if (!discount.isPercentage) {
            return acc + discount.value
        }
        return acc + netPrice * discount.value / 100
    }, 0)
}

const calculateTaxesValue = (cartItem: RequiredCartItem) => {
    const value = cartItem.type === 'product' ? cartItem.product?.price : cartItem.service?.price
    const taxableValue = (value + calculateProductModificationsValue(cartItem)) * cartItem.quantity
    return cartItem.taxes.reduce((acc, tax) => {
        if (tax.isPercentage) {
            return acc + taxableValue * tax.rate / 100
        }
        return acc + tax.rate
    }, 0)
}

const CartPage = (props: Props) => {
    const { cartId, pageNumber } = props

    const { cart, isLoading: isCartLoading } = useCart(cartId)
    const isCartOpen = cart?.status === CartStatusEnum.PENDING
    const {
        cartItems,
        errorMsg,
        isLoading: isCartItemsLoading,
        refetchCartItems
    } = useCartItems(cartId, pageNumber)

    console.log(cartItems)

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'createProduct' | 'createService' | 'none'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('none')

    const handleCartItemDelete = async (cartItemId: number) => {
        const response = await CartItemApi.deleteCartItem(cartItemId)
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
    }

    const productsTable = () => {
        const productItems = cartItems.filter((cartItem) => cartItem.type === 'product')
        const productsColumns: TableColumnData[] = [
            { name: 'Name', key: 'name' },
            { name: 'Quantity', key: 'quantity' },
            { name: 'Price', key: 'price' },
            { name: 'Modification Ids', key: 'modifications' },
            { name: 'Modifications Total', key: 'modificationTotal' },
            { name: 'Total Value', key: 'totalVal' },
            { name: 'Discounts', key: 'discounts' },
            { name: 'Taxes', key: 'taxes' },
            { name: 'Net price', key: 'netPrice' },
            { name: 'Delete', key: 'delete' }
        ]
        const productRows = productItems.map((item) => {
            const { name, price } = item.product
            const modificationsPrice = calculateProductModificationsValue(item)
            const totalVal = item.quantity * (item.product.price + modificationsPrice)
            const discounts = calculateDiscountsValue(item)
            const taxes = calculateTaxesValue(item)
            const netPrice = totalVal - discounts + taxes
            return {
                name,
                quantity: item.quantity,
                modifications: item.productModifications.map((modification) => modification.id).join(', '),
                modificationTotal: modificationsPrice, price, totalVal, discounts, taxes, netPrice,
                delete: (
                    <Button
                        onClick={() => handleCartItemDelete(item.id)}
                    >
                        Delete
                    </Button>
                )
            }
        })

        const summaryRow = {
            name: 'Total',
            quantity: productRows.reduce((acc, row) => acc + row.quantity, 0).toFixed(2),
            price: productRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
            modifications: '...',
            modificationTotal: productRows.reduce((acc, row) => acc + row.modificationTotal, 0).toFixed(2),
            discounts: productRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
            taxes: productRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
            totalVal: productRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
            netPrice: productRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2)
        }
        const rows = [...productRows, summaryRow]

        return (
            <Table
                isLoading={isCartItemsLoading}
                errorMsg={errorMsg}
                columns={productsColumns}
                rows={rows}
                lastRowHighlight
            />
        )
    }

    const servicesTable = () => {
        const serviceItems = cartItems.filter((cartItem) => cartItem.type === 'service')
        const serviceColumns: TableColumnData[] = [
                { name: 'Name', key: 'name' },
                { name: 'Time', key: 'time' },
                { name: 'Price', key: 'price' },
                { name: 'Total Value', key: 'totalVal' },
                { name: 'Discounts', key: 'discounts' },
                { name: 'Taxes', key: 'taxes' },
                { name: 'Net price', key: 'netPrice' },
                { name: 'Delete', key: 'delete' },
            ]
        const serviceRows = serviceItems.map((item) => {
            return {
                name: item.service.name,
                quantity: item.quantity,
                price: item.service?.price,
                totalVal: item.quantity * (item.service?.price ? item.service.price : 0),
                discounts: calculateDiscountsValue(item),
                taxes: calculateTaxesValue(item),
                netPrice: (item.quantity * (item.service?.price ? item.service.price : 0)) - calculateDiscountsValue(item) + calculateTaxesValue(item),
                delete: (
                    <Button
                        onClick={() => handleCartItemDelete(item.id)}
                    >
                        Delete
                    </Button>
                )
            }
        })

        const summaryRow = {
            name: 'Total',
            quantity: serviceRows.reduce((acc, row) => acc + row.quantity, 0).toFixed(2),
            price: serviceRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
            totalVal: serviceRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
            discounts: serviceRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
            taxes: serviceRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
            netPrice: serviceRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2),
        }

        const rows = [...serviceRows, summaryRow]

        return (
            <Table
                isLoading={isCartItemsLoading}
                errorMsg={errorMsg}
                columns={serviceColumns}
                rows={rows}
                lastRowHighlight
            />
        )
    }

    const handleProductItemCreate = async (formPayload: { productId: number; quantity: string; modificationIds: number[] }) => {
        const { productId, quantity, modificationIds } = formPayload
        const quantityParsed = parseInt(quantity)
        if (isNaN(quantityParsed)) {
            console.log('Invalid input')
            return
        }

        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'product',
                quantity: quantityParsed,
                productVersionId: productId,
                variationIds: modificationIds,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const handleServiceCartItemCreate = async ({ serviceId }: { serviceId: number | undefined }) => {
        if (!serviceId) {
            console.log('Invalid input')
            return
        }
        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'service',
                quantity: 1,
                serviceVersionId: serviceId,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'createProduct') return <CreateProductCartItemForm onSubmit={(payload) => handleProductItemCreate(payload)} />
        if (sideDrawerContentType === 'createService') return <CreateServiceCartItemView onSubmit={handleServiceCartItemCreate} />
        return <></>
    }

    return (
        <div>
            <h1>{`Items of cart: ${cartId}`}</h1>
            <div>
                <h4>Products</h4>
                {productsTable()}
                <Button
                    onClick={() => {
                        setSideDrawerContentType('createProduct')
                        sideDrawerRef.current?.open()
                    }}
                    disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                >
                    Add Product
                </Button>
            </div>
            <div>
                <h4>Serivces</h4>
                {servicesTable()}
                <Button
                    onClick={() => {
                        setSideDrawerContentType('createService')
                        sideDrawerRef.current?.open()
                    }}
                    disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                >
                    Add service
                </Button>
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {sideDrawerContent()}
            </SideDrawer>
        </div>
    )
}

export default CartPage
