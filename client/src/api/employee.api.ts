import { apiBaseUrl } from '@/constants/api'
import { Employee } from '@/types/models'
import { fetch, sanitizeData } from '@/utils/fetch'
import { getAuthorizedHeaders } from '@/utils/fetch'
import { HTTPMethod, FetchResponse, PagedResponse } from '@/types/fetch'

export default class EmployeeApi {
    static async getEmployees(
        pageSize: number,
        pageNumber: number,
        onlyActive?: boolean
    ): Promise<FetchResponse<PagedResponse<Employee>>> {
        const params = new URLSearchParams();
        params.append('pageSize', pageSize.toString());
        params.append('pageNumber', pageNumber.toString());
        if (onlyActive !== undefined) {
            params.append('onlyActive', onlyActive.toString());
        }

        return fetch({
            url: `${apiBaseUrl}/employees?${params.toString()}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders(), 
        });
    }

    static async getEmployeeById(id: number): Promise<FetchResponse<Employee>> {
        return await fetch({
            url: `${apiBaseUrl}/employees/${id}`,
            method: HTTPMethod.GET,
            headers: getAuthorizedHeaders(),
        });
    }

    static async createEmployee(employeeData: CreateEmployeeRequest): Promise<FetchResponse<Employee>> {
        const sanitizedEmployeeData = sanitizeData(employeeData);
        return await fetch({
            url: `${apiBaseUrl}/employees/register`,
            method: HTTPMethod.POST,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(sanitizedEmployeeData),
        });
    }

    static async updateEmployeeById(id: number, employeeData: Partial<Employee>): Promise<FetchResponse<Employee>> {
        const sanitizedEmployeeData = sanitizeData(employeeData);

        return await fetch({
            url: `${apiBaseUrl}/employees/${id}`,
            method: HTTPMethod.PUT,
            headers: getAuthorizedHeaders(),
            body: JSON.stringify(sanitizedEmployeeData),
        });
    }

    static async deleteEmployeeById(id: number): Promise<FetchResponse<Employee | null>> {
        return await fetch({
            url: `${apiBaseUrl}/employees/${id}`,
            method: HTTPMethod.DELETE,
            headers: getAuthorizedHeaders(),
        });
    }
}

export type CreateEmployeeRequest = Omit<Employee, 'id' | 'startDate' | 'endDate'>;

