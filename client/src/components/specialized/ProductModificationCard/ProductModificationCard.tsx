import { ProductModification } from '@/types/models'
import React from 'react'

import styles from './ProductModificationsCard.module.scss'

type Props = {
    productModification: ProductModification
    isSelected: boolean
    onClick: (productModification: ProductModification) => void
}

const ProductModificationCard = (props: Props) => {
    const {
        isSelected,
        productModification,
        onClick
    } = props
    const {
        description,
        name,
        price
    } = productModification
    return (
        <button
            className={[
                styles.modification,
                isSelected && styles.modification_selected
            ].join(' ')}
            onClick={() => onClick(productModification)}
        >
            <h6>{name}</h6>
            <p>{description}</p>
            <p>{price}</p>
        </button>
    )
}

export default ProductModificationCard