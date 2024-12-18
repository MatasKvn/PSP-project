'use client'

import { useRouter } from 'next/navigation'
import { GetPageUrl } from '../../../constants/route'
import Button from '../../shared/Button'
import PageChanger from '../../shared/PageChanger'
import SideDrawer, { SideDrawerRef } from '../../shared/SideDrawer'
import styles from './GiftCardsPage.module.scss'
import { useGiftCards } from '../../../hooks/giftcards.hook'
import { useRef, useState } from 'react'
import { GiftCardsDetailFull } from '../../../types/payment'
import Table from '../../shared/Table'
import DynamicForm, { DynamicFormPayload } from '../../shared/DynamicForm'
import BusinessDetailsApi from '../../../api/businessDetails.api'
import GiftCardApi from '../../../api/giftcard.api'

type Props = {
    pageNumber: number
}

const GiftCardsPage = (props: Props) => {
    const { pageNumber } = props
    const router = useRouter()

    const { giftCards, setGiftCards, isLoading, isError } = useGiftCards(pageNumber)
    const [selectedGiftCard, selectGiftCard] = useState<GiftCardsDetailFull | undefined>()

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)

    const giftCardTable = () => {
        const columns = [
            { name: 'Code', key: 'code' },
            { name: 'Date', key: 'date' },
            { name: 'Amount', key: 'amount' },
            { name: 'Delete', key: 'delete_action'}
        ];

        const rows = giftCards.map((giftcard: GiftCardsDetailFull) => ({
            code: giftcard.id,
            date: giftcard.date,
            amount: giftcard.value,
            delete_action: (
                <Button
                    onClick={() => handleGiftCardDelete(giftcard.id)}
                >
                    Delete
                </Button>
            )
        }));

        return (
            <Table
                columns={columns}
                rows={rows}
            />
        );
    }

    const giftCardForm = () => {
        return (
            <>
                <h4>{'Create Gift Card'}</h4>
                <DynamicForm
                    inputs={{
                        date: { label: 'Expiration Date', placeholder: 'Enter gift card expiration date:', type: 'date' },
                        amount: { label: 'Amount', placeholder: 'Enter gift card value:', type: 'number' },
                    }}
                    onSubmit={handleGiftCardCreate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }

    const handleGiftCardCreate = async (formPayload: DynamicFormPayload) => {
        const {
            date,
            amount
        } = formPayload

        const response = await GiftCardApi.createGiftCard({
            date: new Date(date).toLocaleDateString('en-CA'),
            value: Number.parseInt(amount)
        })

        if (!response.result) {
            console.log(response.error)
            return
        }

        const newGiftCard = response.result;
        setGiftCards((prevGiftCards) => [...prevGiftCards, newGiftCard]);

        sideDrawerRef.current?.close()
        console.log('Gift Card created successfully')
    }

    const handleGiftCardDelete = async (id: number) => {
        const response = await GiftCardApi.deleteGiftCard(id)
        if (!response.result) {
            console.log(response.error)
            return
        }

        setGiftCards((prevGiftCards) => prevGiftCards.filter((giftCard) => giftCard.id !== id));
        selectGiftCard(undefined)
    }

    return (
        <div className={styles.page}>
            <h1>GiftCard Page</h1>
            <div className={styles.toolbar}>
                <Button onClick={() => {
                    sideDrawerRef.current?.open()
                }}>Create GiftCard</Button>
            </div>
            <div>{giftCardTable()}</div>
            <PageChanger
                onClickNext={() => router.push(GetPageUrl.giftcards(parseInt(pageNumber as unknown as string) + 1))}
                onClickPrevious={() => router.push(GetPageUrl.giftcards(pageNumber - 1))}
                disabledPrevious={pageNumber <= 0}
                pageNumber={pageNumber}
            />
            <SideDrawer ref={sideDrawerRef}>
                {giftCardForm()}
            </SideDrawer>
        </div>
    )
}

export default GiftCardsPage