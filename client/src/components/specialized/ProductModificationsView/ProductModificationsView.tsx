import ProductModificationCard from '@/components/specialized/ProductModificationCard/ProductModificationCard'
import { ProductModification } from '@/types/models'
import React, { useState } from 'react'

type Props = {
    productModifications: ProductModification[]
    selectedProductModifications: ProductModification[]
    isLoading: boolean
    isError: boolean
    className?: string
    style?: React.CSSProperties
    onClick?: (selectedProductModifications: ProductModification) => void
}

const ProductModificationsView = (props: Props) => {
    const { 
        className, 
        style,
        onClick,
        productModifications,
        selectedProductModifications,
        isLoading,
        isError 
    } = props

    const view = () => {
        if (isLoading) return <div>Loading...</div>
        if (isError) return <div>Error</div>
        return (
            productModifications.map((productModification) => (
                <div key={productModification.id} style={{ margin: '1em', display: 'inline-block' }}>
                    <ProductModificationCard
                        isSelected={selectedProductModifications.some((pm) => pm === productModification)}
                        productModification={productModification}
                        onClick={() => {
                            onClick?.(productModification)
                        }}
                    />
                </div>
            ))
        )
    }

    return (
        <div
            className={className}
            style={style}
        >
            {view()}
        </div>
    )
}

export default ProductModificationsView