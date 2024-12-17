'use client'

import Button from '@/components/shared/Button'
import PageChanger from '@/components/shared/PageChanger'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import { useDiscountedItems, useDiscounts } from '@/hooks/discounts.hook'
import { ItemDiscount, Product, Service } from '@/types/models'
import React, { useState } from 'react'

import styles from './DiscountsPage.module.scss'
import { useRouter } from 'next/navigation'
import { GetPageUrl } from '@/constants/route'
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
    const {
        selectedProducts,
        selectedServices,
        errorMsg: discountedItemsErrorMsg,
        isLoading: discountedItemsLoading,
        setSelectedProducts,
        setSelectedServices
    } = useDiscountedItems(selectedDiscount)
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
        endDate
    }: DiscountFormPayload) => {
        if (!selectedDiscount) return
        const response = await ItemDiscountApi.updateDiscount({
            id: selectedDiscount.id,
            isPercentage,
            value: isNaN(value) || value <= 0 ? selectedDiscount.value : value,
            description: description.length > 0 ? description : selectedDiscount.description,
            startDate: startDate || selectedDiscount.startDate,
            endDate: endDate || selectedDiscount.endDate
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
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
        endDate
    }: DiscountFormPayload) => {
        if (!description) {
            console.log('Please enter a description')
            return
        }
        if (!startDate || !endDate) {
            console.log('Please enter a valid start and end date')
            return
        }
        const response = await ItemDiscountApi.createDiscount({
            isPercentage,
            value,
            description,
            startDate,
            endDate
        })
        const { result: discount } = response
        if (!discount) {
            console.log(response.error)
            return
        }
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
        if (actionType === 'Edit') handleUpdate(discountFormPayload)
        if (actionType === 'Create') handleCreate(discountFormPayload)
    }

    const handleProductClick = async (product: Product) => {
        if (!selectedDiscount) return
        if (selectedProducts.some((selectedProduct) => selectedProduct.id === product.id)) {
            const response = await ItemDiscountApi.removeProductsFromDiscount(selectedDiscount.id, [product.id])
            if (!response.result) {
                console.log(response.error)
                return
            }
            const newSelectedProducts = selectedProducts.filter((selectedProduct) => selectedProduct.id !== product.id)
            setSelectedProducts(newSelectedProducts)
            return
        }
        const response =  await ItemDiscountApi.addProductsToDiscount(selectedDiscount.id, [product.id])
        if (!response.result) {
            console.log(response.error)
            return
        }
        setSelectedProducts([...selectedProducts, product])
    }

    const handleServiceClick = async (service: Service) => {
        if (!selectedDiscount) return
        if (selectedProducts.some((selectedProduct) => selectedProduct.id === service.id)) {
            const response = await ItemDiscountApi.removeServicesFromDiscount(selectedDiscount.id, [service.id])
            if (!response.result) {
                console.log(response.error)
                return
            }
            const newSelectedServices = selectedServices.filter((selectedService) => selectedService.id !== service.id)
            setSelectedServices(newSelectedServices)
            return
        }
        const response =  await ItemDiscountApi.addServicesToDiscount(selectedDiscount.id, [service.id])
        if (!response.result) {
            console.log(response.error)
            return
        }
        setSelectedServices([...selectedServices, service])
    }

    return (
        <div className={styles.page}>
            <h1>Item Discounts Page</h1>
            <div className={styles.toolbar}>
                <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('Create')
                        selectDiscount(undefined)
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
                    showAppliedItems={selectedDiscount !== undefined}
                    actionName={actionType}
                    onSubmit={handleFormSubmit}
                    onProductClick={handleProductClick}
                    onServiceClick={handleServiceClick}
                    selectedProducts={selectedProducts}
                    selectedServices={selectedServices}
                />
            </SideDrawer>
        </div>
    )
}

export default DiscountsPage
