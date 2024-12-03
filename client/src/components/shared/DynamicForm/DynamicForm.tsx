'use client'

import React from 'react'
import Input from '../Input'
import Button, { ButtonProps } from '../Button'

import styles from './DynamivForm.module.scss'

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

        const form = event.target as HTMLFormElement

        const formData = new FormData(form)
        const object = Object.fromEntries(formData)

        onSubmit(object as FormPayload)
        form.reset()
    }

    return (
        <form onSubmit={submit} className={styles.form}>
            {inputsKeyValuePairs.map(([key, { label, ...inputProps }]) => (
                <div key={key} id='form-section'>
                    <label>{label}</label><br />
                    <Input {...inputProps} name={key}/>
                </div>
            ))}
            <div id="form-section">
                {children}
            </div>
        </form>
    )
}

const SubmitButton = (props: ButtonProps) => (<Button {...props} type="submit">{props.children}</Button>)

DynamicForm.Button = SubmitButton

export default DynamicForm
