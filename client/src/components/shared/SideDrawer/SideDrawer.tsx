import React, { forwardRef, Ref, useImperativeHandle, useState } from 'react'

import styles from './SideDrawer.module.scss'

export type SideDrawerRef = {
    open: () => void
    close: () => void
    toggle: () => void
}

type Props = {
    children: React.ReactNode
}

function SideDrawer(props: Props, ref: Ref<SideDrawerRef>) {
    const { children } = props

    const [isOpen, setIsOpen] = useState<boolean>(false)

    useImperativeHandle(ref, () => ({
        open: () => {
            setIsOpen(true)
        },
        close: () => {
            setIsOpen(false)
        },
        toggle: () => {
            setIsOpen(!isOpen)
        }
    }))

    return (
        <div>
            <div className={[
                styles.background,
                isOpen ? styles.background_open : styles.background_closed
                ].join(' ')}
                onClick={() => setIsOpen(false)}
            />
            <div className={[
                styles.drawer,
                isOpen ? styles.drawer_open : styles.drawer_closed
            ].join(' ')}>
                {children}
            </div>
        </div>
    )
}

export default forwardRef(SideDrawer)
