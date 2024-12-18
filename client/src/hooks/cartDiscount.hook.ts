import CartDiscountApi from "@/api/cartDiscount.api";
import { useEffect, useState } from "react"

export const useCartDiscount = (totalPrice: number, cartId: number) => {
    const [cartDiscount, setCartDiscount] = useState<number>(0);
    console.log("aaaaaaaaaaaaaabbb ");
    useEffect(() => {
        const handleFetch = async () => {
            console.log("aaaaaaaaaaaaaaaaa ");
            const response = await CartDiscountApi.getCartDiscount(cartId);

            if (!response.result) {
                return
            }

            const discount = response.result.isPercentage
                ? totalPrice * response.result.value
                : response.result.value;

            setCartDiscount(discount / 100);
            console.log("aaaa " + totalPrice);
        }
    
        handleFetch();
    }, [totalPrice, cartId]);

    return { cartDiscount, setCartDiscount };
}