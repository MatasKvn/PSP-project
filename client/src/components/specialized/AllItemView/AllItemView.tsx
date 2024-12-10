import Button from '@/components/shared/Button'
import PageChanger from '@/components/shared/PageChanger'
import ProductModificationsView from '@/components/shared/ProductModificationsView/ProductModificationsView'
import ProductsView from '@/components/specialized/ProductsView'
import ServicesView from '@/components/specialized/ServicesView'
import { useProductModifications } from '@/hooks/productModifications.hook'
import { useProducts } from '@/hooks/products.hook'
import { useServices } from '@/hooks/services.hook'
import { Product, ProductModification, Service } from '@/types/models'
import React, { useState } from 'react'

import styles from './AllItemView.module.scss'

export type SelectedItems = {
    products: Product[],
    services: Service[],
    productModifications: ProductModification[]
}

type Props = {
    headerText: string
    selectedProducts: Product[]
    onProductClick: (product: Product) => void
    selectedServices: Service[]
    onServiceClick: (service: Service) => void
    selectedPms: ProductModification[]
    onProductModificationClick: (productModification: ProductModification) => void
}

const AllItemView = (props: Props) => {
    const {
        headerText,
        selectedProducts,
        onProductClick,
        selectedServices,
        onServiceClick,
        selectedPms,
        onProductModificationClick
    } = props
    const [itemType, setItemType] = useState<'product' | 'service' | 'productModification'>('product')
    const pretifiedItemTypeText = {
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

    const [servicePageNumber, setServicePageNumber] = useState<number>(0)
    const { errorMsg: serviceErrorMsg, isLoading: isServiesLoading, services } = useServices(servicePageNumber)

    const [pmPageNumber, setPmPageNumber] = useState<number>(0)
    const { productModifications, isLoading: isProductModificationsLoading, isError: isProductModificationsError } = useProductModifications(undefined, pmPageNumber)

    const itemView = () => {
        if (itemType === 'product') return (
            <ProductsView
                isError={isProductsError}
                isLoading={isProductsLoading}
                pageNumber={productPageNumber}
                products={products}
                selectedProducts={selectedProducts}
                onClick={(product: Product) => onProductClick(product)}
            />
        )
        if (itemType === 'service') return (
            <ServicesView
                isError={!!serviceErrorMsg}
                isLoading={isServiesLoading}
                pageNumber={servicePageNumber}
                services={services}
                selectedServices={selectedServices}
                onClick={(service: Service) => onServiceClick(service)}
            />
        )
        return (
            <ProductModificationsView
                isError={isProductModificationsError}
                isLoading={isProductModificationsLoading}
                productModifications={productModifications}
                selectedProductModifications={selectedPms}
                onClick={(productModification: ProductModification) => onProductModificationClick(productModification)}
            />
        )
    }

    return (
        <div>
            <div className={styles.header}>
                <h5>{headerText}</h5>
                <Button onClick={() => nextItemType()}>
                    {pretifiedItemTypeText[itemType]}
                </Button>
            </div>
            <div
                className={styles.item_view_container}
            >
                {itemView()}
                <PageChanger
                    onClickNext={() => {
                        if (itemType === 'product') setProductPageNumber(productPageNumber + 1)
                        else if (itemType === 'service') setServicePageNumber(servicePageNumber + 1)
                        else setPmPageNumber(pmPageNumber + 1)
                    }}
                    onClickPrevious={() => {
                        if (itemType === 'product') setProductPageNumber(productPageNumber - 1)
                        else if (itemType === 'service') setServicePageNumber(servicePageNumber - 1)
                        else setPmPageNumber(pmPageNumber - 1)
                    }}
                    pageNumber={
                        itemType === 'product'
                            ?
                            productPageNumber
                            :
                            (itemType === 'service' ? servicePageNumber : pmPageNumber)
                    }
                />
            </div>
        </div>
    )
}

export default AllItemView