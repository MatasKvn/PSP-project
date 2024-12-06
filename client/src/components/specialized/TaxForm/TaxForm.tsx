import Button from '@/components/shared/Button'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import ProductModificationsView from '@/components/shared/ProductModificationsView/ProductModificationsView'
import ProductsView from '@/components/specialized/ProductsView'
import ServicesView from '@/components/specialized/ServicesView'
import { useProductModifications } from '@/hooks/productModifications.hook'
import { useProducts } from '@/hooks/products.hook'
import { useServices } from '@/hooks/services.hook'
import { Product, ProductModification, Service } from '@/types/models'
import React, { useState } from 'react'

export type TaxFormPayload = {
    name: string,
    rate: number,
    isPercentage: boolean,
    productIds: number[],
    serviceIds: number[],
}

type Props = {
    actionName: string
    onSubmit: (data: TaxFormPayload) => void
}

const TaxForm = ({ actionName, onSubmit }: Props) => {
    const [productPageNumber, setProductPageNumber] = useState<number | undefined>(0)
    const { isError: isProductsError, isLoading: isProductsLoading, products } = useProducts(productPageNumber)
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])

    const [servicePageNumber, setServicePageNumber] = useState<number | undefined>(undefined)
    const { errorMsg: serviceErrorMsg, isLoading: isServiesLoading, services } = useServices(servicePageNumber)
    const [selectedServices, setSelectedServices] = useState<Service[]>([])

    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { name, rate, isPercentage } = formPayload
        const parsedRate = parseFloat(rate)
        if (isNaN(parsedRate)) return
        onSubmit({
            name,
            rate: parsedRate,
            isPercentage: !!isPercentage,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id)
        })
        setSelectedProducts([])
        setSelectedServices([])
    }

    const itemView = () => {
        if (productPageNumber !== undefined) return (
            <div>
                <ProductsView
                    style={{ height: '40vh', overflowY: 'scroll'}}
                    isError={isProductsError}
                    isLoading={isProductsLoading}
                    pageNumber={productPageNumber}
                    products={products}
                    selectedProducts={selectedProducts}
                    onClick={(product: Product) => {
                        if (selectedProducts.some((selectedProduct) => selectedProduct.id === product.id)) {
                            const newSelectedProducts = selectedProducts.filter((selectedProduct) => selectedProduct.id !== product.id)
                            setSelectedProducts(newSelectedProducts)
                            console.log('a')
                            return
                        }
                        console.log(selectedProducts)
                        setSelectedProducts([...selectedProducts, product])
                    }}
                />
            </div>
        )
        if (servicePageNumber !== undefined) return (
            <ServicesView
                style={{ height: '40vh', overflowY: 'scroll'}}
                isError={!!serviceErrorMsg}
                isLoading={isServiesLoading}
                pageNumber={servicePageNumber}
                services={services}
                selectedServices={selectedServices}
                onClick={(service: Service) => {
                    if (selectedServices.some((selectedService) => selectedService.id === service.id)) {
                        const newSelectedServices = selectedServices.filter((selectedService) => selectedService.id !== service.id)
                        setSelectedServices(newSelectedServices)
                        return
                    }
                    setSelectedServices([...selectedServices, service])
                }}
            />
        )
        throw new Error('Should never get here.')
    }

    return (
        <div>
            <Button onClick={() => {
                if (productPageNumber === undefined) {
                    setProductPageNumber(0)
                    setServicePageNumber(undefined)
                    return
                }
                setServicePageNumber(0)
                setProductPageNumber(undefined)
            }}>
                {productPageNumber === undefined ? `Products` : 'Services'}
            </Button>
            {itemView()}
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