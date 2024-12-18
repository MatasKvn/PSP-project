import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import { Product, ProductModification, Service, Tax } from '@/types/models'
import React, { useEffect, useState } from 'react'
import AllItemView from '../AllItemView'
import TaxApi from '@/api/tax.api'
import ProductApi from '@/api/product.api'

export type TaxFormPayload = {
    name: string,
    rate: number,
    isPercentage: boolean,
    productIds: number[],
    serviceIds: number[],
}

type Props = {
    showAppliedItems?: boolean
    selectedProducts: Product[],
    onProductClick: (product: Product) => void
    onServiceClick: (service: Service) => void
    selectedServices: Service[],
    actionName: string
    onSubmit: (data: TaxFormPayload) => void
}

const TaxForm = ({ showAppliedItems = false, selectedProducts, onProductClick, selectedServices, onServiceClick, actionName, onSubmit }: Props) => {
    const returnValues = (formPayload: FormPayload) => {
        const { name, rate, isPercentage } = formPayload
        const parsedRate = parseFloat(rate)
        onSubmit({
            name,
            rate: isNaN(parsedRate) ? 0 : parsedRate,
            isPercentage: !!isPercentage,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id),
        })
    }

    return (
        <div>
            <h4>{actionName} Tax</h4>
            {showAppliedItems && <AllItemView
                headerText='Select Items'
                selectedProducts={selectedProducts}
                onProductClick={onProductClick}
                selectedServices={selectedServices}
                onServiceClick={onServiceClick}
            />}
            <DynamicForm
                inputs={{
                    name: { label: 'Name', placeholder: 'Enter name:', type: 'text' },
                    rate: { label: 'Rate', placeholder: 'Enter rate:', type: 'number' },
                    isPercentage: { label: 'Percentage?', type: 'checkbox' }
                }}
                onSubmit={returnValues}
            >
                <DynamicForm.Button>{actionName}</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default TaxForm