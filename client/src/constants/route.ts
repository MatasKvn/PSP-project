
export const publicRoutes = [
    '/login'
]

export const routes = {
    login: '/login',
    carts: '/dashboard/carts/0',
    cart: (cartId: number) => `/dashboard/cart/${cartId}`,
    products: '/dashboard/products/0'
} as const