import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { BusinessDetails } from '@/types/models'
import { fetch } from '@/utils/fetch'
import { HTTPMethod, FetchResponse } from '@/types/fetch'

const sanitizeData = (data: any) => {
    return Object.fromEntries(
        Object.entries(data).map(([key, value]) => [key, value === undefined ? null : value])
    )
}

export default class BusinessDetailsApi {
    static async getBusinessDetails(): Promise<FetchResponse<BusinessDetails>> {
        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        })
    }

    static async createBusinessDetails(businessDetails: BusinessDetails): Promise<FetchResponse<BusinessDetails>> {
        const sanitizedBusinessDetails = sanitizeData(businessDetails)

        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(sanitizedBusinessDetails)
        })
    }

    static async updateBusinessDetails(businessDetails: BusinessDetails): Promise<FetchResponse<BusinessDetails>> {
        const sanitizedBusinessDetails = sanitizeData(businessDetails)

        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.PUT,
            headers: defaultHeaders,
            body: JSON.stringify(businessDetails)
        })
    }
}