// TODO: check on this when backend models are finalized

export type Cart = {
    id: number
    dateCreated: Date
    isCompleted: boolean
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
    itemType: string // Not sure
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
    product?: Product
    serviceReservation?: ServiceReservation
}

export type Product = {
    id: number
    name: string
    description: string
    price: number
    dateAdded: Date
    dateModified: Date
}

export type ServiceReservation = {
    id: number
    // what is this, is this nescessary, as timeslot is already linked
    bookingTime: Date
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
    // password: string // Not sure
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
    dateModified: Date // Version
}

export type ProductModification = {
    id: number
    name: string
    description: string
    price: number
    dateModified: Date // Version
}

export type Service = {
    id: number
    name: string
    description: string
    duration: number
    price: number
    imageUrl: string
}
