'use client'

import Button from "@/components/shared/Button"
import { useRouter } from "next/navigation"

export default function Home() {
    const router = useRouter()

    return (
        <div>
            <h1>POS Client</h1>
            <Button onClick={() => router.push('/dashboard/carts/0')}>Begin</Button>
        </div>
    )
}