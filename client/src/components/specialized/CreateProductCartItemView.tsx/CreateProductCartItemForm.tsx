import DynamicForm from '@/components/shared/DynamicForm'
import { FormPayload } from '@/components/shared/DynamicForm/DynamicForm'
import { useProducts } from '@/hooks/products.hook'
import { Product, ProductModification } from '@/types/models'
import React, { useState } from 'react'
import ProductModificationsView from '../ProductModificationsView/ProductModificationsView'
import ProductsView from '../ProductsView'
import { useProductModifications } from '@/hooks/productModifications.hook'
import PageChanger from '@/components/shared/PageChanger'

type Props = {
    onSubmit: (formPayload: { productId: number; quantity: string; modificationIds: number[] }) => void
}

const CreateProductCartItemForm = ({ onSubmit }: Props) => {
    const [productPageNumber, setProductPageNumber] = useState<number>(0)
    const [productModificationPageNumber, setProductModificationPageNumber] = useState<number>(0)
    const [selectedProduct, setSelectedProduct] = useState<Product>()
    const {
        products,
        isError: isProductsError,
        isLoading: isProductsLoading
    } = useProducts(productPageNumber)
    const {
        productModifications,
        isLoading: isPMsLoading,
        isError: isPMsError
    } = useProductModifications(selectedProduct?.id || 0, productModificationPageNumber)
    const [selectedProductModifications, setSelectedProductModifications] = useState<ProductModification[]>([])

    const handleFormSubmit = (formPayload: FormPayload) => {
        if (!selectedProduct) {
            return
        }
        const { quantity } = formPayload
        onSubmit({
            productId: selectedProduct.id,
            quantity,
            modificationIds: selectedProductModifications.map((pm) => pm.id)
        })
        setSelectedProduct(undefined)
        setSelectedProductModifications([])
    }

    return (
        <div>
            <h4>Create Product</h4>
            <div style={{ height: '40vh', overflowY: 'scroll' }}>
                <ProductsView
                    products={products}
                    selectedProducts={selectedProduct ? [selectedProduct] : []}
                    isError={isProductsError}
                    isLoading={isProductsLoading}
                    pageNumber={productPageNumber}
                    onClick={(product: Product) => {
                        if (product.id === selectedProduct?.id) {
                            setSelectedProduct(undefined)
                            return
                        }
                        setSelectedProduct(product)
                    }}
                />
                <PageChanger
                    pageNumber={productPageNumber}
                    onClickPrevious={() => { if (productPageNumber >= 1) setProductPageNumber((p) => p - 1) }}
                    onClickNext={() => { setProductPageNumber((p) => p + 1) }}
                />
            </div>
            <div style={{ height: '20vh', overflowY: 'scroll' }}>
                <ProductModificationsView
                    style={{ height: '20vh'}}
                    productModifications={productModifications}
                    selectedProductModifications={selectedProductModifications}
                    onClick={(productModification) => {
                        if (selectedProductModifications.includes(productModification)) {
                            const newSelectedPms = selectedProductModifications.filter((pm) => pm !== productModification)
                            setSelectedProductModifications(newSelectedPms)
                            return
                        }
                        const newSelectedPms = [...selectedProductModifications, productModification]
                        setSelectedProductModifications(newSelectedPms)
                    }}
                    isError={isPMsError}
                    isLoading={isPMsLoading}
                />
                {
                    selectedProduct && (
                        <PageChanger
                            pageNumber={productModificationPageNumber}
                            onClickPrevious={() => { if (productModificationPageNumber >= 1) setProductModificationPageNumber((p) => p - 1) }}
                            onClickNext={() => { setProductModificationPageNumber((p) => p + 1) }}
                        />
                    )
                }
            </div>
            <DynamicForm
                inputs={{
                    quantity: { label: 'Quantity', placeholder: 'Enter quantity:', type: 'number' },
                }}
                onSubmit={handleFormSubmit}
            >
                <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default CreateProductCartItemForm