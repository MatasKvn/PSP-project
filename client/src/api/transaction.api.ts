import { apiBaseUrl, defaultHeaders } from '@/constants/api';
import { Transaction, TransactionStatusEnum } from './../types/models'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { fetch } from '@/utils/fetch';

export default class TransactionApi {
    static async getCartTransactionsItems(cartId: number): Promise<FetchResponse<Transaction[]>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/${cartId}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        });
    }
}