'use client'

import Button from '@/components/shared/Button'
import Input from '@/components/shared/Input'
import React, { useState } from 'react'

import styles from './LoginPage.module.scss'
import AuthApi from '@/api/auth.api'
import { setCookie } from '@/utils/cookie'
import { useRouter } from 'next/navigation'

const LoginPage = () => {
    const [errorMsg, setErrorMsg] = useState<string>('')
    const router = useRouter()

    const onSubmit = async (event: React.FormEvent) => {
        event.preventDefault()
        const { username, password } = event.target as HTMLFormElement
        const response = await AuthApi.login({
            username: username.value,
            password: password.value
        })
        if (response.error) {
            setErrorMsg(response.error)
            return
        }
        const { id, userName, role, jwtToken } = response.result!
        setCookie('jwtToken', jwtToken)
        setCookie('employeeId', id.toString())
        setCookie('userName', userName)
        setCookie('role', role)
        router.push('/dashboard/carts/0')
    }

    return (
        <div className={styles.form_wrapper}>
            <form onSubmit={onSubmit} className={styles.form}>
                <h1 className={styles.form_label}>Login</h1>
                <div className={styles.input_fields_container}>
                    <div>
                        <label>Username</label><br/>
                        <Input
                            placeholder='Enter username:'
                            type="text"
                            name="username"
                        />
                    </div>
                    <div>
                        <label>Password</label><br/>
                        <Input
                            placeholder='Enter password:'
                            type="password"
                            name="password"
                        />
                    </div>
                </div>
                <Button type='submit' className={styles.login_button}>
                    Login
                </Button>
                <span className={styles.error_text}>{errorMsg}</span>
            </form>
        </div>
    )
}

export default LoginPage