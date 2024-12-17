'use client'

import ProductModificationApi from '@/api/productModification.api'
import Button from '@/components/shared/Button'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import { useProductModifications } from '@/hooks/productModifications.hook'
import { Product, ProductModification } from '@/types/models'
import React, { useState } from 'react'
import ProductModificationCard from '../ProductModificationCard/ProductModificationCard'

import styles from './ProductModificationManagementView.module.scss'

type Props = {
    product: Product | undefined
}

const ProductModificationManagementView = (props: Props) => {
    const { product } = props
    const { id: productId, name: productName } = product!

    const {
        productModifications,
        setProductModifications,
        isLoading,
        isError
    } = useProductModifications(productId, 0)

    const [selectedProductModification, setSelectedProductModification] = useState<ProductModification | undefined>(undefined)
    type ActionType = 'create' | 'edit'
    const [actionType, setActionType] = useState<ActionType>('create')

    const handleCreate = async (payload: DynamicFormPayload) => {
        if (!productId) {
            console.log('No product id found.')
            return
        }

        const {
            name,
            description,
            price,
        } = payload

        const parsedPrice = Number.parseInt(price);
        if (isNaN(parsedPrice)) return

        const response = await ProductModificationApi.createProductModification({
            productVersionId: productId,
            name: name,
            description: description,
            price: parsedPrice
        })
        if(!response.result) {
            console.log(response.error)
            return
        }
        const { result } = response
        const newProductModifications = [...productModifications, result]
        setProductModifications(newProductModifications)
    }

    const handleUpdate = async (payload: DynamicFormPayload) => {
        if (!selectedProductModification) return
        const {
            name,
            description,
            price: pmPrice
        } = payload
        const price = parseInt(pmPrice)
        const response = await ProductModificationApi.updateProductModification(selectedProductModification.id, {
            productVersionId: selectedProductModification.productVersionId,
            name: name || selectedProductModification.name,
            description: description || selectedProductModification.description,
            price: isNaN(price) ? selectedProductModification.price : price
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newProductModifications = [
            ...productModifications.filter((pm) => pm.id !== selectedProductModification.id),
            response.result
        ]
        setProductModifications(newProductModifications)
        setSelectedProductModification(response.result)
    }

    const handleDelete = async () => {
        if (!selectedProductModification) return
        const response = await ProductModificationApi.deleteProductModification(selectedProductModification.id)
        if(!response.result) {
            console.log(response.error)
            return
        }
        const newProductModifications = productModifications.filter((pm) => pm.id !== selectedProductModification.id)
        setProductModifications(newProductModifications)
        setSelectedProductModification(undefined)
        setActionType('create')
    }

    return (
        <div>
            <h3>{`${productName} Modifications`}</h3>
            <div className={styles.card_container}>
            {
                isLoading || isError ? <div>Loading...</div> :
                productModifications.map((productModification) => (
                    <div key={productModification.id} className={styles.card_wrapper}>
                        <ProductModificationCard
                            isSelected={selectedProductModification?.id === productModification.id}
                            productModification={productModification}
                            onClick={() => {
                                if (selectedProductModification?.id === productModification.id) {
                                    setSelectedProductModification(undefined)
                                    setActionType('create')
                                    return
                                }
                                setSelectedProductModification(productModification)
                                setActionType('edit')
                            }}
                        />
                    </div>
                ))
            }
            </div>
            <div className={styles.toolbar}>
                <Button onClick={() => handleDelete()}>Delete</Button>
                <br />
                <br />
                <h4>Modify</h4>
            </div>
            <DynamicForm
                inputs={{
                    name: { label: 'Name', placeholder: 'Enter name:', type: 'text' },
                    description: { label: 'Description', placeholder: 'Enter description:', type: 'text' },
                    price: { label: 'Price', placeholder: 'Enter price:', type: 'number' },
                }}
                onSubmit={actionType === 'create' ? handleCreate : handleUpdate}
            >
                <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
        </div>
    )
}

export default ProductModificationManagementView
