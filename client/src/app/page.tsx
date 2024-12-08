'use client'

import Button from "@/components/shared/Button"
import { GetPageUrl } from '@/constants/route'
import { useRouter } from "next/navigation"

export default function Home() {
    const router = useRouter()

    return (
        <div>
            <h1>POS Client</h1>
            <Button onClick={() => router.push(GetPageUrl.carts(0))}>Begin</Button>
        </div>
    )
}