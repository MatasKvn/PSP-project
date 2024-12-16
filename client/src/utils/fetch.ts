import { FetchParams, FetchResponse } from "@/types/fetch"
import { defaultHeaders } from '@/constants/api'

async function fetchWrapper({ url, method, headers, body }: FetchParams): Promise<FetchResponse<any>> {
    try {
        const response = await fetch(url, {
            method,
            body,
            headers
        })
        if (response.ok) {
            try {
                return {
                    result: await response.json()
                }
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
            } catch (e) {
                return {
                    result: {}
                }
            }
        }
        return {
            error: response.statusText || 'Unknown error'
        }
    } catch (err) {
        console.log((err as Error).message)
        return { error: (err as Error).message }
    }
}
export { fetchWrapper as fetch }

export const getAuthorizedHeaders = () => {
    const cookie = document.cookie.split(';').find(x => x.startsWith('jwtToken='))
    const token = cookie?.slice('jwtToken='.length)
    return {
        ...defaultHeaders,
        Authorization: `Bearer ${token}`
    }
}

export const encodeDateToUrlString = (date: Date) => encodeURIComponent(date.toLocaleString('lt'))