'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from '@/hooks/carts.hook'
import { TableColumnData } from '@/types/components/table'
import { RequiredCartItem, CartStatusEnum, ProductCartItem, ServiceCartItem, ProductModification, RequiredProductCartItem, RequiredServiceCartItem } from '@/types/models'
import { useRef, useState } from 'react'
import CreateProductCartItemForm from '../../specialized/CreateProductCartItemForm.tsx/CreateProductCartItemForm'
import CreateServiceCartItemView from '@/components/specialized/CreateServiceCartItemForm'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import CartDiscountApi from '@/api/cartDiscount.api'

import styles from './CartPage.module.scss'

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

    const { cart, setCart, isLoading: isCartLoading } = useCart(cartId)

    const isCartOpen = cart?.status === CartStatusEnum.PENDING
    const {
        cartItems,
        errorMsg,
        isLoading: isCartItemsLoading,
        refetchCartItems
    } = useCartItems(cartId, pageNumber)

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'createProduct' | 'createService' | 'none'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('none')
    const [cartDiscount, setCartDiscount] = useState<number>(0)

    if (!isCartLoading && !cart) return null

    const handleCartItemDelete = async (cartItemId: number) => {
        const response = await CartItemApi.deleteCartItem(cartItemId)
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
    }

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
        const { name } = item.product
        const price = item.product.price / 100
        const modificationsPrice = calculateProductModificationsValue(item)
        const totalVal = item.quantity * (item.product.price + modificationsPrice) / 100
        const discounts = calculateDiscountsValue(item) / 100
        const taxes = calculateTaxesValue(item) / 100
        const netPrice = totalVal - discounts + taxes
        return {
            name: name,
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
    const productsRowsStringified = productRows.map((row) => Object.fromEntries(Object.entries(row).map(
        ([key, value]) => {
            if (typeof value === 'number' && key !== 'quantity') {
                return [key, value.toFixed(2)]
            }
            return [key, value]
        })))
    const productsSummaryRow = {
        name: 'Total',
        quantity: productRows.reduce((acc, row) => acc + row.quantity, 0),
        price: productRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
        modifications: '...',
        modificationTotal: productRows.reduce((acc, row) => acc + row.modificationTotal, 0).toFixed(2),
        discounts: productRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
        taxes: productRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
        totalVal: productRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
        netPrice: productRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2)
    }
    const productsTableRows = [...productsRowsStringified, productsSummaryRow]

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
        const startTime = item.timeSlot.startTime
        return {
            name: item.service.name,
            quantity: item.quantity,
            price: item.service?.price,
            time: `${startTime.getMonth() + 1} ${startTime.getDay()} ${startTime.toLocaleTimeString()}`,
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
    const serviceRowsStringified = serviceRows.map((row) => Object.fromEntries(Object.entries(row).map(
        ([key, value]) => {
            console.log(key, value)
            if (typeof value === 'number') {
                return [key, value.toFixed(2)]
            }
            return [key, value]
        })))
    const summaryRow = {
        name: 'Total',
        time: '...',
        quantity: serviceRows.reduce((acc, row) => acc + row.quantity, 0).toFixed(2),
        price: serviceRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
        totalVal: serviceRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
        discounts: serviceRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
        taxes: serviceRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
        netPrice: serviceRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2),
    }
    const servicesTableRows = [...serviceRowsStringified, summaryRow]

    const totalPrice = productRows.reduce((acc, row) => acc + row.netPrice, 0) + serviceRows.reduce((acc, row) => acc + row.netPrice, 0)

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

    const handleCartDiscount = async (formPayload: FormPayload) => {
        const { discount } = formPayload
        const discountParsed = parseInt(discount)
        if (isNaN(discountParsed)) {
            console.log('Invalid input')
            return
        }
        const response = await CartDiscountApi.applyDiscount(cart!, discountParsed)
        if (!response.result?.discount) {
            console.log(response.error)
            return
        }
        setCartDiscount(response.result.discount)
    }

    const handleCartCheckout = () => {
        throw new Error('Checkout not implemented')
    }

    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'createProduct') return <CreateProductCartItemForm onSubmit={(payload) => handleProductItemCreate(payload)} />
        if (sideDrawerContentType === 'createService') return <CreateServiceCartItemView onSubmit={handleServiceCartItemCreate} />
        return <></>
    }

    const tablesSection = () => (
        <div className={styles.tables_container}>
            <div>
                <h4>Products</h4>
                <Table
                    isLoading={isCartItemsLoading}
                    errorMsg={errorMsg}
                    columns={productsColumns}
                    rows={productsTableRows}
                    lastRowHighlight
                />
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
                <h4>Services</h4>
                <Table
                    isLoading={isCartItemsLoading}
                    errorMsg={errorMsg}
                    columns={serviceColumns}
                    rows={servicesTableRows}
                    lastRowHighlight
                />
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
        </div>
    )

    return (
        <div className={styles.page}>
            <h2 className={styles.header}>{`Items of cart: ${cartId}`}</h2>
            {tablesSection()}
            <div className={styles.discount_summary_wrapper}>
                <div className={styles.discount_container}>
                    <h4>Cart Discount</h4>
                    <DynamicForm
                        inputs={{
                            discount: { label: 'Discount', placeholder: 'Enter discount amount:', type: 'number' },
                        }}
                        onSubmit={(formPayload) => handleCartDiscount(formPayload)}
                    >
                        <DynamicForm.Button>Apply Discount</DynamicForm.Button>
                    </DynamicForm>
                </div>
                <div className={styles.summary_container}>
                    <h4>Checkout</h4>
                    <div>
                        <p>{`Total: ${totalPrice.toFixed(2)} €`}</p>
                        <p>{`Discount: ${cartDiscount.toFixed(2)} €`}</p>
                        <p>{`Total: ${(totalPrice - cartDiscount).toFixed(2)} €`}</p>
                        <Button
                            onClick={handleCartCheckout}
                            disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                        >
                            Checkout
                        </Button>
                    </div>
                </div>
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {sideDrawerContent()}
            </SideDrawer>
        </div>
    )
}

export default CartPage
