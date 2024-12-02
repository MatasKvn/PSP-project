
import Navbar from '@/components/specialized/Navbar'

import styles from './DashboardLayout.module.scss'
import { CookiesProvider } from 'next-client-cookies/server'

const NavbarWithCookieProvider = () => (
    <CookiesProvider>
        <Navbar />
    </CookiesProvider>
)

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <div className={styles.app}>
            <div className={styles.navbar_wrapper}>
                <NavbarWithCookieProvider />
            </div>
            <main className={styles.main}>
                {children}
            </main>
        </div>
    )
}

export default DashboardLayout