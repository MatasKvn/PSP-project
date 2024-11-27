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

export type CartItem = {
    id: number
    quantity: number
    notes: string
} & { product: Product, productModifications: ProductModification[] } | { serviceReservation?: ServiceReservation }

export type Product = {
    id: number
    name: string
    description: string
    price: number
    dateModified: Date
    imageUrl: string
}

export type ServiceReservation = {
    id: number
    timeSlot: TimeSlot
    customerName: string
    customerPhone: string
}

export type TimeSlot = {
    id: number
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
