import TaxesPage from '@/components/pages/TaxesPage'

type Props = {
    params: {
        pageNumber: number
    }
}

export default async function Page({ params }: Props) {
    const { pageNumber } = await params

    return <TaxesPage pageNumber={pageNumber} />

}