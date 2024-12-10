'use client'

import Button from '@/components/shared/Button'
import Input from '@/components/shared/Input'
import React, { useState } from 'react'

import styles from './LoginPage.module.scss'
import AuthApi from '@/api/auth.api'
import { useRouter } from 'next/navigation'
import { GetPageUrl } from '@/constants/route'
import { useCookies } from 'next-client-cookies'
import { setEmployeeId } from '@/utils/employeeId'

const LoginPage = () => {
    const [errorMsg, setErrorMsg] = useState<string>('')
    const router = useRouter()
    const cookies = useCookies()

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
        const { jwtToken, id } = response.result!
        cookies.set('jwtToken', jwtToken, { secure: true, sameSite: 'strict' })
        setEmployeeId(id)
        router.push(GetPageUrl.carts(0))
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