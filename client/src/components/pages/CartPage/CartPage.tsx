'use client'

import CartItemApi from '@/api/cartItem.api'
import Button from '@/components/shared/Button'
import SideDrawer, { SideDrawerRef } from '@/components/shared/SideDrawer'
import Table from '@/components/shared/Table'
import { useCartItems } from '@/hooks/cartItems.hook'
import { useCart } from '@/hooks/carts.hook'
import { TableColumnData } from '@/types/components/table'
import { RequiredCartItem, CartStatusEnum, ProductCartItem, ServiceCartItem, ProductModification, RequiredProductCartItem, RequiredServiceCartItem, TransactionStatusEnum, Transaction, TimeSlot } from '@/types/models'
import { useEffect, useRef, useState } from 'react'
import CreateProductCartItemForm from '../../specialized/CreateProductCartItemForm.tsx/CreateProductCartItemForm'
import CreateServiceCartItemView from '@/components/specialized/CreateServiceCartItemForm'
import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import CartDiscountApi from '@/api/cartDiscount.api'
import styles from './CartPage.module.scss'
import { useCartTransactions } from '../../../hooks/transactions.hook'
import { loadStripe } from '@stripe/stripe-js'
import PaymentApi from '@/api/payment.api'
import ServiceReservationApi from '@/api/serviceReservation.api'
import TimeSlotApi from '@/api/timeSlot.api'
import { CashCheckoutBody, CheckoutCartItem, DateTimeWithMicroseconds, FullCheckoutBody, GiftCardDetails, InitPartialCheckoutBody, PartialCheckoutBody, PartialTransaction, RefundBody } from '@/types/payment'
import { useCartDiscount } from '@/hooks/cartDiscount.hook'

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

const calculateDiscountsValue = (cartItem: RequiredCartItem, discountableValue: number) => {
    const percentageDiscount = cartItem.discounts.reduce((acc, discount) => (
        discount.isPercentage ? acc + discount.value : acc
    ), 0)
    const flatDiscount = cartItem.discounts.reduce((acc, discount) => (
        discount.isPercentage ? acc : acc + discount.value
    ), 0)

    const discountValue = flatDiscount + ((discountableValue - flatDiscount) * percentageDiscount / 10000)
    if (discountValue < 0) return 0
    return discountValue

}

const calculateTaxesValue = (cartItem: RequiredCartItem, discountedValue: number) => {
    const percentageTax = cartItem.taxes.reduce((acc, discount) => (
        discount.isPercentage ? acc + discount.rate : acc
    ), 0)
    const flatTax = cartItem.taxes.reduce((acc, discount) => (
        discount.isPercentage ? acc : acc + discount.rate
    ), 0)

    const taxValue = flatTax + ((discountedValue - flatTax) * percentageTax / 10000)
    if (taxValue < 0) return 0
    return taxValue
}

const CartPage = (props: Props) => {
    const splitCountRef = useRef<HTMLInputElement | null>(null);
    
    const { cartId, pageNumber } = props
    
    const { cart, setCart, isLoading: isCartLoading, isCartOpen, setIsCartOpen } = useCart(cartId)
    const { cartTransactions, setCartTransactions, isLoading: isCartTransactionsLoading} = useCartTransactions(cartId)
    const {
        cartItems,
        errorMsg,
        isLoading: isCartItemsLoading,
        refetchCartItems,
        setCartItems
    } = useCartItems(cartId, pageNumber)
    
    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'createProduct' | 'createService' | 'none'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('none')
    const [appliedTip, setAppliedTip] = useState<number>(0);
    const [selectedTimeSlot, setSelectedTimeSlot] = useState<TimeSlot | undefined>()

    if (!isCartLoading && !cart) return null

    const handleCartItemDelete = async (cartItemId: number) => {
        const response = await CartItemApi.deleteCartItem(cartId, cartItemId)
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
        { name: 'Modifications', key: 'modifications' },
        { name: 'Modifications Total', key: 'modificationTotal' },
        { name: 'Total Value', key: 'totalVal' },
        { name: 'Discounts', key: 'discounts' },
        { name: 'Taxes', key: 'taxes' },
        { name: 'Net price', key: 'netPrice' },
        { name: 'Delete', key: 'delete' }
    ]
    const productRows = productItems.map((item) => {
        const { name } = item.product
        const price = item.product.price / 100
        const modificationsPrice = calculateProductModificationsValue(item) / 100
        const totalVal = item.quantity * (price + modificationsPrice)
        const discounts = calculateDiscountsValue(item, (item.product.price + calculateProductModificationsValue(item)) * item.quantity) / 100
        const taxes = calculateTaxesValue(item, (totalVal - discounts) * 100) / 100
        const netPrice = totalVal - discounts - taxes
        return {
            name: name,
            quantity: item.quantity,
            modifications: item.productModifications.map((modification) => modification.name).join(', '),
            modificationTotal: modificationsPrice, price, totalVal, discounts, taxes, netPrice,
            delete: (
                <Button
                    onClick={() => handleCartItemDelete(item.id)}
                >
                    Delete
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
            { name: 'Delete', key: 'delete' },
        ]

    const serviceRows = serviceItems.map((item) => {
        let timeSlotStartTime = 'Cancelled'
        if (item.timeSlot) {
            timeSlotStartTime = new Date(item.timeSlot.startTime).toLocaleDateString()
        }
        const startTime = timeSlotStartTime
        const price = item.service.price / 100;
        const totalVal = item.quantity * price;
        const discounts = calculateDiscountsValue(item, totalVal * 100) / 100;
        const taxes = calculateTaxesValue(item, (totalVal - discounts) * 100) / 100;
        const netPrice = totalVal - discounts - taxes;
        return {
            name: item.service?.name || '',
            quantity: item.quantity,
            price,
            time: startTime,
            totalVal,
            discounts,
            taxes,
            netPrice,
            delete: (
                <Button
                    onClick={() => handleCartItemDelete(item.id)}
                >
                    Delete
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
    
    let totalPrice = productRows.reduce((acc, row) => acc + row.netPrice, 0) + serviceRows.reduce((acc, row) => acc + row.netPrice, 0);

    const { cartDiscount, setCartDiscount } = useCartDiscount(totalPrice, cartId);
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
            amount: `${(transaction.amount / 100).toFixed(2)} €`,
            tip: transaction.tip === null ? "" : `${transaction.tip?.toFixed(2)} €`,
            status: TransactionStatusEnum[transaction.status] || 'Unknown',
            card_payment_action: transaction.status === TransactionStatusEnum.PENDING ? (
                <DynamicForm
                    inputs={{
                        giftCardCode: { label: 'GiftCard', placeholder: 'Gift card:', type: 'string' },
                        valueToSpend: { label: 'Value to spend', placeholder: 'Value to spend:', type: 'string' },
                    }}
                    onSubmit={async (formPayload) => await handlePayment({ cartId: cartId, id: transaction.id }, { formPayload: formPayload })}
                >
                    <DynamicForm.Button>
                        Pay by card
                    </DynamicForm.Button>
                </DynamicForm>
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

        const response = await CartItemApi.createCartItem({
                type: 'product',
                cartId,
                quantity: quantityParsed,
                productVersionId: productId,
                variationIds: modificationIds,
            })
        if (!response.result) {
            console.log(response.error)
            return
        }
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const handleServiceCartItemCreate = async (formPayload: { serviceId: any; timeSlotId: any; customerName: any; customerPhone: any }) => {
        const { serviceId, timeSlotId, customerName, customerPhone} = formPayload

        if (!serviceId || !timeSlotId || !customerName || !customerPhone) {
            console.log('Invalid input')
            return
        }
        const response = await CartItemApi.createCartItem({
            cartId,
            type: 'service',
            quantity: 1,
            serviceVersionId: Number(serviceId)
        })
        console.log("serviceCreate resp: ", response.result)
        if (!response.result) {
            console.log(response.error)
            return
        }
        const cartItemId = response.result.id
        handleServiceReservationCreate(cartItemId, timeSlotId, customerName, customerPhone)
        handleTimeSlotUpdate(timeSlotId)
        refetchCartItems()
        sideDrawerRef.current?.close()
    }

    const handleServiceReservationCreate = async (cartItemId: number, timeSlotId: number, customerName: string, customerPhone: string) => {
        console.log("reservation: ", cartItemId, timeSlotId, customerName, customerPhone)
        const response = await ServiceReservationApi.create({
            cartItemId: Number(cartItemId),
            timeSlotId: timeSlotId,
            customerName: customerName,
            customerPhone: customerPhone,
            bookingTime: new Date(),
            isCancelled: false
        })
        console.log("reservation create: ", response.result)
        if (!response.result) {
            console.log(response.error)
            return
        }
    }

    const handleTimeSlotUpdate = async (timeSlotId: number) => {
        const response = await TimeSlotApi.getTimeSlotById(timeSlotId);
        if (!response.result) {
            console.log(response.error);
            return;
        }
        setSelectedTimeSlot(response.result)
        const timeSlot = response.result;
        console.log("timeSlot: ", response.result)
        if (!timeSlot) {
            console.error("TimeSlot not found");
            return;
        }
    
        const updateResponse = await TimeSlotApi.update({
            id: Number(timeSlot.id),
            employeeVersionId: Number(timeSlot.employeeVersionId),
            startTime: timeSlot.startTime,
            isAvailable: false,
        })

        if (updateResponse.result) {
            console.log("TimeSlot updated successfully")
        } else {
            console.error("Failed to update TimeSlot:", updateResponse.error)
        }
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

            setCartDiscount(discount / 100);
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

    const handleCashCheckout = async () => {
        let body: CashCheckoutBody = {
            cartId: cartId,
            amount: 10000,
            tip: appliedTip, // sita palikt ir kai sujungta bus
            transactionRef: new Date().toISOString() // sita palikt ir kai sujungta bus
        };

        let response = await PaymentApi.cashCheckout(body);

        if (response.error) {
            alert("Cash transaction was not created.");
            return;
        }

        if (response.result !== undefined) {
            if (cartTransactions !== undefined) {
                setCartTransactions(prev => [...prev, { ...response.result as Transaction, id: response.result!.id } ]);
                setIsCartOpen(false);
            } else {
                console.log(response.result);
                setCartTransactions([response.result]);
            }
        }
    }

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
                cartId: cartId,
                employeeId: cart?.employeeVersionId as number,
                tip: appliedTip,
                cartItems: cartItems.map(item => {
                    if (item.type === "product") {
                        const afterDiscount = item.product.price - calculateDiscountsValue(item, item.product.price);
                        const afterTax = afterDiscount - calculateTaxesValue(item, afterDiscount);
                        const cartItem: CheckoutCartItem = { name: item.product.name, description: item.product.description, price: afterTax, quantity: item.quantity, imageURL: item.product.imageURL };
                        return cartItem;
                    } else {
                        const afterDiscount = item.service.price - calculateDiscountsValue(item, item.service.price);
                        const afterTax = afterDiscount - calculateTaxesValue(item, afterDiscount);
                        return { name: item.service.name, description: item.service.description, price: afterTax, quantity: item.quantity, imageURL: item.service.imageURL }
                    }
                })
            };
            handlePayment(body);
        } else {
            if (totalPrice - cartDiscount < 30) {
                console.log('Cannot process split transaction if the total sum is less than 30 €');
            } else {
                console.log('Triggering multiple checkout');
                const body: InitPartialCheckoutBody = {
                    cartId: cartId,
                    employeeId: cart?.employeeVersionId as number,
                    tip: appliedTip,
                    paymentCount: splitCountParsed,
                    cartItems: cartItems.map(item => {
                        if (item.type === "product") {
                            const afterDiscount = item.product.price - calculateDiscountsValue(item, item.product.price);
                            const afterTax = afterDiscount - calculateTaxesValue(item, afterDiscount);
                            const cartItem: CheckoutCartItem = { name: item.product.name, description: item.product.description, price: afterTax, quantity: item.quantity, imageURL: item.product.imageURL };
                            return cartItem;
                        } else {
                            const afterDiscount = item.service.price - calculateDiscountsValue(item, item.service.price);
                            const afterTax = afterDiscount - calculateTaxesValue(item, afterDiscount);
                            return { name: item.service.name, description: item.service.description, price: afterTax, quantity: item.quantity, imageURL: item.service.imageURL }
                        }
                    })
                };

                handleSplitPaymentInit(body);
            }
        }
    }

    const handlePayment = async (body: FullCheckoutBody | PartialCheckoutBody, giftCard?: { formPayload: FormPayload } ) => {
        let giftCardDetails: GiftCardDetails | null = null;
        
        if (giftCard) {
            const { giftCardCode, valueToSpend } = giftCard.formPayload; 
            const valueAsInt = Number(valueToSpend);
            
            console.log(valueAsInt);

            if (!isNaN(valueAsInt) && valueAsInt !== 0) {
                giftCardDetails = {
                    code: giftCardCode,
                    valueToSpend: valueAsInt
                };
            }
        }

        let response = 'employeeId' in body
            ? await PaymentApi.fullCheckout(body as FullCheckoutBody)
            : giftCardDetails === null
                ? await PaymentApi.partialCheckout(body as PartialCheckoutBody)
                : await PaymentApi.partialCheckout({...body, giftCard: giftCardDetails } as PartialCheckoutBody);

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
        if (response.result&& Array.isArray(response.result.transactions)) {
            let transactions = response.result.transactions;
            transactions.map(tr => {
                let t = {...tr, amount: tr.amount / 100}; 

                if (tr.tip !== undefined)
                    t.tip = tr.tip;

                return t;
            });
            setCartTransactions((prev) => [...prev, ...transactions]);
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
                        <p>{`Tip: ${(appliedTip / 100).toFixed(2)} €`}</p>
                        <p>{`Total: ${((totalPrice - cartDiscount) + appliedTip / 100).toFixed(2)} €`}</p>
                        <div className={styles.split_checkout}>
                            <Button
                                onClick={handleCashCheckout}
                                disabled={isCartLoading || isCartItemsLoading || !isCartOpen}    
                            >
                                Cash
                            </Button>
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