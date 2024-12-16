import { useState, useEffect } from 'react'
import { Transaction, TransactionStatusEnum } from '@/types/models'
import CartTransactionApi from '@/api/transaction.api'

export const useCartTransactions = (cartId: number) => {
    const [cartTransactions, setCartTransactions] = useState<Transaction[] | undefined>(undefined)
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [errorMsg, setErrorMsg] = useState<string>('')

    console.log(cartId);

    useEffect(() => {
        const handleFetch = async () => {
            const response = await CartTransactionApi.getCartTransactionsItems(cartId)
            if (!response.result) {
                setErrorMsg(response.error || 'Failed to get transactions for a cart')
                setIsLoading(false)
                return
            }
            setCartTransactions(response.result)
            setIsLoading(false)
        }

        handleFetch()
    }, [])

    console.log("Transactions: " + cartTransactions);

    return { cartTransactions, setCartTransactions, isLoading, errorMsg }
}