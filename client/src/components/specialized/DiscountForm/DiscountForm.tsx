import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import { Product, ProductModification, Service } from '@/types/models'
import React, { useState } from 'react'
import AllItemView from '../AllItemView'


export type DiscountFormPayload = {
    description: string,
    value: number,
    isPercentage: boolean,
    startDate: string,
    endDate: string,
    productIds: number[],
    serviceIds: number[]
}

type Props = {
    actionName: string
    onSubmit: (data: DiscountFormPayload) => void
}

const DiscountForm = ({
    actionName,
    onSubmit
}: Props) => {
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])
    const [selectedServices, setSelectedServices] = useState<Service[]>([])

    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { description, value, isPercentage, startDate, endDate } = formPayload
        const parsedValue = parseFloat(value)
        onSubmit({
            description,
            value: isNaN(parsedValue) ? 0 : parsedValue,
            isPercentage: !!isPercentage,
            startDate,
            endDate,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id),
        })
        setSelectedProducts([])
        setSelectedServices([])
    }

    return (
        <div>
            <h4>{actionName} Discount</h4>
            <AllItemView
                style={{ height: '35vh' }}
                headerText='Select Items'
                selectedProducts={selectedProducts}
                onProductClick={(product) => {
                    if (selectedProducts.some((selectedProduct) => selectedProduct.id === product.id)) {
                        const newSelectedProducts = selectedProducts.filter((selectedProduct) => selectedProduct.id !== product.id)
                        setSelectedProducts(newSelectedProducts)
                        return
                    }
                    setSelectedProducts([...selectedProducts, product])
                }}
                selectedServices={selectedServices}
                onServiceClick={(service) => {
                    if (selectedServices.some((selectedService) => selectedService.id === service.id)) {
                        const newSelectedServices = selectedServices.filter((selectedService) => selectedService.id !== service.id)
                        setSelectedServices(newSelectedServices)
                        return
                    }
                    setSelectedServices([...selectedServices, service])
                }}
            />
            <DynamicForm
                inputs={{
                    description: { label: 'Description', placeholder: 'Enter description:', type: 'text' },
                    value: { label: 'Value', placeholder: 'Enter value:', type: 'number' },
                    isPercentage: { label: 'Percentage?', type: 'checkbox' },
                    startDate: { label: 'Start Date', placeholder: 'Enter start date', type: 'date' },
                    endDate: { label: 'End Date', placeholder: 'Enter end date', type: 'date' },
                }}
                onSubmit={handleTaxUpdate}
            >
                <DynamicForm.Button>{actionName}</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default DiscountForm
