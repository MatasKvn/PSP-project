export type Cart = {
    id: number
    dateCreated: Date
    status: CartStatusEnum
    employeeVersionId: number
}

export enum CartStatusEnum {
    PENDING,
    IN_PROGRESS,
    COMPLETED
}

export const getCartStatusEnumString = (status: CartStatusEnum): string => {
    switch (status) {
        case CartStatusEnum.PENDING:
            return 'Pending'
        case CartStatusEnum.IN_PROGRESS:
            return 'In Progress'
        case CartStatusEnum.COMPLETED:
            return 'Completed'
    }
}

export type CartDiscount = {
    id: number
    value: number
    isPercentage: boolean
    description: string
    startDate: Date
    endDate: Date
}

export type ItemDiscount = {
    id: number
    value: number
    isPercentage: boolean
    description: string
    startDate: Date
    endDate: Date
}

export type CartItem = ProductCartItem | ServiceCartItem

export type ProductCartItem = {
    type: 'product'
    id: number
    cartId: number
    quantity: number
    productId: number
    product?: Product,
    productModifications?: ProductModification[]
}

export type ServiceCartItem = {
    type: 'service'
    id: number
    cartId: number
    quantity: number
    serviceId: number
    service?: Service
    serviceReservationId: number
    serviceReservation?: ServiceReservation
    timeSlotId: number
    timeSlot?: TimeSlot
}

export type Product = {
    id: number
    name: string
    description: string
    price: number
    dateModified: Date
    imageUrl: string
    stock: number
}

export type ServiceReservation = {
    id: number
    cartItemId: number
    timeSlotId: number
    timeSlot?: TimeSlot
    bookingTime: Date
    customerPhone: string
    customerName: string
}

export type TimeSlot = {
    id: number
    employeeId: number
    startTime: Date
    isAvailable: boolean
}

export type Employee = {
    id: number
    firstName: string
    lastName: string
    email: string
    phoneNumber: string
    birthDate: Date
    startDate: Date
    endDate?: Date
    accessibility: AccessibilityEnum
}

export enum AccessibilityEnum {
    NONE = 'NONE',
    SERVICE_PROVIDER = 'SERVICE_PROVIDER',
    CASHIER = 'CASHIER',
    OWNER = 'OWNER',
    SUPER_ADMIN = 'SUPER_ADMIN',
}

export type Tax = {
    id: number
    name: string
    rate: number
    isPercentage: boolean
    dateModified: Date
}

export type ProductModification = {
    id: number
    productId: number
    name: string
    description: string
    price: number
    dateModified: Date
}

export type Service = {
    id: number
    name: string
    description: string
    duration: number
    price: number
    imageUrl: string
}
