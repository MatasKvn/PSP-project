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
    productModificationIds: number[]
}

type Props = {
    actionName: string
    onSubmit: (data: TaxFormPayload) => void
}

const TaxForm = ({ actionName, onSubmit }: Props) => {
    const [itemType, setItemType] = useState<'product' | 'service' | 'productModification'>('product')
    const pretifiedItemType = {
        product: 'Products',
        service: 'Services',
        productModification: 'Product Modifications'
    } as const

    const nextItemType = () => {
        if (itemType === 'product') setItemType('service')
        else if (itemType === 'service') setItemType('productModification')
        else setItemType('product')
    }
    const [productPageNumber, setProductPageNumber] = useState<number>(0)
    const { isError: isProductsError, isLoading: isProductsLoading, products } = useProducts(productPageNumber)
    const [selectedProducts, setSelectedProducts] = useState<Product[]>([])

    const [servicePageNumber, setServicePageNumber] = useState<number>(0)
    const { errorMsg: serviceErrorMsg, isLoading: isServiesLoading, services } = useServices(servicePageNumber)
    const [selectedServices, setSelectedServices] = useState<Service[]>([])

    const [pmPageNumber, setPmPageNumber] = useState<number>(0)
    const { productModifications, isLoading: isProductModificationsLoading, isError: isProductModificationsError } = useProductModifications(undefined, pmPageNumber)
    const [selectedPms, setSeletedPms] = useState<ProductModification[]>([])

    const handleTaxUpdate = (formPayload: FormPayload) => {
        const { name, rate, isPercentage } = formPayload
        const parsedRate = parseFloat(rate)
        if (isNaN(parsedRate)) return
        onSubmit({
            name,
            rate: parsedRate,
            isPercentage: !!isPercentage,
            productIds: selectedProducts.map((product) => product.id),
            serviceIds: selectedServices.map((service) => service.id),
            productModificationIds: selectedPms.map((pm) => pm.id),
        })
        setSelectedProducts([])
        setSelectedServices([])
    }

    const itemView = () => {
        if (itemType === 'product') return (
            <div>
                <ProductsView
                    style={{ height: '40vh', overflowY: 'scroll' }}
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
        if (itemType === 'service') return (
            <ServicesView
                style={{ height: '40vh', overflowY: 'scroll' }}
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
        if (itemType === 'productModification') return (
            <ProductModificationsView
                style={{ height: '40vh', overflowY: 'scroll' }}
                isError={isProductModificationsError}
                isLoading={isProductModificationsLoading}
                productModifications={productModifications}
                selectedProductModifications={selectedPms}
                onClick={(productModification: ProductModification) => {
                    if (selectedPms.some((selectedPm) => selectedPm.id === productModification.id)) {
                        const newSelectedPms = selectedPms.filter((selectedPm) => selectedPm.id !== productModification.id)
                        setSeletedPms(newSelectedPms)
                        return
                    }
                    setSeletedPms([...selectedPms, productModification])
                }}
            />
        )
        throw new Error('Should never get here.')
    }

    return (
        <div>
            <Button onClick={() => {
                nextItemType()
            }}>
                {pretifiedItemType[itemType]}
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