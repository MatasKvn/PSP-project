import { NextResponse, type NextRequest } from 'next/server'

import { verifyJwtToken } from './utils/jwtToken'
import { publicRoutes, routes } from './constants/route'

const isPublicRoute = (path: string) => !!publicRoutes.find((publicPath) => path.startsWith(publicPath))

export async function middleware(request: NextRequest) {

    const currentRoute = request.nextUrl.pathname

    if (currentRoute.startsWith('/_next')) {
        return NextResponse.next()
    }

    if (process.env.REQUIRE_AUTH !== 'YES') {
        return NextResponse.next()
    }

    // TODO: add route checking
    /* eslint-disable-next-line @typescript-eslint/no-unused-vars */
    const { isAuthenticated, payload } = await verifyJwtToken(request)

    if (isPublicRoute(currentRoute) && !isAuthenticated) {
        return NextResponse.next()
    }

    if (!isAuthenticated) {
        return NextResponse.redirect(new URL('/login', request.url))
    }

    if (isPublicRoute(currentRoute)) {
        return NextResponse.redirect(new URL(routes.carts, request.url))
    }

    return NextResponse.next()
}