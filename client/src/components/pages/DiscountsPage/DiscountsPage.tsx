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

type Props = {
    pageNumber: number
}

const DiscountsPage = ({ pageNumber }: Props) => {
    const { discounts, setDiscounts, isLoading, errorMsg } = useDiscounts(pageNumber)
    const [selectedDiscount, selectDiscount] = useState<ItemDiscount | undefined>(undefined)
    const router = useRouter()

    type DrawerContentType = 'create' | 'edit' | 'none'
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

    return (
        <div className={styles.page}>
            <h1>Item Discounts Page</h1>
            <div className={styles.toolbar}>
                <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Discount
                </Button>
                <Button
                    disabled={!selectedDiscount || isLoading || !!errorMsg}
                    onClick={() => {
                        if (!selectedDiscount) return
                        setActionType('edit')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Edit Discount
                </Button>
                <Button
                    disabled={!selectedDiscount || isLoading || !!errorMsg}
                    onClick={() => {}}
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
                {<div>xzd</div>}
            </SideDrawer>
        </div>
    )
}

export default DiscountsPage
