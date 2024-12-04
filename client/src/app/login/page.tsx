import LoginPage from '@/components/pages/LoginPage'
import { CookiesProvider } from 'next-client-cookies/server'

const Page = () => {

    return (
        <CookiesProvider>
            <LoginPage />
        </CookiesProvider>
    )
}

export default Page