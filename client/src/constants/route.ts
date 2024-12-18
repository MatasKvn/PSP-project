
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
    timeSlots: (pageNumber: number) => `/dashboard/time-slots/${pageNumber}`,
    discounts: (pageNumber: number) => `/dashboard/discounts/${pageNumber}`,
    businessDetails: `/dashboard/business-details`,
    employees: (pageNumber: number) => `/dashboard/employees/${pageNumber}`,
    giftcards: (pageNumber: number) => `/dashboard/giftcards/${pageNumber}`
} as const