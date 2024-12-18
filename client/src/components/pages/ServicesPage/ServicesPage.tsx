'use client'

import Button from '@/components/shared/Button'
import ItemCard from '@/components/shared/ItemCard'
import { Service } from '@/types/models'
import React, { useRef, useState } from 'react'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'

import { useServices } from '@/hooks/services.hook'
import ServiceApi from '@/api/service.api'
import { getEmployeeId } from '@/utils/employeeId'
import PageChanger from '@/components/shared/PageChanger'
import { GetPageUrl } from '@/constants/route'
import { useRouter } from 'next/navigation'

import styles from './ServicesPage.module.scss'

type Props = {
    pageNumber: number
}

const compareServices = (product1: Service, product2: Service) => product1.name.localeCompare(product2.name)

const ServicesPage = (props: Props) => {
    const { pageNumber } = props
    const router = useRouter()

    const { services, setServices, isLoading, errorMsg } = useServices(pageNumber, compareServices)
    const [selectedService, selectService] = useState<Service | undefined>(undefined)

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type ActionType = 'create' | 'edit'
    const [actionType, setActionType] = useState<ActionType>('create')

    const productCards = () => {
        if (isLoading) {
            return <div>Loading...</div>
        }
        if (errorMsg) {
            return <div>{errorMsg}</div>
        }

        return services.map((product) => (
            <ItemCard
                key={product.id}
                type="service"
                id={product.id}
                label={product.name}
                description={product.description}
                imageUrl={product.imageURL}
                price={product.price}
                isSelected={selectedService?.id === product.id}
                onClick={(id: number) => {
                    if (selectedService?.id === id) {
                        selectService(undefined)
                        return
                    }
                    selectService(services.find((product) => product.id === id))
                }}
            />
        ))
    }

    const serviceForm = () => {
        return (
            <>
                <h4>{actionType === 'create' ? 'Create Service' : 'Edit Service'}</h4>
                <DynamicForm
                    inputs={{
                        serviceName: { label: 'Service Name', placeholder: 'Enter service name:', type: 'text' },
                        serviceDescription: { label: 'Service Description', placeholder: 'Enter service description:', type: 'text' },
                        servicePrice: { label: 'Service Price', placeholder: 'Enter service price:', type: 'number' },
                        serviceDuration: { label: 'Service Duration', placeholder: 'Enter service duration:', type: 'number' },
                        serviceImageUrl: { label: 'Service Image URL', placeholder: 'Enter service image url:', type: 'text' },
                    }}
                    onSubmit={actionType === 'create' ? handleServiceCreate : handleServiceUpdate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }
    const handleServiceCreate = async (formPayload: DynamicFormPayload) => {
        const {
            serviceName,
            serviceDescription,
            servicePrice,
            serviceImageUrl,
            serviceDuration
        } = formPayload
        const price = Number.parseInt(servicePrice)
        const duration = Number.parseInt(serviceDuration)
        if (isNaN(price) || isNaN(duration)) return
        const response = await ServiceApi.create({
            name: serviceName,
            description: serviceDescription,
            price: Number.parseInt(servicePrice),
            imageURL: serviceImageUrl,
            duration: Number.parseInt(serviceDuration),
            employeeId: getEmployeeId()
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        console.log(response.result)
        const newServices = [...services, response.result]
            .sort(compareServices)
        setServices(newServices)
        sideDrawerRef.current?.close()
    }
    const handleServiceUpdate = async (formPayload: DynamicFormPayload) => {
        if (!selectedService) return
        const {
            serviceName,
            serviceDescription,
            servicePrice,
            serviceImageUrl,
            serviceDuration
        } = formPayload
        const price = Number.parseInt(servicePrice)
        const duration = Number.parseInt(serviceDuration)
        const response = await ServiceApi.update({
            id: selectedService.id,
            name: serviceName || selectedService.name,
            description: serviceDescription || selectedService.description,
            price: isNaN(price) ? selectedService.price : price,
            imageURL: serviceImageUrl || selectedService.imageURL,
            duration: duration || selectedService.duration,
            employeeId: getEmployeeId()
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newServices = [
            ...services.filter((product) => product.id !== selectedService?.id),
            response.result
        ].sort(compareServices)
        setServices(newServices)
        sideDrawerRef.current?.close()
    }

    const handleServiceDelete = async (product: Service | undefined) => {
        if (!product) return
        const response = await ServiceApi.deleteById(product.id)
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newServices = services.filter((product) => product.id !== selectedService?.id)
            .sort(compareServices)
        setServices(newServices)
        selectService(undefined)
    }

    return (
        <div className={styles.page}>
            <h1>Services Page</h1>
            <div className={styles.toolbar}>
                <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Service
                </Button>
                <Button
                    disabled={!selectedService || isLoading || !!errorMsg}
                    onClick={() => {
                        if (!selectedService) return
                        setActionType('edit')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Edit Service
                </Button>
                <Button
                    disabled={!selectedService || isLoading || !!errorMsg}
                    onClick={() => handleServiceDelete(selectedService)}
                >
                    Delete Service
                </Button>
            </div>
            <div className={styles.card_container}>
                {services.length <= 0 && <div>No products</div>}
                {productCards()}
            </div>
            <PageChanger
                onClickNext={() => router.push(GetPageUrl.services(parseInt(pageNumber as unknown as string) + 1))}
                onClickPrevious={() => router.push(GetPageUrl.services(pageNumber - 1))}
                disabledPrevious={pageNumber <= 0}
                pageNumber={pageNumber}
            />
            <SideDrawer ref={sideDrawerRef}>
                {serviceForm()}
            </SideDrawer>
        </div>
    )
}

export default ServicesPage
