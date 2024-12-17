import { useState, useEffect } from 'react';
import EmployeeApi from '@/api/employee.api'; 
import { Employee } from '@/types/models'; 

export const useEmployees = (pageNumber: number) => {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [isError, setIsError] = useState<boolean>(false);

    useEffect(() => {
        const handleFetch = async () => {
            const response = await EmployeeApi.getEmployees(30, pageNumber);
            if (response.result) {
                setEmployees(response.result.results);
                setIsLoading(false);
                return;
            }
            setIsError(true);
            setIsLoading(false);
        };
        handleFetch();
    }, [pageNumber]);

    return { employees, setEmployees, isLoading, isError };
};

export const useEmployee = (employeeId: number) => {
    const [employee, setEmployee] = useState<Employee | undefined>(undefined);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [isError, setIsError] = useState<boolean>(false);

    useEffect(() => {
        const handleFetch = async () => {
            const response = await EmployeeApi.getEmployeeById(employeeId);
            if (response.result) {
                setEmployee(response.result);
                setIsLoading(false);
                return;
            }
            setIsError(true);
            setIsLoading(false);
        };
        handleFetch();
    }, [employeeId]);

    return { employee, setEmployee, isLoading, isError };
};

