'use client'

import Button from '@/components/shared/Button'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import { useTaxes } from '@/hooks/taxes.hook'
import React, { useRef, useState } from 'react'
import TaxForm, { TaxFormPayload } from '../../specialized/TaxForm/TaxForm'
import TaxApi from '@/api/tax.api'
import { Tax } from '@/types/models'
import Table from '@/components/shared/Table'

import styles from './TaxesPage.module.scss'

type Props = {
    pageNumber: number
}

const TaxesPage = ({ pageNumber }: Props) => {
    const { errorMsg, isLoading, taxes, setTaxes } = useTaxes(pageNumber)
    const sideDrawerRef = useRef<SideDrawerRef | null>(null)

    const [selectedTax, setSelectedTax] = useState<Tax | undefined>()

    type ActionType = 'Create' | 'Edit'
    const [actionType, setActionType] = useState<ActionType>('Create')

    const handleTaxCreate = async ({
        isPercentage,
        name,
        rate,
        productIds,
        productModificationIds,
        serviceIds,
    }: TaxFormPayload) => {
        const taxResponse = await TaxApi.createTax({ isPercentage, name, rate})
        const { result: tax } = taxResponse
        if (!tax) {
            console.log(taxResponse.error)
            return
        }
        const responses = await Promise.all([
            await TaxApi.addProductsToTax(tax.id, productIds),
            await TaxApi.addProductModificationsToTax(tax.id, productModificationIds),
            await TaxApi.addServicesToTax(tax.id, serviceIds),
        ])
        responses.forEach((response) => {
            if (!response.result) console.log(response.error)
        })
        setTaxes([...taxes, tax])
    }

    const handleTaxUpdate = async ({
        isPercentage,
        name,
        rate,
        productIds,
        productModificationIds,
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
        const responses = await Promise.all([
            await TaxApi.addProductsToTax(tax.id, productIds),
            await TaxApi.addProductModificationsToTax(tax.id, productModificationIds),
            await TaxApi.addServicesToTax(tax.id, serviceIds),
        ])
        responses.forEach((response) => {
            if (!response.result) console.log(response.error)
        })
        setTaxes([...taxes, tax])
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

    const taxTable = () => {
        const columns = [
            { name: 'Name', key: 'name' },
            { name: 'Percentage', key: 'isPercentage' },
            { name: 'Rate', key: 'rate' },
            { name: 'Select', key: 'select' },
            { name: 'Delete', key: 'delete' }
        ]
        const rows = taxes.map((tax) => ({
            name: tax.name,
            isPercentage: tax.isPercentage ? 'Yes' : 'No',
            rate: tax.rate,
            select: (
                <Button
                    style={selectedTax?.id === tax.id ? { backgroundColor: 'green' } : {} }
                    onClick={() => {
                        if (selectedTax?.id === tax.id) {
                            setSelectedTax(undefined)
                            return
                        }
                        setSelectedTax(tax)
                    }}
                >
                    Select
                </Button>
            ),
            delete: (
                <Button
                    onClick={() => handleTaxDelete(tax)}
                >
                    Delete
                </Button>
            )
        }))
        return (
            <Table
                columns={columns}
                rows={rows}
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
                        sideDrawerRef.current?.open()
                    }}>
                        Create New Tax
                    </Button>
                    <Button onClick={() => {
                        setActionType('Edit')
                        sideDrawerRef.current?.open()
                    }}>
                        Edit Tax
                    </Button>
                </div>
            </div>
            <div>
                {taxTable()}
            </div>
            <SideDrawer ref={sideDrawerRef}>
                <TaxForm
                    actionName={actionType}
                    onSubmit={handleSubmit}
                />
            </SideDrawer>
        </div>
    )
}

export default TaxesPage