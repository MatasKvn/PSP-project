import Button from '@/components/shared/Button';
import { useServices } from '@/hooks/services.hook';
import React, { useEffect, useState } from 'react';
import ServicesView from '../ServicesView';
import { Service, TimeSlot } from '@/types/models';

import styles from './CreateServiceCartItemView.module.scss';
import PageChanger from '@/components/shared/PageChanger';
import Table from '@/components/shared/Table';
import { useTimeSlots } from '@/hooks/timeSlots.hook';
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm';
import TimeSlotApi from '@/api/timeSlot.api';

type Props = {
    onSubmit: (formPayload: { 
        serviceId: number | undefined ,
        timeSlotId: number | undefined,
        customerName: string | undefined,
        customerPhone: string | undefined
    }) => void;
};

const CreateServiceCartItemForm = ({ onSubmit }: Props) => {
    const [servicePageNumber, setServicePageNumber] = useState(0);
    const [step, setStep] = useState<'addService' | 'timeSlot' | 'customerInfo' | 'completed'>('addService')
    const { errorMsg, isLoading, services } = useServices(0);
    const [selectedService, setSelectedService] = useState<Service | undefined>()
    const [selectedTimeSlot, setSelectedTimeSlot] = useState<TimeSlot | undefined>()
    const [filteredTimeSlots, setFilteredTimeSlots] = useState<TimeSlot[]>([]);

    useEffect(() => {
        if (!selectedService) return;

        const fetchTimeSlots = async () => {
            try {
                let empId = selectedService.employeeId
                if (!empId) return

                const response = await TimeSlotApi.getAllTimeSlotsByEmployeeIdAndAvailability(empId, true)
                if (response.result) {
                    setFilteredTimeSlots(response.result);
                } else {
                    console.error(response.error || 'Failed to fetch time slots');
                }
            } catch (err) {
                console.error('An error occurred while fetching time slots');
            }
        }
    
        fetchTimeSlots();
    }, [selectedService])

    const handleAddServiceSubmit = () => {
        if (!selectedService) return
        setStep('timeSlot')
    }

    const handleTimeSlotSubmit = () => {
        if (!selectedService || !selectedTimeSlot) return;
        setStep('customerInfo')
    }

    const handleCustomerInfoSubmit = (formPayload : DynamicFormPayload) => {
        const { customerName, customerPhone } = formPayload

        if (!selectedService || !selectedTimeSlot || !customerName || !customerPhone) return;
        setStep('addService')
        onSubmit({ 
            serviceId: selectedService.id,
            timeSlotId: selectedTimeSlot.id,
            customerName: customerName,
            customerPhone: customerPhone
        })
        cancelForm
    }

    const cancelForm = () => {
        setStep('addService')
        setSelectedService(undefined)
        setSelectedTimeSlot(undefined)
    }

    const rows = filteredTimeSlots.map((timeSlot) => ({
        id: timeSlot.employeeVersionId,
        startTime: new Date(timeSlot.startTime).toLocaleString(),
        className: selectedTimeSlot?.id === timeSlot.id ? styles.selected : '',
        onClick: () => {
            if (selectedTimeSlot?.id === timeSlot.id) {
                setSelectedTimeSlot(undefined);
            } else {
                setSelectedTimeSlot(filteredTimeSlots.find((slot) => slot.id === timeSlot.id))
            }
        },
    }))
    const columns = [
        { name: 'Start Time', key: 'startTime' }
    ]

    if (step === 'addService') {
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
                                setSelectedService(undefined);
                                return;
                            }
                            setSelectedService(service);
                        }}
                    />
                </div>
                <PageChanger
                    onClickNext={() => setServicePageNumber(servicePageNumber + 1)}
                    onClickPrevious={() => {
                        if (servicePageNumber >= 1) setServicePageNumber(servicePageNumber - 1)
                    }}
                    pageNumber={servicePageNumber}
                />
                <Button onClick={handleAddServiceSubmit}>Next</Button>
                <Button onClick={
                    cancelForm
                }>Cancel</Button>
            </div>
        )
    }

    if (step === 'timeSlot') {
        return (
            <div>
                <h4>Available Time Slots</h4>
                <div className={styles.services_view}>
                    <Table
                        isLoading={isLoading}
                        rows={rows}
                        columns={columns}
                    />
                </div>
                <PageChanger
                    onClickNext={() => setServicePageNumber(servicePageNumber + 1)}
                    onClickPrevious={() => {
                        if (servicePageNumber >= 1) setServicePageNumber(servicePageNumber - 1)
                    }}
                    pageNumber={servicePageNumber}
                />
                <Button onClick={
                    handleTimeSlotSubmit
                }>Next</Button>
                <Button onClick={
                    cancelForm
                }>Cancel</Button>
            </div>
        );
    }

    if (step === 'customerInfo') {
        return (
            <div>
                <h4>Customer Info</h4>
                <div className={styles.services_view}>
                    <DynamicForm
                        inputs={{
                            customerName: { label: 'Name', placeholder: 'name', type: 'text' },
                            customerPhone: { label: 'Phone number', placeholder: 'number', type: 'text' }
                        }}
                        onSubmit={handleCustomerInfoSubmit}
                    >
                        <DynamicForm.Button>Submit</DynamicForm.Button>
                    </DynamicForm>
                </div>
                <PageChanger
                    onClickNext={() => setServicePageNumber(servicePageNumber + 1)}
                    onClickPrevious={() => {
                        if (servicePageNumber >= 1) setServicePageNumber(servicePageNumber - 1)
                    }}
                    pageNumber={servicePageNumber}
                />
                <Button onClick={
                    cancelForm
                }>Cancel</Button>
            </div>
        )
    }

    return (
        <div>
            <h4>Service Cart Item Form Completed</h4>
        </div>
        
    )
}

export default CreateServiceCartItemForm;
