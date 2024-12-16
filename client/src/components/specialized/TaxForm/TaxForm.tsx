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
    selectedTax?: Tax // TODO: get already applied items for this tax, and preselect them
    actionName: string
    onSubmit: (data: TaxFormPayload) => void
}

const TaxForm = ({ selectedTax, actionName, onSubmit }: Props) => {
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])
    const [selectedServices, setSelectedServices] = useState<Service[]>([])

    useEffect(() => {
        if (!selectedTax || actionName === 'Create') return
        const getSelectedTaxItems = async () => {
            const productResponse = await Promise.all([
                TaxApi.getProductsByTaxId(selectedTax.id),
                TaxApi.getServicesByTaxId(selectedTax.id)
            ])
            const { result: products, error: productsErr } = productResponse[0]
            const { result: services, error: servicesErr } = productResponse[1]
            if (!products) {
                console.log(productsErr)
                return
            }
            if (!services) {
                console.log(servicesErr)
                return
            }
            setSelectedProducts(products)
            setSelectedServices(services)
        }
        getSelectedTaxItems()

    }, [selectedTax, actionName])

    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { name, rate, isPercentage } = formPayload
        const parsedRate = parseFloat(rate)
        onSubmit({
            name,
            rate: isNaN(parsedRate) ? 0 : parsedRate,
            isPercentage: !!isPercentage,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id),
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