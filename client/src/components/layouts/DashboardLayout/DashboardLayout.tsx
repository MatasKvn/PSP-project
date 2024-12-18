
import Navbar from '@/components/specialized/Navbar'

import styles from './DashboardLayout.module.scss'

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
    return (
        <div className={styles.app}>
            <div className={styles.navbar_wrapper}>
                <Navbar />
            </div>
            <main className={styles.main}>
                {children}
            </main>
        </div>
    )
}

export default DashboardLayout