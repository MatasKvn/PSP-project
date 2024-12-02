import React from 'react'
import Input from '../Input'
import Button, { ButtonProps } from '../Button'

type DynamicFormInputsType = {
    [key: string]: {
        label: string,
    } & Omit<React.InputHTMLAttributes<HTMLInputElement>, 'name'>
}

type Props = Omit<React.FormHTMLAttributes<HTMLFormElement>, 'onSubmit'> & {
    inputs: DynamicFormInputsType
    onSubmit: (formPayload: { [key: string]: string }) => void
    children: React.ReactNode
}

const DynamicForm = (props: Props) => {
    const { inputs, onSubmit, children } = props

    const inputsKeyValuePairs = Object.entries(inputs)

    const submit = (event: React.FormEvent) => {
        event.preventDefault()

        const formData = new FormData(event.target as HTMLFormElement)
        const object = Object.fromEntries(formData)

        const payloadObject = Object.entries(object)
            .reduce((acc, [key, value]) => ({
                ...acc,
                [key as string]: value as string
            }), {} as { [key: string]: string })

        onSubmit(payloadObject)
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

const SubmitButton = (props: ButtonProps) => (<Button {...props} type="submit">Submit</Button>)

DynamicForm.Button = SubmitButton

export default DynamicForm
