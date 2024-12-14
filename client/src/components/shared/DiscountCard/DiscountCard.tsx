import { ItemDiscount } from '@/types/models'
import React from 'react'

import styles from './DiscountCard.module.scss'
import Image from 'next/image'

type Props = {
    onClick: () => void
    isSelected: boolean
    description: string
    isPercentage: boolean
    value: number
}

const DiscountCard = ({
    isSelected,
    onClick,
    description,
    isPercentage,
    value
}: Props) => {

    return (
        <button
            className={[
                styles.container,
                isSelected && styles.container_selected
            ].join(' ')}
            onClick={() => onClick()}
        >
            <div className={styles.img_wrapper}>
                <Image
                    src="/discount.png"
                    height={100}
                    width={100}
                    alt="Discount icon"
                />
            </div>
            <p id='description'>{description}</p>
            <p id='price'>{`Value: ${(value / 100).toFixed(2)} ${isPercentage ? '%' : '€'}`}</p>
        </button>
    )
}

export default DiscountCard
