'use client'

import Button from '@/components/shared/Button'
import { TimeSlot } from '@/types/models'
import React, { useRef, useState } from 'react'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import { useRouter } from 'next/navigation'

import styles from './TimeSlotsPage.module.scss'
import { useTimeSlots } from '@/hooks/timeSlots.hook'
import Table from '@/components/shared/Table'
import TimeSlotApi from '@/api/timeSlot.api'

type Props = {
    pageNumber: number
}

const TimeSlotsPage = ({ pageNumber }: Props) => {
    const { timeSlots, setTimeSlots, isLoading, errorMsg} = useTimeSlots(pageNumber)
    const router = useRouter()

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    const [selectedTimeSlot, setSelectedTimeSlot] = useState<TimeSlot | null>(null);
    type ActionType = 'create' | 'edit' | 'create-multiple' | 'disable'
    const [actionType, setActionType] = useState<ActionType>()

    const handleTimeSlotCreate = async (formPayload: DynamicFormPayload) => {
        const { employeeId, startTime, isAvailable } = formPayload

        if (Number(employeeId) < 1) {
            console.error('Invalid ID provided:', formPayload.id);
            return;
        }

        const isAvailableValue = isAvailable === 'on' ? true : false

        const response = await TimeSlotApi.create({
            employeeId: Number.parseInt(employeeId),
            startTime: new Date(startTime),
            isAvailable: isAvailableValue
        })
        if (!response.result) {
            console.log(response.error)
            return
        }
        const newTimeSlots = [...timeSlots, response.result]
        setTimeSlots(newTimeSlots)
        sideDrawerRef.current?.close()
    }

    const handleTimeSlotUpdate = async (formPayload: DynamicFormPayload) => {
        const { employeeId, startTime, isAvailable } = formPayload

        const isAvailableValue = isAvailable === 'on' ? true : false
        const id = Number(formPayload.id);
        
        if (isNaN(id) || Number(employeeId) < 1) {
            console.error('Invalid ID provided:', formPayload.id);
            return;
        }

        const response = await TimeSlotApi.update({
            id: Number(id),
            employeeId: Number(employeeId),
            startTime: new Date(startTime),
            isAvailable: isAvailableValue,
        })
    
        if (!response.result) {
            console.error(response.error || 'Failed to update the TimeSlot');
            return;
        }
    
        const updatedTimeSlots = timeSlots.map((slot) =>
            slot.id === response.result?.id ? response.result : slot
        )
    
        setTimeSlots(updatedTimeSlots)
        sideDrawerRef.current?.close()
    }
    
    

    const UpdateTimeSlotForm = (selectedTimeSlot?: TimeSlot) => {
        const id = selectedTimeSlot?.id
        const readOnly = true
        return (
            <>
                <h4>{'Edit Time Slot'}</h4>
                <DynamicForm
                    inputs={{
                        id: { label: `Time Slot ID`, placeholder: `${id}`, type: 'text', readOnly, value: id},
                        employeeId: { label: 'Employee ID', placeholder: 'employee ID', type: 'number' },
                        startTime: { label: 'Start time', placeholder: 'YYYY-MM-DD HH:mm', type: 'datetime-local' },
                        isAvailable: { label: 'Is Time Slot Available?', placeholder: '', type: 'checkbox' }
                    }}
                    onSubmit={handleTimeSlotUpdate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }

    const DisableTimeSlotForm = (selectedTimeSlot?: TimeSlot) => {
        if (!selectedTimeSlot) {
            console.error("No time slot selected")
            return;
        }

        const isAvailableValue = selectedTimeSlot.isAvailable ? 'off' : 'on'

        const formPayload: DynamicFormPayload = {
            id: selectedTimeSlot.id.toString(),
            startTime: selectedTimeSlot.startTime.toString(),
            employeeId: selectedTimeSlot.employeeId.toString(),
            isAvailable: isAvailableValue
        }
        return handleTimeSlotUpdate(formPayload)
    }
    
    // const CreateTimeSlotsForm = (formPayload: DynamicFormPayload) => {
    //     const { employeeId, startTimesAmount, isAvailable } = formPayload;
    //     const count = Number(startTimesAmount);
    
    //     const inputs: {
    //         [key: string]: { label: string; placeholder: string; type: string;}
    //     } = {
    //         employeeId: { label: 'Employee ID', placeholder: `${employeeId}`, type: 'text'},
    //         isAvailable: { label: 'Is time slot available?', placeholder: `${isAvailable}`, type: 'checkbox'},
    //     }
    
    //     for (let i = 1; i <= count; i++) {
    //         inputs[`startTime${i}`] = {
    //             label: `Start time`,
    //             placeholder: 'YYYY-MM-DD HH:mm',
    //             type: 'text',
    //         }
    //     }

    //     return (
    //         <>
    //             <h4>{'Create Time Slot'}</h4>
    //             <DynamicForm
    //                 inputs={inputs}
    //                 onSubmit={handleTimeSlotCreate}
    //             >
    //                 <DynamicForm.Button>Submit</DynamicForm.Button>
    //             </DynamicForm>
    //         </>
    //     )
    // }

    const CreateTimeSlotForm = () => {
        return (
            <>
                <h4>{'Create Time Slot'}</h4>
                <DynamicForm
                    inputs={{
                        employeeId: { label: 'Employee ID', placeholder: 'employee ID', type: 'number' },
                        startTime: { label: 'Start time', placeholder: 'YYYY-MM-DD HH:mm', type: 'datetime-local' },
                        sAvailable: { label: 'Is Time Slot Available?', placeholder: '', type: 'checkbox' }
                    }}
                    onSubmit={handleTimeSlotCreate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }

    const dummyTimeSlotRow = {
        id: 0,
        employeeId: 0,
        startTime: new Date(),
        isAvailable: '',
        edit: ''
    }
    const rows = timeSlots.map((timeSlot) => ({
        ...timeSlot,
        startTime: new Date(timeSlot.startTime).toLocaleString(),
        isAvailable:
            <Button onClick={() => {
                    setSelectedTimeSlot(timeSlot)
                    DisableTimeSlotForm(timeSlot)
                }}
            >
                {timeSlot.isAvailable.toString()}
            </Button>,
        edit: 
            <Button onClick={() => {
                    setActionType('edit')
                    setSelectedTimeSlot(timeSlot)
                    sideDrawerRef.current?.open()
                }}
            >
                Edit
            </Button>
    }))
    

    const columns = Object.keys(dummyTimeSlotRow).map((key) => ({
        name: key,
        key
    }))
    
    return (
        <>
            <h1>Time Slots Page</h1>
            <div className={styles.toolbar}>
                <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('create')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Time Slot
                </Button>
                {/* <Button
                    disabled={isLoading || !!errorMsg}
                    onClick={() => {
                        setActionType('create-multiple')
                        sideDrawerRef.current?.open()
                    }}
                >
                    Create Many Time Slots
                </Button> */}
            </div>
            <Table
                columns={columns}
                rows={rows}
                isLoading={isLoading}
                errorMsg={errorMsg}
            />
            <SideDrawer ref={sideDrawerRef}>
                {actionType === 'create' && CreateTimeSlotForm()}
                {actionType === 'edit' && selectedTimeSlot && UpdateTimeSlotForm(selectedTimeSlot)}
            </SideDrawer>
        </>
    )
}

export default TimeSlotsPage

