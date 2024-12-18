import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { fetch, getAuthorizedHeaders, sanitizeData } from '@/utils/fetch'
import { HTTPMethod, FetchResponse, PagedResponse } from '@/types/fetch'
import { GiftCardDetails, GiftCardsDetailFull } from '../types/payment'

export default class GiftCardApi {
    static async getGiftCards(pageNum: number): Promise<FetchResponse<PagedResponse<GiftCardsDetailFull>>> {
        return await fetch({
            url: `${apiBaseUrl}/giftcards?pageNum=${pageNum}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders()
        })
    }

    static async createGiftCard(dto: GiftCardRequest): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/giftcards`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(dto)
        })
    }

    static async deleteGiftCard(id: number): Promise<FetchResponse<any>> {
        return await fetch({
            url: `${apiBaseUrl}/giftcards/${id}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders()
        })
    }
}

type GiftCardRequest = Omit<GiftCardsDetailFull, 'id'>