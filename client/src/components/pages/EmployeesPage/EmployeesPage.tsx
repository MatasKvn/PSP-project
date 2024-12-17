'use client'
import React, { useState, useRef } from 'react'
import EmployeeApi from '@/api/employee.api'
import Button from '@/components/shared/Button'
import { useEmployees } from '@/hooks/employees.hook'
import { Employee, RoleEnum } from '@/types/models'
import SideDrawer from '@/components/shared/SideDrawer'
import { SideDrawerRef } from '@/components/shared/SideDrawer'
import DynamicForm, { DynamicFormPayload } from '@/components/shared/DynamicForm'
import Table from '@/components/shared/Table'
import styles from './EmployeesPage.module.scss'
import { userAgent } from 'next/server'

type Props = {
    pageNumber: number
}

function getNumericValue(roleId: string): number {
    switch (roleId) {
      case 'NONE':
        return 0;
      case 'SERVICE_PROVIDER':
        return 1;
      case 'CASHIER':
        return 2;
      case 'OWNER':
        return 3;
      case 'SUPER_ADMIN':
        return 4;
      default:
        return 0;  
    }
  }
  
const roleIdMap: { [key: string]: keyof typeof RoleEnum } = {
    "None": "NONE",
    "Service Provider": "SERVICE_PROVIDER",
    "Cashier": "CASHIER",
    "Owner": "OWNER",
    "Super Admin": "SUPER_ADMIN"
  };

const compareEmployees = (employee1: Employee, employee2: Employee) => employee1.firstName.localeCompare(employee2.firstName)

const EmployeesPage = ({ pageNumber }: Props) => {
    const { employees, setEmployees, isLoading, isError } = useEmployees(pageNumber)
    const [selectedEmployee, selectEmployee] = useState<Employee | undefined>()

    const sideDrawerRef = useRef<SideDrawerRef | null>(null)
    type SideDrawerContentType = 'create' | 'edit'
    const [sideDrawerContentType, setSideDrawerContentType] = useState<SideDrawerContentType>('create')

    const handleEmployeeCreate = async (formPayload: DynamicFormPayload) => {
        const {
            firstName,
            lastName,
            userName,
            email,
            phoneNumber,
            roleId//,
            //birthDate
        } = formPayload;

        // const selectedRole = roleId as keyof typeof roleIdLabelMapping;
        // const roleIdNum = roleIdLabelMapping[selectedRole]; // Set numeric value
        // console.log("roleIdNum efasf6:", roleIdNum)
        //const roleIdEnumValue = RoleEnum[roleId as keyof typeof RoleEnum];
        //const birthDateParsed = new Date(birthDate);

        // if (birthDate !== '') {
        //     console.log('Incorrect Date format')
        //     return
        // }
        const roleIdNum = getNumericValue(roleId);
        const response = await EmployeeApi.createEmployee({
            firstName,
            lastName,
            userName,
            email,
            phoneNumber,
            roleId: roleIdNum,
            //,
            //birthDate: birthDateParsed,
        });

        if (!response.result) {
            console.log(response.error)
            return;
        }

        const newEmployees = [...employees, response.result].sort((a, b) => a.firstName.localeCompare(b.firstName));
        setEmployees(newEmployees);
        sideDrawerRef.current?.close();
    };

    const handleEmployeeUpdate = async (formPayload: DynamicFormPayload) => {
        if (!selectedEmployee) return;
        const { firstName, lastName, userName, email, phoneNumber, roleId } = formPayload;
        
        // const selectedRole = roleId as keyof typeof roleIdLabelMapping;
        // const roleIdNum = roleIdLabelMapping[selectedRole]; // Set numeric value
        // console.log("roleIdNum efasf6:", roleIdNum)

        //const roleIdEnumValue = RoleEnum[roleId as keyof typeof RoleEnum];
        const roleIdNum = getNumericValue(roleId);
        
        const dataToSend = {
            firstName: firstName || selectedEmployee.firstName,
            lastName: lastName || selectedEmployee.lastName,
            userName: userName || selectedEmployee.userName,
            email: email || selectedEmployee.email,
            phoneNumber: phoneNumber || selectedEmployee.phoneNumber,
            roleId: roleIdNum || selectedEmployee.roleId,
            // birthDate: selectedEmployee.birthDate,
        };

        console.log('Data being sent to update employee:', dataToSend);

        const response = await EmployeeApi.updateEmployeeById(selectedEmployee.id, dataToSend);

        if (!response.result) {
            console.log(response.error)
            return;
        }

        const newEmployees = [
            ...employees.filter((employee) => employee.id !== selectedEmployee?.id),
            response.result,
        ].sort((a, b) => a.firstName.localeCompare(b.firstName));

        setEmployees(newEmployees);
        sideDrawerRef.current?.close();
    };

    const handleEmployeeDelete = async (employee: Employee | undefined) => {
        if (!employee) return
        const response = await EmployeeApi.deleteEmployeeById(employee.id)
        if (!response.result) {
            console.log(response.error)
            return
        }
        // const newEmployees = employees.filter((emp) => emp.id !== selectedEmployee?.id)
        //     .sort(compareEmployees)
        // setEmployees(newEmployees)
        selectEmployee(undefined)
        window.location.reload();
    }

    const employeeTable = () => {
        const columns = [
            { name: 'First Name', key: 'firstName' },
            { name: 'Last Name', key: 'lastName' },
            { name: 'Email', key: 'email' },
            { name: 'User Name', key: 'userName' },
            { name: 'Phone Number', key: 'phoneNumber' },
            { name: 'Birth Date', key: 'birthDate' },
            { name: 'Start Date', key: 'startDate' },
            { name: 'End Date', key: 'endDate' },
            { name: 'Accesibility', key: 'roleId' }
        ];

        const rows = employees.map((employee: Employee) => ({
            id: employee.id,
            firstName: employee.firstName,
            lastName: employee.lastName,
            userName: employee.userName,
            email: employee.email,
            phoneNumber: employee.phoneNumber,
            birthDate: employee.birthDate ? new Date(employee.birthDate).toLocaleDateString() : 'N/A',
            startDate: employee.startDate ? new Date(employee.startDate).toLocaleDateString() : 'N/A',
            endDate: employee.endDate ? new Date(employee.endDate).toLocaleDateString() : 'N/A',
            roleId: employee.roleId,
            className: selectedEmployee?.id === employee.id ? styles.selected : '',
            onClick: (row: any) => {
                if (selectedEmployee?.id === row.id) selectEmployee(undefined)
                else selectEmployee(employees.find((employee) => employee.id === row.id))
            }
        }));

        return (
            <Table
                columns={columns}
                rows={rows}
            />
        );
    }

    const createEmployeeForm = () => {
        const [roleId, setRole] = useState<string>("None");
      
        const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
            setRole(event.target.value); 
        };
      
        const getNumericValue = (value: string): RoleEnum => {
            const key = roleIdMap[value]; 
            return RoleEnum[key];
        };
      
        return (
          <>
            <h4>Create Employee</h4>
            <DynamicForm
              inputs={{
                firstName: { label: 'First Name', placeholder: 'Enter first name:', type: 'text' },
                lastName: { label: 'Last Name', placeholder: 'Enter last name:', type: 'text' },
                userName: { label: 'User Name', placeholder: 'Enter user name:', type: 'text' },
                email: { label: 'Email', placeholder: 'Enter email:', type: 'email' },
                phoneNumber: { label: 'Phone Number', placeholder: 'Enter phone number:', type: 'text' },
                birthDate: { label: 'Birth Date', type: 'date' }
              }}
              onSubmit={(data) => {
                const formData = { ...data }; 
                handleEmployeeCreate(formData); 
              }}
            >
              <div className="form-group">
                <div className="form-row">
                  <label htmlFor="roleId" className="col-form-label">Role</label>
                </div>
                <div className="form-row">
                  <select
                    id="roleId"
                    name="roleId"
                    value={roleId}  
                    onChange={handleChange}
                    aria-label="Select roleId option"
                    className="form-control"
                  >
                    {Object.keys(roleIdMap).map((key) => (
                      <option key={key} value={key}>
                        {key}  
                      </option>
                    ))}
                  </select>
                </div>
              </div>
              <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
          </>
        );
    };
    
    
    const editEmployeeForm = () => {
        const initialEmployeeData = selectedEmployee || { roleId: RoleEnum.NONE };  // Default to NONE if no employee selected
      
        const initialRole = Object.keys(RoleEnum)
          .find((key) => RoleEnum[key as keyof typeof RoleEnum] === initialEmployeeData.roleId)
          ?.toString() || "None"; 
      
        const [roleId, setRole] = useState<string>(initialRole);
      
        const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
          setRole(event.target.value);  
        };
      
        const getNumericValue = (value: string): RoleEnum => {
          const key = roleIdMap[value];
          return RoleEnum[key]; 
        };
      
        return (
          <>
            <h4>Edit Employee</h4>
            <DynamicForm
              inputs={{
                firstName: { label: 'First Name', placeholder: 'Enter first name:', type: 'text' },
                lastName: { label: 'Last Name', placeholder: 'Enter last name:', type: 'text' },
                userName: { label: 'User Name', placeholder: 'Enter user name:', type: 'text' },
                email: { label: 'Email', placeholder: 'Enter email:', type: 'email' },
                phoneNumber: { label: 'Phone Number', placeholder: 'Enter phone number:', type: 'text' },
              }}
              onSubmit={(data) => {
                const formData = { ...data };  
                handleEmployeeUpdate(formData); 
              }}
            >
              <div className="form-group">
                <div className="form-row">
                  <label htmlFor="roleId" className="col-form-label">Role</label>
                </div>
                <div className="form-row">
                  <select
                    id="roleId"
                    name="roleId"
                    value={roleId} 
                    onChange={handleChange}
                    aria-label="Select roleId option"
                    className="form-control"
                  >
                    {Object.keys(RoleEnum)
                      .filter(key => isNaN(Number(key)))
                      .map((key) => {
                        return (
                          <option key={key} value={key}>
                            {key}  
                          </option>
                        );
                      })}
                  </select>
                </div>
              </div>
              <DynamicForm.Button>Submit</DynamicForm.Button>
            </DynamicForm>
          </>
        );
    };
    
    const sideDrawerContent = () => {
        if (sideDrawerContentType === 'create') return createEmployeeForm()
        if (sideDrawerContentType === 'edit') return editEmployeeForm()
    }

    return (
        <div className={styles.page}>
            <h1>Employees Page</h1>
            <div className={styles.toolbar}>
                <Button onClick={() => {
                        setSideDrawerContentType('create')
                        sideDrawerRef.current?.open()
                    }}>Create Employee</Button>
                <Button
                    onClick={() => {
                        if (!selectedEmployee) return
                        setSideDrawerContentType('edit')
                        sideDrawerRef.current?.open()
                    }}
                >Edit Employee</Button>
                <Button
                    onClick={() => handleEmployeeDelete(selectedEmployee)}
                >Delete Employee</Button>
            </div>
            <div>{employeeTable()}</div>
            <SideDrawer ref={sideDrawerRef}>{sideDrawerContent()}</SideDrawer>
        </div>
    )
}

export default EmployeesPage