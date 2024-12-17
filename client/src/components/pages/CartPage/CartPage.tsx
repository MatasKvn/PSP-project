'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from '@/hooks/carts.hook'
import { TableColumnData } from '@/types/components/table'
import { RequiredCartItem, CartStatusEnum, ProductCartItem, ServiceCartItem, ProductModification, RequiredProductCartItem, RequiredServiceCartItem, TransactionStatusEnum, Transaction } from '@/types/models'
import { useRef, useState } from 'react'
import CreateProductCartItemForm from '../../specialized/CreateProductCartItemForm.tsx/CreateProductCartItemForm'
import CreateServiceCartItemView from '@/components/specialized/CreateServiceCartItemForm'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import CartDiscountApi from '@/api/cartDiscount.api'
import styles from './CartPage.module.scss'
import { useCartTransactions } from '../../../hooks/transactions.hook'
import { loadStripe } from '@stripe/stripe-js'
import PaymentApi from '@/api/payment.api'
import { DateTimeWithMicroseconds, FullCheckoutBody, InitPartialCheckoutBody, PartialCheckoutBody, RefundBody } from '@/types/payment'

type Props = {
    cartId: number
    pageNumber: number
}

const calculateProductModificationsValue = (cartItem: RequiredCartItem) => {
    if (cartItem.type === 'product') {
        return cartItem.productModifications.reduce((acc, modification) => acc + modification.price, 0)
    }
    return 0
}

const calculateDiscountsValue = (cartItem: RequiredCartItem) => {
    const value = cartItem.type === 'product' ? cartItem.product?.price : cartItem.service?.price
    const productModificaitonValue = calculateProductModificationsValue(cartItem)
    const netPrice = (value + productModificaitonValue) * cartItem.quantity
    return cartItem.discounts.reduce((acc, discount) => {
        if (!discount.isPercentage) {
            return acc + discount.value
        }
        return acc + netPrice * discount.value / 100
    }, 0)
}

const calculateTaxesValue = (cartItem: RequiredCartItem) => {
    const value = cartItem.type === 'product' ? cartItem.product?.price : cartItem.service?.price
    const taxableValue = (value + calculateProductModificationsValue(cartItem)) * cartItem.quantity
    return cartItem.taxes.reduce((acc, tax) => {
        if (tax.isPercentage) {
            return acc + taxableValue * tax.rate / 100
        }
        return acc + tax.rate
    }, 0)
}

const CartPage = (props: Props) => {
    const splitCountRef = useRef<HTMLInputElement | null>(null);

    const { cartId, pageNumber } = props

    const { cart, setCart, isLoading: isCartLoading } = useCart(cartId)
    const { cartTransactions, setCartTransactions, isLoading: isCartTransactionsLoading} = useCartTransactions(cartId)

    const isCartOpen = cart?.status === CartStatusEnum.IN_PROGRESS
    const {
        cartItems,
        errorMsg,
        isLoading: isCartItemsLoading,
        refetchCartItems
    } = useCartItems(cartId, pageNumber)

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'createProduct' | 'createService' | 'none'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('none')
    const [cartDiscount, setCartDiscount] = useState<number>(0)
    const [appliedTip, setAppliedTip] = useState<number>(0);

    if (!isCartLoading && !cart) return null

    const handleCartItemDeconste = async (cartItemId: number) => {
        const response = await CartItemApi.deconsteCartItem(cartItemId)
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
    }

    const productItems = cartItems.filter((cartItem) => cartItem.type === 'product')
    const productsColumns: TableColumnData[] = [
        { name: 'Name', key: 'name' },
        { name: 'Quantity', key: 'quantity' },
        { name: 'Price', key: 'price' },
        { name: 'Modification Ids', key: 'modifications' },
        { name: 'Modifications Total', key: 'modificationTotal' },
        { name: 'Total Value', key: 'totalVal' },
        { name: 'Discounts', key: 'discounts' },
        { name: 'Taxes', key: 'taxes' },
        { name: 'Net price', key: 'netPrice' },
        { name: 'Deconste', key: 'deconste' }
    ]
    const productRows = productItems.map((item) => {
        const { name } = item.product
        const price = item.product.price / 100
        const modificationsPrice = calculateProductModificationsValue(item)
        const totalVal = item.quantity * (item.product.price + modificationsPrice) / 100
        const discounts = calculateDiscountsValue(item) / 100
        const taxes = calculateTaxesValue(item) / 100
        const netPrice = totalVal - discounts + taxes
        return {
            name: name,
            quantity: item.quantity,
            modifications: item.productModifications.map((modification) => modification.id).join(', '),
            modificationTotal: modificationsPrice, price, totalVal, discounts, taxes, netPrice,
            deconste: (
                <Button
                    onClick={() => handleCartItemDeconste(item.id)}
                >
                    Deconste
                </Button>
            )
        }
    })
    const productsRowsStringified = productRows.map((row) => Object.fromEntries(Object.entries(row).map(
        ([key, value]) => {
            if (typeof value === 'number' && key !== 'quantity') {
                return [key, value.toFixed(2)]
            }
            return [key, value]
        })))
    const productsSummaryRow = {
        name: 'Total',
        quantity: productRows.reduce((acc, row) => acc + row.quantity, 0),
        price: productRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
        modifications: '...',
        modificationTotal: productRows.reduce((acc, row) => acc + row.modificationTotal, 0).toFixed(2),
        discounts: productRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
        taxes: productRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
        totalVal: productRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
        netPrice: productRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2)
    }
    const productsTableRows = [...productsRowsStringified, productsSummaryRow]

    const serviceItems = cartItems.filter((cartItem) => cartItem.type === 'service')
    const serviceColumns: TableColumnData[] = [
            { name: 'Name', key: 'name' },
            { name: 'Time', key: 'time' },
            { name: 'Price', key: 'price' },
            { name: 'Total Value', key: 'totalVal' },
            { name: 'Discounts', key: 'discounts' },
            { name: 'Taxes', key: 'taxes' },
            { name: 'Net price', key: 'netPrice' },
            { name: 'Deconste', key: 'deconste' },
        ]
    const serviceRows = serviceItems.map((item) => {
        const startTime = item.timeSlot.startTime
        return {
            name: item.service.name,
            quantity: item.quantity,
            price: item.service?.price,
            time: `${startTime.getMonth() + 1} ${startTime.getDay()} ${startTime.toLocaleTimeString()}`,
            totalVal: item.quantity * (item.service?.price ? item.service.price : 0),
            discounts: calculateDiscountsValue(item),
            taxes: calculateTaxesValue(item),
            netPrice: (item.quantity * (item.service?.price ? item.service.price : 0)) - calculateDiscountsValue(item) + calculateTaxesValue(item),
            deconste: (
                <Button
                    onClick={() => handleCartItemDeconste(item.id)}
                >
                    Deconste
                </Button>
            )
        }
    })
    const serviceRowsStringified = serviceRows.map((row) => Object.fromEntries(Object.entries(row).map(
        ([key, value]) => {
            console.log(key, value)
            if (typeof value === 'number') {
                return [key, value.toFixed(2)]
            }
            return [key, value]
        })))
    const summaryRow = {
        name: 'Total',
        time: '...',
        quantity: serviceRows.reduce((acc, row) => acc + row.quantity, 0).toFixed(2),
        price: serviceRows.reduce((acc, row) => acc + row.price, 0).toFixed(2),
        totalVal: serviceRows.reduce((acc, row) => acc + row.totalVal, 0).toFixed(2),
        discounts: serviceRows.reduce((acc, row) => acc + row.discounts, 0).toFixed(2),
        taxes: serviceRows.reduce((acc, row) => acc + row.taxes, 0).toFixed(2),
        netPrice: serviceRows.reduce((acc, row) => acc + row.netPrice, 0).toFixed(2),
    }
    const servicesTableRows = [...serviceRowsStringified, summaryRow]

    const totalPrice = productRows.reduce((acc, row) => acc + row.netPrice, 0) + serviceRows.reduce((acc, row) => acc + row.netPrice, 0) - cartDiscount;
    const cartTransactionTable = () => {
        const cartTransactionColumns = [
            { name: 'Id', key: 'id' },
            { name: 'Amount', key: 'amount' },
            { name: 'Tip', key: 'tip' },
            { name: 'Status', key: 'status' },
            { name: 'Card payment', key: 'card_payment_action' },
            { name: 'Cash payment', key: 'cash_payment_action' },
            { name: 'Refund', key: 'refund_action' }
        ]

        if (!cartTransactions) {
            return <div>Loading...</div>;
        }

        const cartTransactionRows = cartTransactions.map(transaction => ({
            id: transaction.id,
            amount: `${transaction.amount.toFixed(2)} €`,
            tip: transaction.tip === null ? "" : `${transaction.tip?.toFixed(2)} €`,
            status: TransactionStatusEnum[transaction.status] || 'Unknown',
            card_payment_action: transaction.status === TransactionStatusEnum.PENDING ? (
                <Button onClick={async () => await handlePayment({ cartId: cartId, id: transaction.id })}>
                    Pay by card
                </Button>
            ) : null,
            cash_payment_action: transaction.status === TransactionStatusEnum.PENDING ? (
                <Button>
                    Pay by cash
                </Button>
            ) : null,
            refund_action: ((transaction.status === TransactionStatusEnum.SUCEEDED) && allTransactionsSucceededOrRefunded(cartTransactions)) || transaction.status === TransactionStatusEnum.CASH ? (
                <Button onClick={async () => await handleRefund(transaction.id, { cartId: cartId, isCard: transaction.status === TransactionStatusEnum.SUCEEDED ? true : false })}>
                    Refund
                </Button>
            ) : null
        }));

        return (
            <Table
                columns={cartTransactionColumns}
                rows={cartTransactionRows}
            />
        )
    }

    const allTransactionsSucceededOrRefunded = (transactions: Transaction[]): boolean => {
        return transactions.every(transaction => transaction.status === TransactionStatusEnum.SUCEEDED || transaction.status === TransactionStatusEnum.REFUNDED);
    };

    const handleProductItemCreate = async (formPayload: { productId: number; quantity: string; modificationIds: number[] }) => {
        const { productId, quantity, modificationIds } = formPayload
        const quantityParsed = parseInt(quantity)
        if (isNaN(quantityParsed)) {
            console.log('Invalid input')
            return
        }

        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'product',
                quantity: quantityParsed,
                productVersionId: productId,
                variationIds: modificationIds,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const handleServiceCartItemCreate = async ({ serviceId }: { serviceId: number | undefined }) => {
        if (!serviceId) {
            console.log('Invalid input')
            return
        }
        const response = await CartItemApi.createCartItem(
            cartId,
            {
                type: 'service',
                quantity: 1,
                serviceVersionId: serviceId,
            }
        )
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const handleCartDiscount = async (formPayload: FormPayload) => {
        const { code } = formPayload;
        console.log(code);
        if (cart === undefined)
            throw new Error("Cart data could not be loaded.");

        if (cart.status !== CartStatusEnum.IN_PROGRESS) {
            return Promise.resolve({
                error: 'Cannot apply discount to not in progress cart.'
            })
        }

        const response = await CartDiscountApi.applyCartDiscount(cart.id, { discountCode: code });
        if (response.error) {
            console.log(response.error)
            return
        }
        
        if (response.result) {
            const discount = response.result.isPercentage
                ? totalPrice * response.result.value
                : response.result.value;

            setCartDiscount(discount);
        }
    }

    const handconstip = async (formPayload: FormPayload) => {
        const { tip } = formPayload;
        const tipParsed = parseInt(tip);

        if (isNaN(tipParsed)) {
            console.log('Invalid input')
            return
        }
        setAppliedTip(tipParsed);
    }

    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'createProduct') return <CreateProductCartItemForm onSubmit={(payload) => handleProductItemCreate(payload)} />
        if (sideDrawerContentType === 'createService') return <CreateServiceCartItemView onSubmit={handleServiceCartItemCreate} />
        return <></>
    }

    const tablesSection = () => (
        <div className={styles.tables_container}>
            <div>
                <h4>Products</h4>
                <Table
                    isLoading={isCartItemsLoading}
                    errorMsg={errorMsg}
                    columns={productsColumns}
                    rows={productsTableRows}
                    lastRowHighlight
                />
                <Button
                    onClick={() => {
                        setSideDrawerContentType('createProduct')
                        sideDrawerRef.current?.open()
                    }}
                    disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                >
                    Add Product
                </Button>
            </div>
            <div>
                <h4>Services</h4>
                <Table
                    isLoading={isCartItemsLoading}
                    errorMsg={errorMsg}
                    columns={serviceColumns}
                    rows={servicesTableRows}
                    lastRowHighlight
                />
                <Button
                    onClick={() => {
                        setSideDrawerContentType('createService')
                        sideDrawerRef.current?.open()
                    }}
                    disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                >
                    Add service
                </Button>
            </div>
            <div>
                <h4>Related Transactions</h4>
                { cartTransactionTable() }
            </div>
        </div>
    )

    const handleCartCheckout = () => {
        const splitCountValue = splitCountRef.current?.value.trim();
        const splitCountParsed = splitCountValue ? parseInt(splitCountValue) : null;

        if (splitCountParsed === null || splitCountParsed < 1) {
            console.log('Invalid input for split transaction provided');
            return;
        }

        if (splitCountParsed > 5) {
            console.log('Cannot process more than 5 split transactions');
            return;
        }

        if (splitCountParsed === 1 || splitCountValue === '') {
            console.log('Triggering single checkout');
            const body: FullCheckoutBody = {
                cartId: 1,
                employeeId: 6,
                tip: appliedTip,
                cartItems: [
                    { name: "a", description: "a", price: 10000, quantity: 2, imageURL: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCy16nhIbV3pI1qLYHMJKwbH2458oiC9EmA&s" },
                    { name: "a", description: "a", price: 10000, quantity: 2, imageURL: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCy16nhIbV3pI1qLYHMJKwbH2458oiC9EmA&s" }
                ]
            };
            handlePayment(body);
        } else {
            if (totalPrice - cartDiscount < 30) {
                console.log('Cannot process split transaction if the total sum is less than 30 €');
            } else {
                console.log('Triggering multiple checkout');
                const body: InitPartialCheckoutBody = {
                    cartId: 1,
                    employeeId: 6,
                    tip: appliedTip,
                    paymentCount: splitCountParsed,
                    cartItems: [
                        { name: "a", description: "a", price: 10000, quantity: 2, imageURL: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCy16nhIbV3pI1qLYHMJKwbH2458oiC9EmA&s" },
                        { name: "a", description: "a", price: 10000, quantity: 2, imageURL: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCy16nhIbV3pI1qLYHMJKwbH2458oiC9EmA&s" }
                    ]
                };

                handleSplitPaymentInit(body);
            }
        }
    }

    const handlePayment = async (body: FullCheckoutBody | PartialCheckoutBody) => {
        const response = 'employeeId' in body
            ? await PaymentApi.fullCheckout(body as FullCheckoutBody)
            : await PaymentApi.partialCheckout(body as PartialCheckoutBody);

        if (response.error) {
            alert("Payment session was not created.");
            return;
        }

        if (response.result) {
            const stripe = await loadStripe(response.result.pubKey);
            
            if (!stripe) {
                alert("Stripe was not launched.");
            }

            const result = await stripe?.redirectToCheckout({ sessionId: response.result.sessionId });

            if (!result) {
                console.log(result);
                alert("Transaction failed.");
            }
        }
    }

    const handleSplitPaymentInit = async (body: InitPartialCheckoutBody) => {       
        const response = await PaymentApi.initializePartialCheckout(body);

        if (response.error) {
            alert("Payments were not initialized.");
            return;
        }
    }

    const handleRefund = async (id: DateTimeWithMicroseconds, body: RefundBody) => {
        const response = await PaymentApi.refundPayment(id, body);

        if (response.error) {
            alert("Failed to issue refund.");
            return;
        }
        
        if (response.result) {
            const refundedTransaction = response.result;
            setCartTransactions(cartTransactions?.map((value) => value.id === id ? refundedTransaction : value));
        }
    }

    return (
        <div className={styles.page}>
            <h2 className={styles.header}>{`Items of cart: ${cartId}`}</h2>
            {tablesSection()}
            <div className={styles.discount_summary_wrapper}>
                <div className={styles.discount_container}>
                    <h4>Cart Discount</h4>
                    <DynamicForm
                        inputs={{
                            code: { label: 'Discount', placeholder: 'Enter discount code:', type: 'string' },
                        }}
                        onSubmit={(formPayload) => handleCartDiscount(formPayload)}
                    >
                        <DynamicForm.Button>Apply Discount</DynamicForm.Button>
                    </DynamicForm>
                </div>
                <div className={styles.discount_container}>
                    <h4>Apply a tip</h4>
                    <DynamicForm
                        inputs={{
                            tip: { label: 'Tip', placeholder: 'Enter tip amount:', type: 'number' },
                        }}
                        onSubmit={(formPayload) => handconstip(formPayload)}
                    >
                        <DynamicForm.Button>Apply Tip</DynamicForm.Button>
                    </DynamicForm>
                </div>
                <div className={styles.summary_container}>
                    <h4>Checkout</h4>
                    <div>
                        <p>{`Total: ${totalPrice.toFixed(2)} €`}</p>
                        <p>{`Discount: ${cartDiscount.toFixed(2)} €`}</p>
                        <p>{`Tip: ${appliedTip.toFixed(2)} €`}</p>
                        <p>{`Total: ${((totalPrice - cartDiscount) + appliedTip).toFixed(2)} €`}</p>
                        <div className={styles.split_checkout}>
                            <Button
                                onClick={handleCartCheckout}
                                disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                            >
                                Checkout
                            </Button>
                            <input
                                type="number"
                                placeholder="Number of people"
                                min="1"
                                className={styles.split_input}
                                disabled={isCartLoading || isCartItemsLoading || !isCartOpen}
                                ref={splitCountRef}
                            />
                        </div>
                    </div>
                </div>
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {sideDrawerContent()}
            </SideDrawer>
        </div>
    )
}

export default CartPage
