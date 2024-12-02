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
        imageUrl: '',
        stock: 12
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
                results: products
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
        const productToCreate: Product = {
            ...product,
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
        if (productToUpdate) {
            productToUpdate.dateModified = new Date()
            if (product.name) productToUpdate.name = product.name
            if (product.description) productToUpdate.description = product.description
            if (product.price) productToUpdate.price = product.price
            if (product.imageUrl) productToUpdate.imageUrl = product.imageUrl
        }
        return Promise.resolve({ result: productToUpdate })
    }
}

type CreateProductDto = Omit<Product, 'id'>
type EditProductDto = Partial<Omit<Product, 'id' | 'dateModified'>>