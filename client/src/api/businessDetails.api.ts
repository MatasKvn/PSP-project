import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { BusinessDetails } from '@/types/models'
import { fetch, getAuthorizedHeaders, sanitizeData } from '@/utils/fetch'
import { HTTPMethod, FetchResponse } from '@/types/fetch'


export default class BusinessDetailsApi {
    static async getBusinessDetails(): Promise<FetchResponse<BusinessDetails>> {
        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async createBusinessDetails(businessDetails: BusinessDetails): Promise<FetchResponse<BusinessDetails>> {
        const sanitizedBusinessDetails = sanitizeData(businessDetails)

        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(sanitizedBusinessDetails)
        })
    }

    static async updateBusinessDetails(businessDetails: BusinessDetails): Promise<FetchResponse<BusinessDetails>> {
        const sanitizedBusinessDetails = sanitizeData(businessDetails)

        return await fetch({
            url: `${apiBaseUrl}/business-details`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(businessDetails)
        })
    }
}