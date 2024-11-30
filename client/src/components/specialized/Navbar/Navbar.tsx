'use client'

import { usePathname } from 'next/navigation'
import styles from './Navbar.module.scss'
import { navItems } from '@/constants/navbar'

const Navbar = () => {
    const location = usePathname()

    return (
        <nav className={styles.navbar}>
            <h4 className={styles.label}>POS-System</h4>
            <ul>
                {navItems.map((navItem) => (
                    <a key={navItem.name} href={navItem.href}>
                        <li className={navItem.href === location ? styles.nav_item_active : styles.nav_item}>
                                {navItem.name}
                        </li>
                    </a>
                ))}
            </ul>
        </nav>
    )
}

export default Navbar