import { apiBaseUrl, defaultHeaders } from "@/constants/api";
import { FetchResponse, HTTPMethod } from "@/types/fetch";
import { DateTimeWithMicroseconds, Transaction } from "@/types/models";
import { Checkout, FullCheckoutBody, InitPartialCheckoutBody, PartialCheckoutBody, PartialTransaction, RefundBody } from "@/types/payment";
import { fetch } from "@/utils/fetch";

export default class PaymentApi {
    static async fullCheckout(body: FullCheckoutBody) : Promise<FetchResponse<Checkout>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/full-checkout`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(body)
        });
    }

    static async initializePartialCheckout(body: InitPartialCheckoutBody): Promise<FetchResponse<PartialTransaction[]>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/init-partial-checkout`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(body)
        });
    }

    static async partialCheckout(body: PartialCheckoutBody): Promise<FetchResponse<Checkout>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/partial-checkout`,
            method: HTTPMethod.POST,
            headers: defaultHeaders,
            body: JSON.stringify(body)
        });
    }

    static async refundPayment(id: DateTimeWithMicroseconds, body: RefundBody): Promise<FetchResponse<Transaction>> {
        return await fetch({
            url: `${apiBaseUrl}/payments/refund/${id}`,
            method: HTTPMethod.PATCH,
            headers: defaultHeaders,
            body: JSON.stringify(body)
        });
    }
};

