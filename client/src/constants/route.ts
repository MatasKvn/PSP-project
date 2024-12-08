
export const publicRoutes = [
    '/login'
]

export const GetPageUrl = {
    login: '/login',
    carts: (pageNumber: number) => `/dashboard/carts/${pageNumber}`,
    cart: (cartId: number, pageNumber: number) => `/dashboard/cart/${cartId}?pageNumber=${pageNumber}`,
    products: (pageNumber: number) => `/dashboard/products/${pageNumber}`,
    taxes: (pageNumber: number) => `/dashboard/taxes/${pageNumber}`
} as const