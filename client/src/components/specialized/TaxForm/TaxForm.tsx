import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import { Product, ProductModification, Service, Tax } from '@/types/models'
import React, { useState } from 'react'
import AllItemView from '../AllItemView'

export type TaxFormPayload = {
    name: string,
    rate: number,
    isPercentage: boolean,
    productIds: number[],
    serviceIds: number[],
    productModificationIds: number[]
}

type Props = {
    selectedTax?: Tax // TODO: get already applied items for this tax, and preselect them
    actionName: string
    onSubmit: (data: TaxFormPayload) => void
}

const TaxForm = ({ selectedTax, actionName, onSubmit }: Props) => {
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])
    const [selectedServices, setSelectedServices] = useState<Service[]>([])
    const [selectedPms, setSeletedPms] = useState<ProductModification[]>([])

    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { name, rate, isPercentage } = formPayload
        const parsedRate = parseFloat(rate)
        onSubmit({
            name,
            rate: isNaN(parsedRate) ? 0 : parsedRate,
            isPercentage: !!isPercentage,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id),
            productModificationIds: selectedPms.map((pm) => pm.id),
        })
        setSelectedProducts([])
        setSelectedServices([])
    }

    return (
        <div>
            <h4>{actionName} Tax</h4>
            <AllItemView
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
                selectedPms={selectedPms}
                onProductModificationClick={(pm) => {
                    if (selectedPms.some((selectedPm) => selectedPm.id === pm.id)) {
                        const newSelectedPms = selectedPms.filter((selectedPm) => selectedPm.id !== pm.id)
                        setSeletedPms(newSelectedPms)
                        return
                    }
                    setSeletedPms([...selectedPms, pm])
                }}
            />
            <DynamicForm
                inputs={{
                    name: { label: 'Name', placeholder: 'Enter name:', type: 'text' },
                    rate: { label: 'Rate', placeholder: 'Enter rate:', type: 'number' },
                    isPercentage: { label: 'Percentage?', type: 'checkbox' }
                }}
                onSubmit={handleTaxUpdate}
            >
                <DynamicForm.Button>{actionName}</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default TaxForm