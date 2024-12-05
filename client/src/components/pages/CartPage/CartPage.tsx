'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from '@/hooks/carts.hook'
import { TableColumnData } from '@/types/components/table'
import { CartStatusEnum, ProductCartItem, ServiceCartItem } from '@/types/models'
import { useRef, useState } from 'react'
import CreateProductCartItemForm from '../../specialized/CreateProductCartItemForm.tsx/CreateProductCartItemForm'
import CreateServiceCartItemView from '@/components/specialized/CreateServiceCartItemForm'

type Props = {
    cartId: number
    pageNumber: number
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
        const productItems: ProductCartItem[] = cartItems.filter((cartItem) => cartItem.type === 'product')
        const productsColumns: TableColumnData[] = [
            { name: 'Product', key: 'product' },
            { name: 'Quantity', key: 'quantity' },
            { name: 'Price', key: 'price' },
            { name: 'Modification Ids', key: 'modifications' },
            { name: 'Item Modification Total', key: 'modificationTotal' },
            { name: 'Total', key: 'total' },
            { name: 'Delete', key: 'delete' },
        ]
        const productsRows = [
            ...productItems.map((productCartItem) => {
                const modificationPrice = productCartItem.productModifications!.reduce((acc, modification) => acc + modification.price, 0)
                return {
                    product: productCartItem.product?.name,
                    quantity: productCartItem.quantity,
                    modifications: productCartItem.productModifications?.map((modification) => modification.id).join(', '),
                    modificationTotal: modificationPrice,
                    price: productCartItem.product?.price,
                    total: productCartItem.quantity * (productCartItem.product!.price + modificationPrice),
                    delete: (
                        <Button
                            onClick={() => handleCartItemDelete(productCartItem.id)}
                        >
                            Delete
                        </Button>
                    )
                }
            }),
            {
                product: 'Total',
                quantity: '...',
                // eslint-disable-next-line
                // @ts-ignore
                price: '...',
                // eslint-disable-next-line
                // @ts-ignore
                total: productItems.reduce(
                    (acc, item) => acc + item.quantity *
                        (item.product!.price +
                        item.productModifications!.reduce((acc, modification) => acc + modification.price, 0)),
                    0
                )
            }
        ]

        return (
            <Table
                isLoading={isCartItemsLoading}
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
                { name: 'Delete', key: 'delete' },
            ]
        const serviceRows = [
            ...serviceItems.map((serviceCartItem) => ({
                product: serviceCartItem.service?.name,
                time: serviceCartItem.timeSlot?.startTime?.toLocaleString(),
                price: serviceCartItem.service?.price,
                total: serviceCartItem.quantity * (serviceCartItem.service?.price ? serviceCartItem.service.price : 0),
                delete: (
                    <Button
                        onClick={() => handleCartItemDelete(serviceCartItem.id)}
                    >
                        Delete
                    </Button>
                )
            })),
            {
                product: 'Total',
                time: '...',
                // eslint-disable-next-line
                // @ts-ignore
                price: '...',
                total: serviceItems.reduce((total, item) => total + item.quantity * (item.service?.price ? item.service.price : 0), 0)
            }
        ]
        return (
            <Table
                isLoading={isCartItemsLoading}
                errorMsg={errorMsg}
                columns={serviceColumns}
                rows={serviceRows}
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
