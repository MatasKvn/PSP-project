import { TransactionStatusEnum } from "./models";

export type DateTimeWithMicroseconds = string;

export type CashCheckoutBody = {
    cartId: number,
    amount: number,
    tip?: number,
    transactionRef: string
}

export type RefundBody = {
    cartId: number,
    isCard: boolean
}

export type Checkout = {
    sessionId: string,
    pubKey: string
}

export type PartialTransactions = {
    transactions: PartialTransaction[]
}

export type PartialTransaction = {
    id: DateTimeWithMicroseconds,
    amount: number,
    tip?: number,
    transactionRef: string,
    status: TransactionStatusEnum
}

export interface FullCheckoutBody {
    cartId: number,
    employeeId: number,
    tip?: number,
    cartItems: CheckoutCartItem[],
}

export type InitPartialCheckoutBody = FullCheckoutBody & {
    paymentCount: number
}

export type CheckoutCartItem = {
    name: string,
    description: string,
    price: number,
    quantity: number,
    imageURL: string
}

export interface PartialCheckoutBody {
    cartId: number,
    id: DateTimeWithMicroseconds
    giftCard?: GiftCardDetails
}

export type GiftCardDetails = {
    code: string,
    valueToSpend: number
}