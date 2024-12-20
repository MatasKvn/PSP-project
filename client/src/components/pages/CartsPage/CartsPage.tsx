'use client'

import React from 'react'

import CartApi from '@/api/cart.api'
import Button from '@/components/shared/Button'
import Table from '@/components/shared/Table'
import { useCarts } from '@/hooks/carts.hook'
import { Cart, CartStatusEnum, getCartStatusEnumString } from '@/types/models'
import { useRouter } from 'next/navigation'
import { GetPageUrl } from '@/constants/route'
import { getEmployeeId } from '@/utils/employeeId'

type Props = {
    pageNumber: number
}

const CartsPage = ({ pageNumber }: Props) => {
    const { carts, setCarts, isLoading, isError } = useCarts(pageNumber)
    const router = useRouter()

    const handleCartCreate = async () => {
        const response = await CartApi.createCart({ employeeVersionId: getEmployeeId() })
        if (response.error) {
            console.log('An error occured when creating cart: ', response.error)
            return
        }
        setCarts([...carts, response.result!])
    }

    const handleCartDelete = async (cart: Cart) => {
        if (cart.status !== CartStatusEnum.IN_PROGRESS) {
            console.log('Cannot delete a not in progress cart.')
            return
        }
        const response = await CartApi.deleteCart(cart.id)
        if (response.error) {
            console.log('An error occurred:', response.error)
            return
        }
        setCarts(carts.filter((c) => c.id != cart.id))
    }

    const dummyCartRow = {
        dateCreated: new Date(),
        employeeVersionId: 0,
        id: 0,
        status: CartStatusEnum.PENDING,
        Delete: '',
        Open: ''
    }
    const rows = carts.map((cart) => ({
        ...cart,
        status: getCartStatusEnumString(cart.status),
        Delete: <Button onClick={() => handleCartDelete(cart)}>Delete</Button>,
        Open: <Button onClick={() => router.push(GetPageUrl.cart(cart.id, 0))}>Open</Button>
    })) || []
    const columns = Object.keys(dummyCartRow).map((key) => ({ name: key, key }))

    return (
        <>
            <h1>Carts Page</h1>
            <div style={{ margin: '2em auto' }}>
                <Button onClick={() => handleCartCreate()}>
                    <h5>Create New Cart</h5>
                </Button>
            </div>
                <Table
                    columns={columns}
                    rows={rows}
                    isLoading={isLoading}
                    errorMsg={isError ? 'An error occurred' : ''}
                />
        </>
    )
}

export default CartsPage
