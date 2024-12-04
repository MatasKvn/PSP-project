'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { TableColumnData } from '@/types/components/table'
import { ProductCartItem, ServiceCartItem } from '@/types/models'
import { useRef, useState } from 'react'

type Props = {
    cartId: number
    pageNumber: number
}

const CartPage = (props: Props) => {
    const { cartId, pageNumber } = props

    const {
        cartItems,
        errorMsg,
        isLoading,
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
                isLoading={isLoading}
                errorMsg={errorMsg}
                columns={serviceColumns}
                rows={serviceRows}
                lastRowHighlight
            />
        )
    }

    const handleProductItemCreate = async (formPayload: FormPayload) => {
        const { productId, quantity, modificationIds } = formPayload
        const productIdParsed = parseInt(productId)
        const quantityParsed = parseInt(quantity)
        const modificationIdsArray = modificationIds.split(',').map((id) => parseInt(id))
        const modificationsAreValid = modificationIds.length === 0 || modificationIdsArray.every((id) => !isNaN(id))
        if (isNaN(productIdParsed) || isNaN(quantityParsed) || !modificationsAreValid) {
            console.log('Invalid input')
            return
        }

        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'product',
                quantity: quantityParsed,
                productVersionId: productIdParsed,
                variationIds: modificationIdsArray,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
    }

    const createProductCartItemForm = () => (
        <div>
            <h4>Create Product</h4>
            <DynamicForm
                inputs={{
                    productId: { label: 'Product Id', placeholder: 'Enter product id:', type: 'text' },
                    quantity: { label: 'Quantity', placeholder: 'Enter quantity:', type: 'number' },
                    modificationIds: { label: 'Modification Ids', placeholder: '1, 2, 3...', type: 'text' },
                }}
                onSubmit={handleProductItemCreate}
            >
                <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
        </div>
    )

    const handleServiceCartItemCreate = async (formPayload: FormPayload) => {
        const { serviceId } = formPayload
        const serviceIdParsed = parseInt(serviceId)
        if (isNaN(serviceIdParsed)) {
            console.log('Invalid input')
            return
        }
        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'service',
                quantity: 1,
                serviceVersionId: serviceIdParsed,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
    }

    const createServiceCartItemForm = () => (
        <div>
            <h4>Create Service</h4>
            <DynamicForm
                inputs={{
                    serviceId: { label: 'Service Id', placeholder: 'Enter service id:', type: 'text' }
                }}
                onSubmit={handleServiceCartItemCreate}
            >
                <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
        </div>
    )

    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'createProduct') return createProductCartItemForm()
        if (sideDrawerContentType === 'createService') return createServiceCartItemForm()
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
