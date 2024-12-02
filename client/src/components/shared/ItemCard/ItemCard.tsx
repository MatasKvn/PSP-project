import Image from 'next/image'
import React from 'react'

import styles from './ItemCard.module.scss'

type Props = {
    id: number
    imageUrl: string
    label: string
    description: string
    price: number
    isSelected: boolean
    onClick: (id: number) => void
} & (ProductSpecificProps | ServiceSpecificProps)

type ProductSpecificProps = {
    type: 'product'
    stock: number
}

type ServiceSpecificProps = {
    type: 'service'
}

const ItemCard = (props: Props) => {
    const {
        id,
        imageUrl,
        label,
        description,
        price,
        stock,
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
                width={100}
                height={100}
            />
            <h6>{label}</h6>
            <p className={styles.description}>{description}</p>
            <p>{`Price: ${priceDisplay}`}</p>
            {stock &&
                <p className={stock > 0 ? styles.stock : styles.stock_empty}>{`In stock: ${stock}`}</p>
            }
        </button>
    )
}

export default ItemCard
