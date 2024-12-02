'use client'

import ProductApi from '@/api/product.api'
import Button from '@/components/shared/Button'
import ItemCard from '@/components/shared/ItemCard'
import { useProducts } from '@/hooks/products.hook'
import { Product } from '@/types/models'
import React, { useRef, useState } from 'react'

import styles from './ProductsPage.module.scss'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import Input from '@/components/shared/Input'

type Props = {
    pageNumber: number
}

const compareProducts = (product1: Product, product2: Product) => product1.name.localeCompare(product2.name)

const ProductsPage = (props: Props) => {
    const { pageNumber } = props

    const { products, setProducts, isLoading, isError } = useProducts(pageNumber, compareProducts)
    const [selectedProduct, selectProduct] = useState<Product | undefined>(undefined)


    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerFormType = 'create' | 'edit'
    const [sideDrawerFormType, setSideDrawerFormType] = useState<SideDrawerFormType>('create')


    const productCards = () => {
        if (isLoading) {
            return <div>Loading...</div>
        }
        if (isError) {
            return <div>Error</div>
        }

        return products.map((product) => (
            <ItemCard
                key={product.id}
                type="product"
                id={product.id}
                label={product.name}
                description={product.description}
                imageUrl={product.imageUrl}
                price={product.price}
                stock={product.stock}
                isSelected={selectedProduct?.id === product.id}
                onClick={(id: number) => {
                    if (selectedProduct?.id === id) {
                        selectProduct(undefined)
                        return
                    }
                    selectProduct(products.find((product) => product.id === id))
                }}
            />
        ))
    }

    const handleProductCreate = async (event: React.FormEvent) => {
        event.preventDefault()
        const {
            productName,
            productDescription,
            productPrice,
            productStock,
            productImageUrl,
        } = event.target as HTMLFormElement
        const response = await ProductApi.createProduct({
            name: productName.value,
            description: productDescription.value,
            price: productPrice.value,
            stock: productStock.value,
            imageUrl: productImageUrl.value,
        })
        if (response.error) {
            console.log(response.error)
            return
        }
        const newProducts = [...products, response.result!].sort(compareProducts)
        setProducts(newProducts)
    }

    const handleProductDelete = async (product: Product | undefined) => {
        if (!product) return
        const response = await ProductApi.deleteProductById(product.id)
        if (response.error) {
            console.log(response.error)
            return
        }
        const newProducts = products.filter((product) => product.id !== selectedProduct?.id)
            .sort(compareProducts)
        setProducts(newProducts)
        selectProduct(undefined)
    }

    const createProductForm = () => {
        return (
            <form
                onSubmit={handleProductCreate}
                style={{ display: 'inline-block' }}
            >
                <div>
                    <label>Product Name</label><br />
                    <Input
                        placeholder='Enter product name:'
                        type="text"
                        name="productName"
                    />
                </div>
                <div>
                    <label>Product Description</label><br />
                    <Input
                        placeholder='Enter product description:'
                        type="text"
                        name="productDescription"
                    />
                </div>
                <div>
                    <label>Product Price</label><br />
                    <Input
                        placeholder='Enter product price:'
                        type="number"
                        name="productPrice"
                    />
                </div>
                <div>
                    <label>Product Stock</label><br />
                    <Input
                        placeholder='Enter product stock:'
                        type="number"
                        name="productStock"
                    />
                </div>
                <div>
                    <label>Product Image URL</label><br />
                    <Input
                        placeholder='Enter product image url:'
                        type="text"
                        name="productImageUrl"
                    />
                </div>
                <Button type='submit'>Create</Button>
            </form>
        )
    }
    const editProductForm = () => {
        return (
            <form
                onSubmit={handleProductEdit}
                style={{ display: 'inline-block' }}
            >
                <div>
                    <label>Product Name</label><br />
                    <Input
                        placeholder='Enter product name:'
                        type="text"
                        name="productName"
                    />
                </div>
                <div>
                    <label>Product Description</label><br />
                    <Input
                        placeholder='Enter product description:'
                        type="text"
                        name="productDescription"
                    />
                </div>
                <div>
                    <label>Product Price</label><br />
                    <Input
                        placeholder='Enter product price:'
                        type="number"
                        name="productPrice"
                    />
                </div>
                <div>
                    <label>Product Stock</label><br />
                    <Input
                        placeholder='Enter product stock:'
                        type="number"
                        name="productStock"
                    />
                </div>
                <div>
                    <label>Product Image URL</label><br />
                    <Input
                        placeholder='Enter product image url:'
                        type="text"
                        name="productImageUrl"
                    />
                </div>
                <Button type='submit'>Edit</Button>
            </form>
        )
    }
    const handleProductEdit = async (event: React.FormEvent) => {
        event.preventDefault()
        if (!selectedProduct) return
        const {
            productName,
            productDescription,
            productPrice,
            productStock,
            productImageUrl,
        } = event.target as HTMLFormElement
        const response = await ProductApi.updateProductById(selectedProduct.id, {
            name: productName.value || undefined,
            description: productDescription.value || undefined,
            price: productPrice.value || undefined,
            stock: productStock.value || undefined,
            imageUrl: productImageUrl.value || undefined,
        })
        if (response.error) {
            console.log(response.error)
            return
        }
        const newProducts = [
            ...products.filter((product) => product.id !== selectedProduct?.id),
            response.result!
        ].sort(compareProducts)
        setProducts(newProducts)
    }

    return (
        <div className={styles.page}>
            <h1>Products Page</h1>
            <div className={styles.toolbar}>
                <Button
                    onClick={() => {
                        setSideDrawerFormType('create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Product
                </Button>
                <Button
                    onClick={() => {
                        if(!selectedProduct) return
                        setSideDrawerFormType('edit')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Edit Product
                </Button>
                <Button
                    onClick={() => handleProductDelete(selectedProduct)}
                >
                    Delete Product
                </Button>
            </div>
            <div className={styles.card_container}>
                {products.length <= 0 && <div>No products</div>}
                {productCards()}
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {
                    sideDrawerFormType === 'create'
                        ?
                            createProductForm()
                        :
                            editProductForm()
                }
            </SideDrawer>
        </div>
    )
}

export default ProductsPage