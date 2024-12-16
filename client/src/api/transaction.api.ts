import { apiBaseUrl, defaultHeaders } from '@/constants/api';
import { Transaction, TransactionStatusEnum } from './../types/models'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { fetch } from '@/utils/fetch';

// let transactions: Transaction[] = [
//     {
//         id: 1,
//         cartId: 1,
//         amount: 19.99,
//         tip: undefined,
//         status: TransactionStatusEnum.SUCEEDED
//     },
//     {
//         id: 2,
//         cartId: 1,
//         amount: 15.99,
//         tip: 1.99,
//         status: TransactionStatusEnum.SUCEEDED
//     },
//     {
//         id: 3,
//         cartId: 1,
//         amount: 1.99,
//         tip: 1.99,
//         status: TransactionStatusEnum.PENDING
//     },
//     {
//         id: 4,
//         cartId: 2,
//         amount: 1000.99,
//         tip: 1.99,
//         status: TransactionStatusEnum.SUCEEDED
//     }
// ]

export default class TransactionApi {
    static async getCartTransactionsItems(cartId: number): Promise<FetchResponse<Transaction[]>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/${cartId}`,
            method: HTTPMethod.GET,
            headers: defaultHeaders
        });
    }
}