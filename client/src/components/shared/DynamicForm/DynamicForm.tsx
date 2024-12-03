'use client'

import React from 'react'
import Input from '../Input'
import Button, { ButtonProps } from '../Button'

export type DynamicFormInputs = {
    [key: string]: {
        label: string
        placeholder: React.InputHTMLAttributes<HTMLInputElement>['placeholder']
        type: React.InputHTMLAttributes<HTMLInputElement>['type']
    }
}
export type FormPayload = {
    [U in keyof DynamicFormInputs]: string
}

type Props<T extends DynamicFormInputs> = Omit<React.FormHTMLAttributes<HTMLFormElement>, 'onSubmit'> & {
    inputs: T
    onSubmit: (formPayload: FormPayload) => void
    children: React.ReactNode
}

const DynamicForm = <T extends DynamicFormInputs,>(props: Props<T>) => {
    const { inputs, onSubmit, children } = props

    const inputsKeyValuePairs = Object.entries(inputs)

    const submit = (event: React.FormEvent) => {
        event.preventDefault()

        const formData = new FormData(event.target as HTMLFormElement)
        const object = Object.fromEntries(formData)

        onSubmit(object as FormPayload)
    }

    return (
        <form onSubmit={submit}>
            {inputsKeyValuePairs.map(([key, { label, ...inputProps }]) => (
                <div key={key}>
                    <label>{label}</label><br />
                    <Input {...inputProps} name={key}/>
                </div>
            ))}
            {children}
        </form>
    )
}

const SubmitButton = (props: ButtonProps) => (<Button {...props} type="submit">{props.children}</Button>)

DynamicForm.Button = SubmitButton

export default DynamicForm
