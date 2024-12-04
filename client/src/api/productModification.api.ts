import { FetchResponse, PagedResponse } from '@/types/fetch'
import { Product, ProductModification } from './../types/models'

const productModifications: ProductModification[] = [
    {
        id: 1,
        productId: 1,
        name: 'Brown',
        description: 'Brown is a color',
        price: 10,
        dateModified: new Date(),
    },
    {
        id: 2,
        productId: 1,
        name: 'Green',
        description: 'Unripe banana',
        price: 5,
        dateModified: new Date(),
    },
    {
        id: 3,
        productId: 2,
        name: 'Granny smith',
        description: 'Granny smith apple',
        price: 10,
        dateModified: new Date(),
    },
    {
        id: 4,
        productId: 2,
        name: 'Sweet',
        description: 'A sweeter apple',
        price: 20,
        dateModified: new Date(),
    },
]

export default class ProductModificationApi {
    static async getByProductId(productId: number, pageNumber: number): Promise<FetchResponse<PagedResponse<ProductModification>>> {
        const result: ProductModification[] = productModifications.filter(pm => pm.productId === productId)
        return Promise.resolve({
            result: {
                pageNum: pageNumber,
                pageSize: 35,
                totalCount: result.length,
                results: result
            }
        })
    }

    static async getByCartItemId(cartItemId: number, pageNumber: number): Promise<FetchResponse<PagedResponse<ProductModification>>> {
        const result = productModifications.slice(1, 10)
        return Promise.resolve({
            result: {
                pageNum: pageNumber,
                pageSize: 35,
                totalCount: result.length,
                results: result
            }
        })
    }

    static async createProductModification(productId: number, dto: CreateProductModificationDto): Promise<FetchResponse<ProductModification>> {
        const maxId = Math.max(...productModifications.map(pm => pm.id))
        if (!dto.name) return Promise.resolve({ error: 'Name is required' } )
        if (Number.isNaN(dto.price)) return Promise.resolve({ error: 'Price is required' } )

        const productModification = {
            id: maxId + 1,
            name: dto.name,
            description: dto.description || '',
            dateModified: new Date(),
            price: dto.price,
            productId: productId
        }

        productModifications.push(productModification)

        return Promise.resolve({ result: productModification })
    }

    static async updateProductModification(id: number, dto: EditProductModificationDto): Promise<FetchResponse<ProductModification>> {
        let productModification = productModifications.find(pm => pm.id === id)

        if (!productModification) return Promise.resolve({ error: 'Product modification not found' } )

        productModification = {
            ...productModification,
            ...dto,
            dateModified: new Date()
        }

        return Promise.resolve({ result: productModification })
    }

    static async deleteProductModification(id: number): Promise<FetchResponse<any>> {
        const productModificationToDelete = productModifications.find(pm => pm.id === id)
        if (!productModificationToDelete) return Promise.resolve({ error: 'Product modification not found' } )
        productModifications.splice(productModifications.indexOf(productModificationToDelete), 1)

        return ({ result: productModificationToDelete })
    }
}

type CreateProductModificationDto = Omit<ProductModification, 'id' | 'dateModified'>
type EditProductModificationDto = Partial<Omit<ProductModification, 'id' | 'dateModified'>>