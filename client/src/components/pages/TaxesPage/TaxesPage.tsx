'use client'

import Button from '@/components/shared/Button'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import { useTaxedItems, useTaxes } from '@/hooks/taxes.hook'
import React, { useRef, useState } from 'react'
import TaxForm, { TaxFormPayload } from '../../specialized/TaxForm/TaxForm'
import TaxApi from '@/api/tax.api'
import { Product, Service, Tax } from '@/types/models'
import Table from '@/components/shared/Table'

import styles from './TaxesPage.module.scss'

type Props = {
    pageNumber: number
}

const TaxesPage = ({ pageNumber }: Props) => {
    const { errorMsg, isLoading, taxes, setTaxes } = useTaxes(pageNumber)
    const sideDrawerRef = useRef<SideDrawerRef | null>(null)

    const [selectedTax, setSelectedTax] = useState<Tax | undefined>()
    const {
        errorMsg: itemsError,
        isLoading: itemsIsLoading,
        appliedProducts,
        appliedServices,
        setAppliedProducts,
        setAppliedServices,
    } = useTaxedItems(selectedTax)

    type ActionType = 'Create' | 'Edit'
    const [actionType, setActionType] = useState<ActionType>('Create')

    const handleTaxCreate = async ({
        isPercentage,
        name,
        rate,
        productIds,
        serviceIds,
    }: TaxFormPayload) => {
        const taxResponse = await TaxApi.createTax({ isPercentage, name, rate })
        const { result: tax } = taxResponse
        if (!tax) {
            console.log(taxResponse.error)
            return
        }
        setTaxes([...taxes, tax])
    }

    const handleTaxUpdate = async ({
        isPercentage,
        name,
        rate,
        productIds,
        serviceIds,
    }: TaxFormPayload) => {
        if (!selectedTax) return
        const id = selectedTax.id
        const taxResponse = await TaxApi.updateTax({
            id,
            isPercentage,
            name: name || selectedTax.name,
            rate: rate || selectedTax.rate
        })
        const { result: tax } = taxResponse
        if (!tax) {
            console.log(taxResponse.error)
            return
        }
        const newTaxes = [
            ...taxes.filter((tax) => tax.id !== id),
            tax
        ]
        setTaxes(newTaxes)
    }

    const handleSubmit = (formPayload: TaxFormPayload) => {
        if (actionType === 'Create') handleTaxCreate(formPayload)
        if (actionType === 'Edit') handleTaxUpdate(formPayload)
        sideDrawerRef.current?.close()
    }

    const handleTaxDelete = async (taxToDelete: Tax) => {
        const response = await TaxApi.deleteTax(taxToDelete.id)
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newTaxes = taxes.filter((tax) => tax.id !== taxToDelete.id)
        setTaxes(newTaxes)
    }

    const handleProductClick = async (product: Product) => {
        if (!appliedProducts || !selectedTax) return
        if (appliedProducts.some((selectedProduct) => selectedProduct.id === product.id)) {
            const response = await TaxApi.removeProductsFromTax(selectedTax.id, [product.id])
            if (!response.result) {
                console.log(response.error)
                return
            }
            const newSelectedProducts = appliedProducts.filter((selectedProduct) => selectedProduct.id !== product.id)
            setAppliedProducts(newSelectedProducts)
            return
        }
        console.log({
            selectedTax,
            product
        })
        const response = await TaxApi.addProductsToTax(selectedTax.id, [product.id])
        if (!response.result) {
            console.log(response.error)
            return
        }
        setAppliedProducts([...appliedProducts, product])
    }

    const handleServiceClick = async (service: Service) => {
        if (!appliedServices || !selectedTax) return
        if (appliedServices.some((selectedService) => selectedService.id === service.id)) {
            const response = await TaxApi.removeServicesFromTax(selectedTax.id, [service.id])
            if (!response.result) {
                console.log(response.error)
                return
            }
            const newSelectedServices = appliedServices.filter((selectedService) => selectedService.id !== service.id)
            setAppliedServices(newSelectedServices)
            return
        }
        const response = await TaxApi.addServicesToTax(selectedTax.id, [service.id])
        if (!response.result) {
            console.log(response.error)
            return
        }
        setAppliedServices([...appliedServices, service])
    }

    const taxTable = () => {
        const columns = [
            { name: 'Name', key: 'name' },
            { name: 'Percentage', key: 'isPercentage' },
            { name: 'Rate', key: 'rate' }
        ]
        const rows = taxes.map((tax) => ({
            id: tax.id,
            name: tax.name,
            isPercentage: tax.isPercentage ? 'Yes' : 'No',
            rate: `${tax.rate / 100}${tax.isPercentage ? ' %' : ' €'}`,
            className: selectedTax?.id === tax.id ? styles.selected : '',
            onClick: (row: any) => {
                if (selectedTax?.id === row.id) setSelectedTax(undefined)
                else setSelectedTax(taxes.find((tax) => tax.id === row.id))
            }
        }))
        return (
            <Table
                columns={columns}
                rows={rows}
                isLoading={isLoading}
                errorMsg={errorMsg}
            />
        )
    }

    return (
        <div className={styles.page}>
            <div className={styles.header}>
                <h1>Taxes</h1>
                <div className={styles.toolbar}>
                    <Button onClick={() => {
                        setActionType('Create')
                        setSelectedTax(undefined)
                        setAppliedProducts([])
                        setAppliedServices([])
                        sideDrawerRef.current?.open()
                    }}>
                        Create New Tax
                    </Button>
                    <Button onClick={() => {
                        if (!selectedTax) return
                        setActionType('Edit')
                        sideDrawerRef.current?.open()
                    }}>
                        Edit Tax
                    </Button>
                    <Button
                        onClick={() => {
                            if (!selectedTax) return
                            handleTaxDelete(selectedTax)
                        }}
                    >
                        Delete Tax
                    </Button>
                </div>
            </div>
            <div>
                {taxTable()}
            </div>
            <SideDrawer ref={sideDrawerRef}>
                <TaxForm
                    showAppliedItems={selectedTax !== undefined}
                    actionName={actionType}
                    onSubmit={handleSubmit}
                    selectedProducts={appliedProducts}
                    onProductClick={handleProductClick}
                    selectedServices={appliedServices}
                    onServiceClick={handleServiceClick}
                />
            </SideDrawer>
        </div>
    )
}

export default TaxesPage