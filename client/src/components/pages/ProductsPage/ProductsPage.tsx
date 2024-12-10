'use client'

import ProductApi from '@/api/product.api'
import Button from '@/components/shared/Button'
import ItemCard from '@/components/shared/ItemCard'
import { useProducts } from '@/hooks/products.hook'
import { Product } from '@/types/models'
import React, { useMemo, useRef, useState } from 'react'

import styles from './ProductsPage.module.scss'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import ProductModificationsView from '@/components/specialized/ProductModificationManagementView'

type Props = {
    pageNumber: number
}

const compareProducts = (product1: Product, product2: Product) => product1.name.localeCompare(product2.name)

const ProductsPage = (props: Props) => {
    const { pageNumber } = props

    const { products, setProducts, isLoading, isError } = useProducts(pageNumber, compareProducts)
    const [selectedProduct, selectProduct] = useState<Product | undefined>(undefined)


    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'create' | 'edit' | 'productModifications'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('create')


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

    const createProductForm = () => {
        return (
            <>
                <h4>Create Product</h4>
                <DynamicForm
                    inputs={{
                        productName: { label: 'Product Name', placeholder: 'Enter product name:', type: 'text' },
                        productDescription: { label: 'Product Description', placeholder: 'Enter product description:', type: 'text' },
                        productPrice: { label: 'Product Price', placeholder: 'Enter product price:', type: 'number' },
                        productStock: { label: 'Product Stock', placeholder: 'Enter product stock:', type: 'number' },
                        productImageUrl: { label: 'Product Image URL', placeholder: 'Enter product image url:', type: 'text' },
                    }}
                    onSubmit={handleProductCreate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }
    const handleProductCreate = async (formPayload: DynamicFormPayload) => {
        const {
            productName,
            productDescription,
            productPrice,
            productStock,
            productImageUrl,
        } = formPayload
        const response = await ProductApi.createProduct({
            name: productName,
            description: productDescription,
            price: Number.parseInt(productPrice),
            stock: Number.parseInt(productStock),
            imageUrl: productImageUrl,
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newProducts = [...products, response.result].sort(compareProducts)
        setProducts(newProducts)
        sideDrawerRef.current?.close()
    }

    const editProductForm = () => {
        return (
            <>
                <h4>Edit Product</h4>
                <DynamicForm
                    inputs={{
                        productName: { label: 'Product Name', placeholder: 'Enter product name:', type: 'text' },
                        productDescription: { label: 'Product Description', placeholder: 'Enter product description:', type: 'text' },
                        productPrice: { label: 'Product Price', placeholder: 'Enter product price:', type: 'number' },
                        productStock: { label: 'Product Stock', placeholder: 'Enter product stock:', type: 'number' },
                        productImageUrl: { label: 'Product Image URL', placeholder: 'Enter product image url:', type: 'text' },
                    }}
                    onSubmit={(formPayload) => {
                        handleProductUpdate(formPayload)
                    }}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }
    const handleProductUpdate = async (formPayload: DynamicFormPayload) => {
        if (!selectedProduct) return
        const {
            productName,
            productDescription,
            productPrice,
            productStock,
            productImageUrl,
        } = formPayload
        const price = Number.parseInt(productPrice)
        const stock = Number.parseInt(productStock)
        const response = await ProductApi.updateProductById(selectedProduct.id, {
            name: productName || selectedProduct.name,
            description: productDescription || selectedProduct.description,
            price: isNaN(price) ? selectedProduct.price : price,
            stock: isNaN(stock) ? selectedProduct.stock : stock,
            imageUrl: productImageUrl || selectedProduct.imageUrl,
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newProducts = [
            ...products.filter((product) => product.id !== selectedProduct?.id),
            response.result
        ].sort(compareProducts)
        setProducts(newProducts)
        sideDrawerRef.current?.close()
    }

    const handleProductDelete = async (product: Product | undefined) => {
        if (!product) return
        const response = await ProductApi.deleteProductById(product.id)
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newProducts = products.filter((product) => product.id !== selectedProduct?.id)
            .sort(compareProducts)
        setProducts(newProducts)
        selectProduct(undefined)
    }

    const [productModificationsNotification, notifyProductModificationRefetch_] = useState(false)
    const refetchModifications = () => { notifyProductModificationRefetch_(!productModificationsNotification)}
    const productModificationsView = useMemo(() => (
        <ProductModificationsView product={selectedProduct} />
        // eslint-disable-next-line react-hooks/exhaustive-deps
    ), [productModificationsNotification])

    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'create') return createProductForm()
        if (sideDrawerContentType === 'edit') return editProductForm()
        if (sideDrawerContentType === 'productModifications') return productModificationsView
    }

    return (
        <div className={styles.page}>
            <h1>Products Page</h1>
            <div className={styles.toolbar}>
                <Button
                    onClick={() => {
                        setSideDrawerContentType('create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Product
                </Button>
                <Button
                    onClick={() => {
                        if (!selectedProduct) return
                        setSideDrawerContentType('edit')
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
                <Button
                    onClick={() => {
                        if (!selectedProduct) return
                        refetchModifications()
                        setSideDrawerContentType('productModifications')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Manage Modifications
                </Button>
            </div>
            <div className={styles.card_container}>
                {products.length <= 0 && <div>No products</div>}
                {productCards()}
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {sideDrawerContent()}
            </SideDrawer>
        </div>
    )
}

export default ProductsPage
