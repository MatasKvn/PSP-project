import Image from 'next/image'
import React from 'react'

import styles from './ItemCard.module.scss'

type PropsShared = {
    id: number
    imageUrl: string
    label: string
    description: string
    price: number
    isSelected: boolean
    onClick: (id: number) => void
}

type ProductSpecificProps = {
    type: 'product'
    stock: number
} & PropsShared

type ServiceSpecificProps = {
    type: 'service'
} & PropsShared

type Props = ProductSpecificProps | ServiceSpecificProps

const isProduct = (props: Props): props is ProductSpecificProps => props.type === 'product'

const ItemCard = (props: Props) => {
    const {
        id,
        imageUrl,
        label,
        description,
        price,
        isSelected,
        onClick
    } = props

    const priceDisplay = `${(price / 100).toFixed(2)} €`

    return (
        <button
            className={[styles.container, isSelected && styles.container_selected].join(' ')}
            onClick={() => onClick(id)}
        >
            <Image
                className={styles.image}
                alt='Image'
                src={imageUrl || '/image_icon.png'}
                width={120}
                height={120}
            />
            <h6>{label}</h6>
            <p className={styles.description}>{description}</p>
            <p>{`Price: ${priceDisplay}`}</p>
            {isProduct(props) &&
                <p className={props.stock > 0 ? styles.stock : styles.stock_empty}>{`In stock: ${props.stock}`}</p>
            }
        </button>
    )
}

export default ItemCard
