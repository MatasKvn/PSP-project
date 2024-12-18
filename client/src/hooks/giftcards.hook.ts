import { useState, useEffect } from "react"
import GiftCardApi from "../api/giftcard.api"
import { GiftCardsDetailFull } from "../types/payment"
import PagedResponseMapper from "../mappers/pagedResponse.mapper"

export const useGiftCards = (pageNum: number) => {
    const [giftCards, setGiftCards] = useState<GiftCardsDetailFull[]>([])
    const [isLoading, setIsLoading] = useState<boolean>(true)
    const [isError, setIsError] = useState<boolean>(false)

    useEffect(() => {
        if (pageNum === undefined) {
            setIsLoading(false)
            return
        }

        const handleFetch = async () => {
            const response = await GiftCardApi.getGiftCards(pageNum)
            if (response.result) {
                const giftCards = PagedResponseMapper.fromPageResponse(response.result!)
                setGiftCards(giftCards)
                setIsLoading(false)
                return
            }
            setIsError(true)
            setIsLoading(false)
        }

        handleFetch()
    }, [pageNum])

    return { giftCards, setGiftCards, isLoading, isError }
}