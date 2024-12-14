'use client'

import Button from '@/components/shared/Button'
import PageChanger from '@/components/shared/PageChanger'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import { useDiscounts } from '@/hooks/discounts.hook'
import { ItemDiscount } from '@/types/models'
import React, { useState } from 'react'

import styles from './DiscountsPage.module.scss'
import { useRouter } from 'next/navigation'
import { GetPageUrl } from '@/constants/route'
import ItemCard from '@/components/shared/ItemCard'
import DiscountCard from '@/components/shared/DiscountCard'
import DiscountForm from '@/components/specialized/DiscountForm'
import { DiscountFormPayload } from '@/components/specialized/DiscountForm/DiscountForm'
import ItemDiscountApi from '@/api/itemDiscount.api'

type Props = {
    pageNumber: number
}

const DiscountsPage = ({ pageNumber }: Props) => {
    const { discounts, setDiscounts, isLoading, errorMsg } = useDiscounts(pageNumber)
    const [selectedDiscount, selectDiscount] = useState<ItemDiscount | undefined>(undefined)
    const router = useRouter()

    type DrawerContentType = 'Create' | 'Edit' | 'none'
    const [actionType, setActionType] = React.useState<DrawerContentType>('none')
    const sideDrawerRef = React.useRef<SideDrawerRef | null>(null)

    const discountCards = () => {
        if (isLoading) return <div>Loading...</div>
        if (errorMsg) return <div>Error</div>
        if (discounts.length <= 0) return <div>No discounts</div>

        return discounts.map((discount) => (
            <div key={discount.id}>
                <DiscountCard
                    {...discount}
                    isSelected={discount.id === selectedDiscount?.id}
                    onClick={() => {
                        if (selectedDiscount?.id === discount.id) {
                            selectDiscount(undefined)
                            return
                        }
                        selectDiscount(discount)
                    }}
                />
            </div>
        ))
    }

    const handleUpdate = async ({
        isPercentage,
        description,
        value,
        startDate,
        endDate,
        productIds,
        serviceIds
    }: DiscountFormPayload) => {
        if (!selectedDiscount) return
        const startDateParsed = new Date(startDate)
        const endDateParsed = new Date(endDate)
        const response = await ItemDiscountApi.updateDiscount({
            id: selectedDiscount.id,
            isPercentage,
            value: isNaN(value) ? selectedDiscount.value : value,
            description: description.length > 0 ? description : selectedDiscount.description,
            startDate: isNaN(endDateParsed.getTime()) ? selectedDiscount.startDate : startDateParsed,
            endDate: isNaN(endDateParsed.getTime()) ? selectedDiscount.endDate : endDateParsed
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const responses = await Promise.all([
            await ItemDiscountApi.addProductsToDiscount(selectedDiscount.id, productIds),
            await ItemDiscountApi.addServicesToDiscount(selectedDiscount.id, serviceIds)
        ])
        responses.forEach((response) => {
            if (!response.result) console.log(response.error)
        })
        const newDiscounts = [
            ...discounts.filter((discount) => discount.id !== selectedDiscount!.id),
            response.result
        ]
        setDiscounts(newDiscounts)
    }

    const handleCreate = async ({
        isPercentage,
        description,
        value,
        startDate,
        endDate,
        productIds,
        serviceIds
    }: DiscountFormPayload) => {
        const startDateParsed = new Date(startDate)
        const endDateParsed = new Date(endDate)
        if (isNaN(startDateParsed.getTime()) && startDate !== '' || isNaN(endDateParsed.getTime()) && endDate !== '') {
            console.log('Incorrect Date format')
            return
        }
        if (!description) {
            console.log('Please enter a description')
            return
        }
        const response = await ItemDiscountApi.createDiscount({
            isPercentage,
            value,
            description,
            startDate: startDateParsed,
            endDate: endDateParsed
        })
        const { result: discount } = response
        if (!discount) {
            console.log(response.error)
            return
        }
        const responses = await Promise.all([
            await ItemDiscountApi.addProductsToDiscount(discount.id, productIds),
            await ItemDiscountApi.addServicesToDiscount(discount.id, serviceIds)
        ])
        responses.forEach((response) => {
            if (!response.result) console.log(response.error)
        })
        const newDiscounts = [
            ...discounts,
            discount
        ]
        setDiscounts(newDiscounts)
    }

    const handleDelete = async () => {
        if (!selectedDiscount) return
        const response = await ItemDiscountApi.deleteDiscount(selectedDiscount.id)
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newDiscounts = discounts.filter((discount) => discount.id !== selectedDiscount.id)
        setDiscounts(newDiscounts)
    }

    const handleFormSubmit = (discountFormPayload: DiscountFormPayload) => {
        console.log(discountFormPayload)
        if (actionType === 'Edit') handleUpdate(discountFormPayload)
        if (actionType === 'Create') handleCreate(discountFormPayload)
    }

    return (
        <div className={styles.page}>
            <h1>Item Discounts Page</h1>
            <div className={styles.toolbar}>
                <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('Create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Discount
                </Button>
                <Button
                    disabled={!selectedDiscount || isLoading || !!errorMsg}
                    onClick={() => {
                        if (!selectedDiscount) return
                        setActionType('Edit')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Edit Discount
                </Button>
                <Button
                    disabled={!selectedDiscount || isLoading || !!errorMsg}
                    onClick={() => handleDelete()}
                >
                    Delete Discount
                </Button>
            </div>
            <div className={styles.card_container}>
                {discountCards()}
            </div>
            <PageChanger
                onClickNext={() => router.push(GetPageUrl.discounts(parseInt(pageNumber as unknown as string) + 1))}
                onClickPrevious={() => router.push(GetPageUrl.discounts(pageNumber - 1))}
                disabledPrevious={pageNumber <= 0}
                pageNumber={pageNumber}
            />
            <SideDrawer ref={sideDrawerRef}>
                <DiscountForm
                    actionName={actionType}
                    onSubmit={handleFormSubmit}
                />
            </SideDrawer>
        </div>
    )
}

export default DiscountsPage
