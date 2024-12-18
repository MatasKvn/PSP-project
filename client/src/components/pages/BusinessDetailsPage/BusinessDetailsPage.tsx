'use client'

import { useRef } from "react";
import BusinessDetailsApi from "../../../api/businessDetails.api";
import ServiceApi from "../../../api/service.api";
import { useBusinessDetails } from "../../../hooks/businessDetails.hook";
import { getEmployeeId } from "../../../utils/employeeId";
import Button from "../../shared/Button"
import DynamicForm, { DynamicFormPayload } from "../../shared/DynamicForm";
import SideDrawer, { SideDrawerRef } from "../../shared/SideDrawer";
import Table from "../../shared/Table";

import styles from './BusinessDetailsPage.module.scss'

const BusinessDetailsPage = () => {
    const { errorMsg, isLoading, businessDetails, setBusinessDetails } = useBusinessDetails()

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)

    const businessDetailsTable = () => {
        const columns = [
            { name: 'Name', key: 'name' },
            { name: 'Value', key: 'value' }
        ]

        if (!businessDetails) {
            return <div>Loading...</div>;
        }

        const rows = Object.entries(businessDetails).map(([key, value]) => ({
            id: key,
            name: key,
            value: typeof value === 'number' ? value.toString() : value
        }));

        return (
            <Table
                columns={columns}
                rows={rows}
            />
        )
    }

    const businessDetailsForm = () => {
        return (
            <>
                <h4>{'Update Business Details'}</h4>
                <DynamicForm
                    inputs={{
                        businessName: { label: 'Business Name', placeholder: 'Enter business name:', type: 'text' },
                        businessEmail: { label: 'Business Email', placeholder: 'Enter business email:', type: 'email' },
                        businessPhone: { label: 'Business Phone', placeholder: 'Enter business phone:', type: 'tel' },
                        businessCountry: { label: 'Business Country', placeholder: 'Enter business country:', type: 'text' },
                        businessCity: { label: 'Business City', placeholder: 'Enter business city:', type: 'text' },
                        businessStreet: { label: 'Business Street', placeholder: 'Enter business street', type: 'text' },
                        businessHouseNumber: { label: 'Business House Number', placeholder: 'Enter business house number', type: 'number' },
                        businessFlatNumber: { label: 'Business Flat Number', placeholder: 'Enter business flat number', type: 'number'}
                    }}
                    onSubmit={handleBusinessDetailsUpdate}
                >
                    <DynamicForm.Button>Submit</DynamicForm.Button>
                </DynamicForm>
            </>
        )
    }

    const handleBusinessDetailsUpdate = async (formPayload: DynamicFormPayload) => {
        if (!businessDetails) return;

        const {
            businessName,
            businessEmail,
            businessPhone,
            businessCountry,
            businessCity,
            businessStreet,
            businessHouseNumber,
            businessFlatNumber
        } = formPayload

        const response = await BusinessDetailsApi.updateBusinessDetails({
            businessName: businessName || businessDetails.businessName,
            businessEmail: businessEmail || businessDetails.businessEmail,
            businessPhone: businessPhone || businessDetails.businessPhone,
            country: businessCountry || businessDetails.country,
            city: businessCity || businessDetails.city,
            street: businessStreet || businessDetails.street,
            houseNumber: Number.parseInt(businessHouseNumber) || businessDetails.houseNumber,
            flatNumber: Number.parseInt(businessFlatNumber)
        })

        if (!response.result) {
            console.log(response.error)
            return
        }

        sideDrawerRef.current?.close()
        setBusinessDetails(response.result);
        console.log('Business details updated successfully')
    }

    return (
        <div className={styles.page}>
            <div className={styles.header}>
                <h1>Business Details</h1>
                <div className={styles.toolbar}>
                    <Button
                        disabled={isLoading || !!errorMsg}
                        onClick={() => {
                            sideDrawerRef.current?.open()
                        }}>
                        <h5>Update Business details</h5>
                    </Button>
                </div>
            </div>
            <div>
                {businessDetailsTable()}
            </div>
            <SideDrawer ref={sideDrawerRef}>
                {businessDetailsForm()}
            </SideDrawer>
        </div>
    )
}

export default BusinessDetailsPage