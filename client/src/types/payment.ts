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

export type FullCheckoutBody = {
    cartId: number,
    employeeId: number,
    tip?: number,
    giftCardCode?: string,
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

export type PartialCheckoutBody = {
    cartId: number,
    id: string
}