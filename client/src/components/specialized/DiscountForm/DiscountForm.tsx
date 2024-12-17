import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import { Product, ProductModification, Service } from '@/types/models'
import React, { useState } from 'react'
import AllItemView from '../AllItemView'


export type DiscountFormPayload = {
    description: string,
    value: number,
    isPercentage: boolean,
    startDate: Date | undefined,
    endDate: Date | undefined
}

type Props = {
    showAppliedItems?: boolean
    actionName: string
    onSubmit: (data: DiscountFormPayload) => void
    selectedProducts: Product[]
    onProductClick: (product: Product) => void
    selectedServices: Service[]
    onServiceClick: (service: Service) => void
}

const DiscountForm = ({
    showAppliedItems = false,
    actionName,
    onSubmit,
    selectedProducts,
    onProductClick,
    selectedServices,
    onServiceClick
}: Props) => {
    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { description, value, isPercentage, startDate, endDate } = formPayload
        const parsedValue = parseFloat(value)
        const parsedStartDate = new Date(startDate)
        const parsedEndDate = new Date(endDate)
        onSubmit({
            description,
            value: isNaN(parsedValue) ? 0 : parsedValue,
            isPercentage: !!isPercentage,
            startDate: isNaN(parsedStartDate.getTime()) ? undefined : parsedStartDate,
            endDate: isNaN(parsedEndDate.getTime()) ? undefined : parsedEndDate
        })
    }

    return (
        <div>
            <h4>{actionName} Discount</h4>
            {showAppliedItems && <AllItemView
                style={{ height: '35vh' }}
                headerText='Select Items'
                selectedProducts={selectedProducts}
                onProductClick={(product: Product) => {
                    console.log('prod click')
                    onProductClick(product)
                }}
                selectedServices={selectedServices}
                onServiceClick={onServiceClick}
            />}
            <DynamicForm
                inputs={{
                    description: { label: 'Description', placeholder: 'Enter description:', type: 'text' },
                    value: { label: 'Value', placeholder: 'Enter value:', type: 'number' },
                    isPercentage: { label: 'Percentage?', type: 'checkbox' },
                    startDate: { label: 'Start Date', placeholder: 'Enter start date', type: 'date', note: 'Date needs to be in the future.' },
                    endDate: { label: 'End Date', placeholder: 'Enter end date', type: 'date', note: 'End date must be later than start date.' },
                }}
                onSubmit={handleTaxUpdate}
            >
                <DynamicForm.Button>{actionName}</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default DiscountForm
