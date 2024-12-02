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

type Props = {
    pageNumber: number
}

const ProductsPage = (props: Props) => {
    const { pageNumber } = props

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    const { products, setProducts, isLoading, isError } = useProducts(pageNumber)
    const [selectedProduct, selectProduct] = useState<Product | undefined>(undefined)


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

    const handleProductCreate = () => {
    }

    const handleProductEdit = () => {

    }

    const handleProductDelete = async (product: Product | undefined) => {
        if (!product) return
        const response = await ProductApi.deleteProductById(product.id)
        if (response.error) {
            console.log(response.error)
            return
        }
        const newProducts = products.filter((product) => product.id !== selectedProduct?.id)
        setProducts(newProducts)
        selectProduct(undefined)
    }

    return (
        <div>
            <h1>Products Page</h1>
            <p>Page Number: {pageNumber}</p>
            <div className={styles.toolbar}>
                <Button
                    onClick={() => sideDrawerRef.current?.open()}
                >
                    Create Product
                </Button>
                <Button>
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
                <div>Form</div>
            </SideDrawer>
        </div>
    )
}

export default ProductsPage
