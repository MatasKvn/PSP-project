import { FetchParams, FetchResponse } from "@/types/fetch"

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
