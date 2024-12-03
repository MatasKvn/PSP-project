'use client'

import ProductModificationApi from '@/api/productModification.api'
import Button from '@/components/shared/Button'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import { useProductModifications } from '@/hooks/productModifications.hook'
import { Product, ProductModification } from '@/types/models'
import React, { useState } from 'react'
import ProductModificationCard from '../ProductModificationCard/ProductModificationCard'

import styles from './ProductModificationsView.module.scss'

type Props = {
    product: Product | undefined
}

const ProductModificationsView = (props: Props) => {
    const { product } = props
    const { id: productId, name: productName } = product!

    const {
        productModifications,
        setProductModifications,
        isLoading,
        isError
    } = useProductModifications(productId, 0)

    const [selectedProductModification, setSelectedProductModification] = useState<ProductModification | undefined>(undefined)

    const handleCreate = async () => {
        if (!productId) {
            console.log('No product id found.')
            return
        }
        const response = await ProductModificationApi.createProductModification(productId, {
            name: 'Name',
            description: 'Description',
            price: 0
        })
        if(response.error) {
            console.log(response.error)
            return
        }
        const { result } = response
        const newProductModifications = [...productModifications, result!]
        setProductModifications(newProductModifications)
    }

    const handleEdit = async (payload: DynamicFormPayload) => {
        if (!selectedProductModification) return
        const {
            name,
            description,
            price
        } = payload
        const response = await ProductModificationApi.updateProductModification(selectedProductModification.id, {
            name,
            description,
            price: Number.parseInt(price)
        })
        if (response.error) {
            console.log(response.error)
            return
        }
        const newProductModifications = [
            ...productModifications.filter((pm) => pm.id !== selectedProductModification.id),
            response.result!
        ]
        setProductModifications(newProductModifications)
        setSelectedProductModification(response.result!)
    }

    const handleDelete = async () => {
        if (!selectedProductModification) return
        const response = await ProductModificationApi.deleteProductModification(selectedProductModification.id)
        if(response.error) {
            console.log(response.error)
            return
        }
        const newProductModifications = productModifications.filter((pm) => pm.id !== selectedProductModification.id)
        setProductModifications(newProductModifications)
        setSelectedProductModification(undefined)
    }

    return (
        <div>
            <h3>{`${productName} Modifications`}</h3>
            <div>
                {
                    isLoading || isError ? <div>Loading...</div> :
                    productModifications.map((productModification) => (
                        <ProductModificationCard
                            key={productModification.id}
                            isSelected={selectedProductModification?.id === productModification.id}
                            productModification={productModification}
                            onClick={() => setSelectedProductModification(productModification)}
                        />
                    ))
                }
            </div>
            <Button onClick={handleCreate}>Create New</Button>
            <DynamicForm
                inputs={{
                    name: { label: 'Name', placeholder: 'Enter name:', type: 'text' },
                    description: { label: 'Description', placeholder: 'Enter description:', type: 'text' },
                    price: { label: 'Price', placeholder: 'Enter price:', type: 'number' },
                }}
                onSubmit={handleEdit}
            >
                <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
            <Button onClick={() => handleDelete()}>Delete</Button>
        </div>
    )
}

export default ProductModificationsView
