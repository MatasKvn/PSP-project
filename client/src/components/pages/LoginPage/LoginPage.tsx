'use client'

import Button from '@/components/shared/Button'
import Input from '@/components/shared/Input'
import React from 'react'

import styles from './LoginPage.module.scss'

const LoginPage = () => {

    const onSubmit = (event: React.FormEvent) => {
        event.preventDefault()
        const { username, password } = event.target as HTMLFormElement
        console.log(username.value, password.value)
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
            </form>
        </div>
    )
}

export default LoginPage