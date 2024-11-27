
import CartsPage from "@/components/pages/CartsPage"

type Props = {
    params: {
        pageNumber: number
    }
}

const Page = async ({ params }: Props) => {
    const { pageNumber } = await params

    return (
        <CartsPage
            pageNumber={pageNumber}
        />
    )
}

export default Page
