
export type DateTimeWithMicroseconds = string;

export type Cart = {
    id: number,
    dateCreated: Date,
    status: CartStatusEnum,
    employeeVersionId: number,
    discount?: number,
}

export type CartDiscountBody = {
    discountCode: string
};

export type CartDiscount = {
    id: string,
    value: number,
    isPercentage: boolean
}

export enum CartStatusEnum {
    PENDING,
    IN_PROGRESS,
    COMPLETED,
    REFUNDED
}

export type Transaction = {
    id: DateTimeWithMicroseconds,
    amount: number,
    tip?: number,
    transactionRef: string,
    status: TransactionStatusEnum,
}

export enum TransactionStatusEnum {
    SUCEEDED,
    FAILED,
    PENDING,
    REFUNDED,
    CASH
}

export const getCartStatusEnumString = (status: CartStatusEnum): string => {
    switch (status) {
        case CartStatusEnum.PENDING:
            return 'Pending'
        case CartStatusEnum.IN_PROGRESS:
            return 'In Progress'
        case CartStatusEnum.COMPLETED:
            return 'Completed'
        case CartStatusEnum.REFUNDED:
            return 'Refunded'
    }
}

export type ItemDiscount = {
    id: number
    value: number
    isPercentage: boolean
    description: string
    startDate: Date
    endDate: Date
}

export type RequiredCartItem = RequiredProductCartItem | RequiredServiceCartItem
export type RequiredServiceCartItem = Required<ServiceCartItem>
export type RequiredProductCartItem = Required<ProductCartItem>

export type CartItem = ProductCartItem | ServiceCartItem

export type ProductCartItem = {
    type: 'product'
    id: number
    cartId: number
    quantity: number
    productId: number
    product?: Product,
    productModifications?: ProductModification[]
    discounts?: ItemDiscount[]
    taxes?: Tax[]
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
    discounts?: ItemDiscount[]
    taxes: Tax[]
}

export type Product = {
    id: number
    name: string
    description: string
    price: number
    dateModified: Date
    imageURL: string
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

export type BusinessDetails = {
    businessName: string
    businessEmail: string
    businessPhone: string
    country: string
    city: string
    street: string
    houseNumber: number
    flatNumber?: number
}

export enum RoleEnum {
    NONE = 0,               
    SERVICE_PROVIDER = 1,    
    CASHIER = 2,            
    OWNER = 3,               
    SUPER_ADMIN = 4,         
  }

  export type Employee = {
    id: number
    firstName: string
    lastName: string
    userName: string
    email: string
    password: string
    phoneNumber: string
    birthDate?: string
    startDate?: Date
    endDate?: Date
    roleId: number; 
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
    productVersionId: number
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
    imageURL: string
    employeeId: number
}
