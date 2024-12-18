import React, { CSSProperties, useState } from 'react'
import Button from '@/components/shared/Button'
import PageChanger from '@/components/shared/PageChanger'
import ProductsView from '@/components/specialized/ProductsView'
import ServicesView from '@/components/specialized/ServicesView'
import { useProducts } from '@/hooks/products.hook'
import { useServices } from '@/hooks/services.hook'
import { Product, ProductModification, Service } from '@/types/models'

import styles from './AllItemView.module.scss'

export type SelectedItems = {
    products: Product[],
    services: Service[],
    productModifications: ProductModification[]
}

type Props = {
    errorMsg?: string
    isLoading?: boolean
    style?: CSSProperties
    className?: string
    headerText: string
    selectedProducts: Product[]
    onProductClick: (product: Product) => void
    selectedServices: Service[]
    onServiceClick: (service: Service) => void
}

const AllItemView = (props: Props) => {
    const {
        errorMsg,
        isLoading,
        className,
        style,
        headerText,
        selectedProducts,
        onProductClick,
        selectedServices,
        onServiceClick
    } = props
    const [itemType, setItemType] = useState<'product' | 'service'>('product')
    const pretifiedItemTypeText = {
        product: 'Products',
        service: 'Services',
        productModification: 'Product Modifications'
    } as const

    const nextItemType = () => {
        if (itemType === 'product') setItemType('service')
        if (itemType === 'service') setItemType('product')
    }
    const [productPageNumber, setProductPageNumber] = useState<number>(0)
    const { isError: isProductsError, isLoading: isProductsLoading, products } = useProducts(productPageNumber)

    const [servicePageNumber, setServicePageNumber] = useState<number>(0)
    const { errorMsg: serviceErrorMsg, isLoading: isServiesLoading, services } = useServices(servicePageNumber)

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
    }

    const view = () => {
        if (errorMsg) return <div>{errorMsg}</div>
        if (isLoading) return <div>Loading...</div>
        return itemView()
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
                className={className || styles.item_view_container}
                style={style}
            >
                {view()}
                <PageChanger
                    onClickNext={() => {
                        if (itemType === 'product') setProductPageNumber(productPageNumber + 1)
                        else if (itemType === 'service') setServicePageNumber(servicePageNumber + 1)
                    }}
                    onClickPrevious={() => {
                        if (itemType === 'product') setProductPageNumber(productPageNumber - 1)
                        else if (itemType === 'service') setServicePageNumber(servicePageNumber - 1)
                    }}
                    pageNumber={
                        itemType === 'product'
                            ?
                            productPageNumber
                            :
                            servicePageNumber
                    }
                />
            </div>
        </div>
    )
}

export default AllItemView