import EmployeesPage from '@/components/pages/EmployeesPage'
import React from 'react'

type Props = {
    params: {
        pageNumber: number
    }
}

const Page = async ({ params }: Props) => {
    const { pageNumber } = await params
    return (
        <EmployeesPage
            pageNumber={pageNumber}
        />
    )
}

export default Page