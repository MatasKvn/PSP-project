import React from 'react'

import styles from './Input.module.scss'

type Props = React.InputHTMLAttributes<HTMLInputElement> & {
    className?: string
}


const Input = (props: Props) => {
    const { className, ...rest } = props

    return (
        <input className={[styles.input, className].join(' ')} {...rest}/>
    )
}

export default Input