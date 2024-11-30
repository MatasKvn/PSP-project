import React from 'react'

import styles from './Input.module.scss'

type Props = React.InputHTMLAttributes<HTMLInputElement>


const Input = (props: Props) => {
    const { ...rest } = props

    return (
        <input className={styles.input} {...rest}/>
    )
}

export default Input