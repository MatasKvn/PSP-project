import { NextResponse, type NextRequest } from 'next/server'

import { verifyJwtToken } from './utils/jwtToken'
import { publicRoutes, Routes } from './constants/route'

const isPublicRoute = (path: string) => !!publicRoutes.find((publicPath) => path.startsWith(publicPath))

export async function middleware(request: NextRequest) {

    const currentRoute = request.nextUrl.pathname

    if (currentRoute.startsWith('/_next')) {
        return NextResponse.next()
    }

    if (process.env.REQUIRE_AUTH !== 'YES') {
        return NextResponse.next()
    }

    const jwtValidationResult = await verifyJwtToken(request)
    if (!jwtValidationResult.isAuthenticated) {
        return NextResponse.redirect(new URL('/login', request.url))
    }

    if (isPublicRoute(currentRoute)) {
        return NextResponse.redirect(new URL(Routes.carts, request.url))
    }
    // TODO: add route checking
    /* eslint-disable-next-line @typescript-eslint/no-unused-vars */
    const { payload } = jwtValidationResult

    return NextResponse.next()
}