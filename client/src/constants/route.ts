
export const publicRoutes = [
    '/login'
]

export const GetPageUrl = {
    login: '/login',
    carts: (pageNumber: number) => `/dashboard/carts/${pageNumber}`,
    cart: (cartId: number, pageNumber: number) => `/dashboard/cart/${cartId}?pageNumber=${pageNumber}`,
    products: (pageNumber: number) => `/dashboard/products/${pageNumber}`,
    services: (pageNumber: number) => `/dashboard/services/${pageNumber}`,
    taxes: (pageNumber: number) => `/dashboard/taxes/${pageNumber}`,
    businessDetails: `/dashboard/business-details`
} as const