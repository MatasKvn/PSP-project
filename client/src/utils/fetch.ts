import { defaultHeaders } from "@/constants/api";
import { FetchParams, FetchResponse } from "@/types/fetch"
import { getCookie } from 'cookies-next/client'

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
                // console.error('Error parsing JSON response:', e);
                return {
                    result: response.statusText
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
    const token = getCookie('jwtToken')
    return {
        ...defaultHeaders,
        Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJUcmFuc2FjdGlvbldyaXRlIjoiWSIsIlRyYW5zYWN0aW9uUmVhZCI6IlkiLCJIaXN0b3JpY1RyYW5zYWN0aW9uV3JpdGUiOiJZIiwiSGlzdG9yaWNUcmFuc2FjdGlvblJlYWQiOiJZIiwiU2VydmljZVdyaXRlIjoiWSIsIlNlcnZpY2VSZWFkIjoiWSIsIkl0ZW1Xcml0ZSI6IlkiLCJJdGVtUmVhZCI6IlkiLCJFbXBsb3llZXNXcml0ZSI6IlkiLCJFbXBsb3llZXNSZWFkIjoiWSIsIlRheFdyaXRlIjoiWSIsIlRheFJlYWQiOiJZIiwiSGlzdG9yaWNXcml0ZSI6IlkiLCJIaXN0b3JpY1JlYWQiOiJZIiwiR2lmdENhcmRSZWFkIjoiWSIsIkdpZnRDYXJkV3JpdGUiOiJZIiwiQ2FydFJlYWQiOiJZIiwiQ2FydFdyaXRlIjoiWSIsIkNhcnRJdGVtUmVhZCI6IlkiLCJDYXJ0SXRlbVdyaXRlIjoiWSIsIkl0ZW1EaXNjb3VudFJlYWQiOiJZIiwiSXRlbURpc2NvdW50V3JpdGUiOiJZIiwiQnVzaW5lc3NEZXRhaWxzUmVhZCI6IlkiLCJCdXNpbmVzc0RldGFpbHNXcml0ZSI6IlkiLCJleHAiOjE3MzQ1NDE4MzYsImlzcyI6Imh0dHBzOi8vYXV0aC5QT1MtU3lzdGVtLmNvbSIsImF1ZCI6Imh0dHBzOi8vYnVzaW5lc3MuY29tIn0.F4dIlkMX3Ov74FI7nbGmtJV5pyKJAHV9J0TawLJEUCw`
    }
}

export const encodeDateToUrlString = (date: Date) => encodeURIComponent(date.toLocaleString('lt'))

export const sanitizeData = (data: any) => {
    return Object.fromEntries(
        Object.entries(data).map(([key, value]) => [key, value === undefined ? null : value])
    )
}