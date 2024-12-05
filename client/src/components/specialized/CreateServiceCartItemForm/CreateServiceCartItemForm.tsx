import Button from '@/components/shared/Button'
import { useServices } from '@/hooks/services.hook'
import React, { useState } from 'react'
import ServicesView from '../ServicesView'
import { Service } from '@/types/models'

import styles from './CreateServiceCartItemView.module.scss'
import PageChanger from '@/components/shared/PageChanger'

type Props = {
    onSubmit: (formPayload: { serviceId: number | undefined }) => void
}

const CreateServiceCartItemForm = ({ onSubmit }: Props) => {
    const [servicePageNumber, setServicePageNumber] = useState(0)
    const {
        errorMsg,
        isLoading,
        services
    } = useServices(0)
    const [selectedService, setSelectedService] = useState<Service | undefined>()

    return (
        <div>
            <h4>Add Service</h4>
            <div className={styles.services_view}>
                <ServicesView
                    isError={!!errorMsg}
                    isLoading={isLoading}
                    services={services}
                    selectedServices={selectedService ? [selectedService] : []}
                    pageNumber={servicePageNumber}
                    onClick={(service) => {
                        if (selectedService?.id === service.id) {
                            setSelectedService(undefined)
                            return
                        }
                        setSelectedService(service)
                    }}
                />
            </div>
            <PageChanger
                onClickNext={() => setServicePageNumber(servicePageNumber + 1)}
                onClickPrevious={() => { if (servicePageNumber >= 1) setServicePageNumber(servicePageNumber - 1) }}
                pageNumber={servicePageNumber}
            />
            <Button onClick={() => {
                onSubmit({ serviceId: selectedService?.id })
                setSelectedService(undefined)
            }}>Submit</Button>
        </div>
    )
}

export default CreateServiceCartItemForm