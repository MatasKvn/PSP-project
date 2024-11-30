import React from 'react'

import styles from './Button.module.scss'

type Props = React.ButtonHTMLAttributes<HTMLButtonElement> & {
    children: React.ReactNode
    className?: string
}

const Button = (props: Props) => {
    const {
        children,
        className,
        ...rest
    } = props

    return (
        <button className={[styles.button, className].join(' ')} {...rest}>{children}</button>
    )
}

export default Button
