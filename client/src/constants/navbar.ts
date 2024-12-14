import { GetPageUrl } from './route'

export const navItems = [
    { name: 'Home', url: '/', href: '/' },
    { name: 'Carts', url: '/dashboard/carts', href: GetPageUrl.carts(0) },
    { name: 'Products', url: '/dashboard/products', href: GetPageUrl.products(0) },
    { name: 'Services', url: '/dashboard/services', href: GetPageUrl.services(0) },
    { name: 'Taxes', url: '/dashboard/taxes', href: GetPageUrl.taxes(0) },
    { name: 'Discounts', url: '/dashboard/discounts', href: GetPageUrl.discounts(0) }
]