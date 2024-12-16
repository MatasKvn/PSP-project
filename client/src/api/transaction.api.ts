import { Transaction, TransactionStatusEnum } from './../types/models'
import { FetchResponse, PagedResponse } from '@/types/fetch'

let transactions: Transaction[] = [
    {
        id: 1,
        cartId: 1,
        amount: 19.99,
        tip: undefined,
        status: TransactionStatusEnum.SUCEEDED
    },
    {
        id: 2,
        cartId: 1,
        amount: 15.99,
        tip: 1.99,
        status: TransactionStatusEnum.SUCEEDED
    },
    {
        id: 3,
        cartId: 1,
        amount: 1.99,
        tip: 1.99,
        status: TransactionStatusEnum.PENDING
    },
    {
        id: 4,
        cartId: 2,
        amount: 1000.99,
        tip: 1.99,
        status: TransactionStatusEnum.SUCEEDED
    }
]

export default class TransactionApi {
    static async getCartTransactionsItems(cartId: number): Promise<FetchResponse<Transaction[]>> {
        const filteredTransactions = transactions.filter(transaction => transaction.cartId == cartId)

        return Promise.resolve({
            result: filteredTransactions
        })
    }
}