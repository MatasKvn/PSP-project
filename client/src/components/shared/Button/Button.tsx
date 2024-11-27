import React from 'react'

import styles from './Button.module.scss'

type Props = {
    children: React.ReactNode
} & React.ButtonHTMLAttributes<HTMLButtonElement>

const Button = (props: Props) => {
    const {
        children,
        ...rest
    } = props

    return (
        <button className={styles.button} {...rest}>{children}</button>
    )
}

export default Button
