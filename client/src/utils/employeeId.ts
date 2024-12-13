'use client'

export const getEmployeeId = () => {
    const employeeId = localStorage.getItem('employeeId')
    if (!employeeId) throw new Error('Employee Id not found')
    return parseInt(employeeId)
}

export const setEmployeeId = (id: number) => {
    localStorage.setItem('employeeId', id.toString())
}

export const removeEmployeeId = () => localStorage.removeItem('employeeId')