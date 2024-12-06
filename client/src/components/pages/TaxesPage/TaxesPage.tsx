'use client'

import Button from '@/components/shared/Button'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import { useTaxes } from '@/hooks/taxes.hook'
import React, { useRef, useState } from 'react'
import TaxForm, { TaxFormPayload } from '../../specialized/TaxForm/TaxForm'

type Props = {
    pageNumber: number
}

const TaxesPage = ({ pageNumber }: Props) => {
    const { errorMsg, isLoading, taxes, setTaxes } = useTaxes(pageNumber)
    const sideDrawerRef = useRef<SideDrawerRef | null>(null)

    type ActionType = 'Create' | 'Edit'
    const [actionType, setActionType] = useState<ActionType>('Create')

    const handleTaxCreate = async (formPayload: TaxFormPayload) => {
        console.log('create', formPayload)
    }

    const handleTaxUpdate = async (formPayload: TaxFormPayload) => {
        console.log('update', formPayload)
    }

    const handleSubmit = (formPayload: TaxFormPayload) => {
        if (actionType === 'Create') handleTaxCreate(formPayload)
        if (actionType === 'Edit') handleTaxUpdate(formPayload)
        sideDrawerRef.current?.close()
    }


    return (
        <div>
            <div>
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