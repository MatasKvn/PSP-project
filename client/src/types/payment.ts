
export type DateTimeWithMicroseconds = string;

export type RefundBody = {
    cartId: number,
    isCard: boolean
}

export type Checkout = {
    sessionId: string,
    pubKey: string
}

export type PartialTransaction = {
    id: string,
    amount: number,
    tip?: number,
    transactionRef: string,
    status: string
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
}