import { FetchResponse } from "@/types/fetch";
import { EmployeeService } from "@/types/models";

let employeeServices: EmployeeService[] = [
    {
        employeeId: 1,
        serviceId: 1
    },
    {
        employeeId: 1,
        serviceId: 2
    },
    {
        employeeId: 2,
        serviceId: 1
    },
    {
        employeeId: 2,
        serviceId: 2
    }
]

export default class EmployeeServiceApi {
    static async getAllEmployeeServices(): Promise<FetchResponse<EmployeeService[]>> {
        return Promise.resolve({
            result: employeeServices
        })
    }
}