'use client'

import { usePathname, useRouter } from 'next/navigation'
import styles from './Navbar.module.scss'
import { navItems } from '@/constants/navbar'
import Button from '@/components/shared/Button'
import { routes } from '@/constants/route'
import { useCookies } from 'next-client-cookies'

const Navbar = () => {
    const location = usePathname()
    const router = useRouter()
    const cookies = useCookies()

    const handleLogOut = () => {
        cookies.remove('jwtToken', { secure: true })
        router.push(routes.login)
    }

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
            <div className={styles.logout_button_wrapper}>
                <Button onClick={handleLogOut}>Logout</Button>
            </div>
        </nav>
    )
}

export default Navbar