
export const publicRoutes = [
    '/login'
]

export const routes = {
    login: '/login',
    carts: '/dashboard/carts/0',
    cart: (cartId: number) => `/dashboard/cart/${cartId}`
} as const