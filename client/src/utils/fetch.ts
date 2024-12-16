import { FetchParams, FetchResponse } from "@/types/fetch"

async function fetchWrapper({ url, method, headers, body }: FetchParams): Promise<FetchResponse<any>> {
    try {

        console.log('Request URL:', url);
        console.log('Request Method:', method);
        console.log('Request Headers:', headers);
        console.log('Request Body:', body);  

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
                console.error('Error parsing JSON response:', e);
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
