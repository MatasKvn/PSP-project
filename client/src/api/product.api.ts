import { Product } from './../types/models'
import { apiBaseUrl, defaultHeaders } from '@/constants/api'
import { FetchResponse, HTTPMethod, PagedResponse } from '@/types/fetch'
import { Product } from '@/types/models'
import { fetch } from '@/utils/fetch'

let products: Product[] = [
    {
        id: 1,
        name: 'Banana',
        description: 'Banana is a fruit',
        price: 10,
        dateModified: new Date(),
        imageUrl: '',
        stock: 11
    },
    {
        id: 2,
        name: 'Apple',
        description: 'Apple is a fruit',
        price: 20,
        dateModified: new Date(),
        imageUrl: 'https://upload.wikimedia.org/wikipedia/en/c/c2/Peter_Griffin.png',
        stock: 12
    },
    {
        id: 3,
        name: 'Orange',
        description: 'Orange is a fruit',
        price: 30,
        dateModified: new Date(),
        imageUrl: '',
        stock: 13
    },
    {
        id: 4,
        name: 'Grape',
        description: 'Grape is a fruit',
        price: 40,
        dateModified: new Date(),
        imageUrl: '',
        stock: 14
    },
    {
        id: 5,
        name: 'Strawberry',
        description: 'Strawberry is a fruit',
        price: 50,
        dateModified: new Date(),
        imageUrl: '',
        stock: 15
    },
    {
        id: 6,
        name: 'Pineapple',
        description: 'Pineapple is a fruit',
        price: 60,
        dateModified: new Date(),
        imageUrl: '',
        stock: 16
    },
    {
        id: 7,
        name: 'Watermelon',
        description: 'Watermelon is a fruit',
        price: 70,
        dateModified: new Date(),
        imageUrl: '',
        stock: 17
    },
    {
        id: 8,
        name: 'Mango',
        description: 'Mango is a fruit',
        price: 80,
        dateModified: new Date(),
        imageUrl: '',
        stock: 18
    }
]

export default class ProductApi {
    static async getAllProducts(pageNumber: number): Promise<FetchResponse<PagedResponse<Product>>> {
        // return await fetch({
        //     url: `${apiBaseUrl}/product?pageNum=${pageNumber}`,
        //     method: HTTPMethod.GET,
        //     headers: defaultHeaders
        // })
        return Promise.resolve({
            result: {
                totalCount: 0,
                pageSize: 0,
                pageNum: 0,
                results: products.map((product) => ({ ...product }))
            }
        })
    }

    static async getProductById(productId: number): Promise<FetchResponse<Product>> {
        // return await fetch({
        //     url: `${apiBaseUrl}/product/${productId}`,
        //     method: HTTPMethod.GET,
        //     headers: defaultHeaders
        // })
        return Promise.resolve({
            result: products[productId - 1]
        })
    }

    static async createProduct(product: CreateProductDto): Promise<FetchResponse<Product>> {
        // return await fetch({
        //     url: `${apiBaseUrl}/product`,
        //     method: HTTPMethod.POST,
        //     headers: defaultHeaders,
        //     body: JSON.stringify(product)
        // })
        if (Object.entries(product).some(([key, value]) => key === 'imageUrl' ? false : !value)) {
            throw new Error('Cannot create Product with missing properties')
        }
        const productToCreate: Product = {
            ...product,
            dateModified: new Date(),
            id: Math.max(...products.map(p => p.id)) + 1
        }
        products.push(productToCreate)
        return Promise.resolve({ result: productToCreate })
    }

    static async deleteProductById(productId: number): Promise<FetchResponse<any>> {
        // return await fetch({
        //     url: `${apiBaseUrl}/product/${productId}`,
        //     method: HTTPMethod.DELETE,
        //     headers: defaultHeaders
        // })
        const productToDelete = products.find((product) => product.id === productId)
        if (!productToDelete) return Promise.resolve({ error: 'Product not found' })
        const filteredProducts = products.filter((product) => product.id !== productId)
        products = filteredProducts
        return Promise.resolve({ })
    }

    static async updateProductById(productId: number, product: EditProductDto): Promise<FetchResponse<Product>> {
        // return await fetch({
        //     url: `${apiBaseUrl}/product/${productId}`,
        //     method: HTTPMethod.PUT,
        //     headers: defaultHeaders,
        //     body: JSON.stringify(product)
        // })
        const productToUpdate = products.find((product) => product.id === productId)
        if (!productToUpdate) return Promise.resolve({ error: 'Product not found' })
        if (productToUpdate) {
            productToUpdate.dateModified = new Date()
            if (product.name) productToUpdate.name = product.name
            if (product.description) productToUpdate.description = product.description
            if (product.price) productToUpdate.price = product.price
            if (product.description) productToUpdate.description = product.description
            if (product.stock) productToUpdate.stock = product.stock
            if (product.imageUrl) productToUpdate.imageUrl = product.imageUrl
        }
        return Promise.resolve({ result: { ...productToUpdate } })
    }
}

type CreateProductDto = Omit<Product, 'id' | 'dateModified'>
type EditProductDto = Partial<Omit<Product, 'id' | 'dateModified'>>