import { defaultHeaders } from '@/constants/api'

export const getAuthorizedHeaders = () => {
    const cookie = document.cookie.split(';').find(x => x.startsWith('jwtToken='))
    const token = cookie?.slice('jwtToken='.length)
    return {
        ...defaultHeaders,
        Authorization: `Bearer ${token}`
    }
}