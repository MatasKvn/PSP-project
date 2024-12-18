'use client'

import { usePathname, useRouter } from 'next/navigation'
import styles from './Navbar.module.scss'
import { navItems } from '@/constants/navbar'
import Button from '@/components/shared/Button'
import { GetPageUrl } from '@/constants/route'
import { removeEmployeeId } from '@/utils/employeeId'
import { deleteCookie } from 'cookies-next'

const Navbar = () => {
    const location = usePathname()
    const locationNoNumbers = location.split('/').filter((item) => Number.isNaN(parseInt(item))).join('/')
    const router = useRouter()

    const handleLogOut = () => {
        deleteCookie('jwtToken', { secure: true, sameSite: 'strict' })
        removeEmployeeId()
        router.push(GetPageUrl.login)
    }

    return (
        <nav className={styles.navbar}>
            <h4 className={styles.label}>POS-System</h4>
            <ul>
                {navItems.map((navItem) => (
                    <a key={navItem.name} href={navItem.href}>
                        <li className={locationNoNumbers === navItem.url ? styles.nav_item_active : styles.nav_item}>
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