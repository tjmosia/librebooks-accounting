import { Button, Caption1, Caption2, Divider, Field, Input, MessageBar, Spinner } from "@fluentui/react-components"
import { useContext, useEffect, useState, type ChangeEvent, type SubmitEvent } from "react"
import { MdLock, MdLockOpen } from "react-icons/md"
import { useNavigate } from "react-router"
import { ajax, AjaxError } from "rxjs/ajax"
import { FormValidators, type IFormField } from "../../../core/forms"
import { type ITransactionError, type ITransactionResult } from "../../../core/http"
import { type IUser } from "../../../core/identity"
import { serverData } from "../../../strings"
import { lowerFirstLetter } from "../../../utils"
import { sendEmailVerificationCode } from "../auth-funcs"
import { AuthLayoutContext } from "../auth-layout-contexts"

interface IRegisterPageModel {
    name: IFormField<string>
    surname: IFormField<string>
    password: IFormField<string>
    code: IFormField<string>
    isValid?: () => boolean
}


const initialModel: IRegisterPageModel = {
    name: {
        value: ""
    },
    surname: {
        value: ""
    },
    password: {
        value: ""
    },
    code: {
        value: ""
    }
}

export default function RegisterPage() {
    const [model, setModel] = useState<IRegisterPageModel>(initialModel)
    const { setLoading, loading, setFormTitle, setFormMessage, email, setRootMessage } = useContext(AuthLayoutContext)
    const [resendingCode, setResendingCode] = useState<boolean>(false)
    const [showPassword, setShowPassword] = useState<boolean>(false)
    const navigate = useNavigate()

    function onInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target
        const _value = value.replace(/\s/g, '')

        setModel((prev) => ({
            ...prev,
            [name]: {
                value: _value
            }
        }))
    }

    function onCodeChange(event: ChangeEvent<HTMLInputElement>) {
        const { value } = event.target
        const _value = value.replace(/\s/g, '')
        if (isNaN(Number(_value)))
            return

        setModel((prev) => ({
            ...prev,
            code: {
                value: _value
            }
        }))
    }

    useEffect(() => {
        if (!email) {
            navigate("/auth")
        }

        setFormMessage("Let's create your new account.")
        setFormTitle("Sign Up")
        return () => {
            setLoading(false)
            setRootMessage(undefined)
        }
    }, [setFormTitle])

    function onPasswordInputBlur() {
        if (model.password.value && !FormValidators.validatePassword(model.password.value)) {
            setModel((prev) => ({
                ...prev,
                password: {
                    ...prev.password,
                    erred: true,
                    errorMessage: "Password doesn't meet requirements.",
                }
            }))
        }
    }

    function onSubmit(e: SubmitEvent<HTMLFormElement>) {
        e.preventDefault()

        if (!validateModel(model, setModel))
            return

        setLoading(true)

        ajax<ITransactionResult<IUser>>({
            url: serverData.route("/auth/register"),
            withCredentials: true,
            headers: {
                "Content-Type": "application/json"
            },
            method: "POST",
            body: JSON.stringify({
                email: email,
                name: model.name.value,
                surname: model.surname.value,
                password: model.password.value,
                code: model.code.value
            })
        }).subscribe({
            next: (response) => {
                const data = response.response
                console.log(response)
                setLoading(false)
                if (data.succeeded) {
                    navigate("/app")
                } else {
                    for (const error of data.errors) {
                        if (error.code === "Email") {
                            navigate("/auth")
                            break
                        } else {
                            if (error.code == "Code") {
                                setModel(prev => ({
                                    ...prev,
                                    code: {
                                        ...prev.code,
                                        erred: true,
                                        errorMessage: error.description
                                    }
                                }))
                            }
                        }
                    }
                }
            },
            error: (error: AjaxError) => {
                setLoading(false)
                if (error.status === 0) {
                    setRootMessage({
                        message: "Network error. Please check your connection and try again.",
                        intent: "error"
                    })
                }

                if (error.status === 400) {
                    const errors = error.response as ITransactionError[]
                    if (errors && errors.length > 0) {
                        for (const err of errors) {
                            if (err.code === "Email") {
                                navigate("/auth")
                                break
                            } else {
                                if (err.code) {
                                    try {
                                        const key = lowerFirstLetter(err.code) as keyof IRegisterPageModel
                                        setModel(prev => ({
                                            ...prev,
                                            [key]: {
                                                ...prev[key],
                                                erred: true
                                            }
                                        }))
                                    } catch (error) {
                                        console.log(`Error setting error message for field ${err.code}:`, error)
                                        setRootMessage({
                                            message: err.description,
                                            intent: "error"
                                        })
                                    }
                                } else {
                                    setRootMessage({
                                        message: err.description,
                                        intent: "error"
                                    })
                                }
                            }
                        }
                    }
                    setRootMessage({
                        message: "Please ensure that all fields are filled out correctly.",
                        intent: "error"
                    })
                }
            }
        })
    }

    return (
        <>
            <title>Register</title>
            <form className="register-form animate__animated animate__fadeIn" onSubmit={onSubmit}>

                <div className="field">
                    <Divider>Personal Information</Divider>
                </div>
                <div className="field">
                    <Field label="Name" size="small" required
                        validationMessage={model.name.errorMessage}
                        validationState={model.name.erred ? "error" : "none"}>
                        <Input disabled={loading} placeholder="Name" onChange={onInputChange} value={model.name.value} name="name" />
                    </Field>
                </div>
                <div className="field">
                    <Field label="Surname" size="small" required
                        validationMessage={model.surname.errorMessage}
                        validationState={model.surname.erred ? "error" : "none"}>
                        <Input disabled={loading} placeholder="Surname" onChange={onInputChange} value={model.surname.value} name="surname" />
                    </Field>
                </div>
                <div className="field">
                    <Divider>Create your Password</Divider>
                </div>
                <div className="field">
                    <MessageBar intent="info" icon={null}>
                        <Caption2>Min 8 characters mixed-case letters, numbers, and special characters.</Caption2>
                    </MessageBar>
                </div>
                <div className="field">
                    <Field label="Password" size="small" required
                        validationMessage={model.password.errorMessage}
                        validationState={model.password.erred ? "error" : "none"}>
                        <Input disabled={loading} placeholder="Password" onBlur={onPasswordInputBlur}
                            type={showPassword ? "text" : "password"}
                            contentAfter={
                                <Button size="small" onClick={() => setShowPassword(prev => !prev)}
                                    appearance="subtle" icon={
                                        showPassword ? <MdLockOpen /> : <MdLock />
                                    } />
                            }
                            onChange={onInputChange} value={model.password.value} name="password" />
                    </Field>
                </div>
                <div className="field">
                    <Divider>Verification</Divider>
                </div>
                <div className="field">
                    <Caption1>Enter the verification code sent to your mailbox.</Caption1>
                </div>
                <div className="field">
                    <Field label="Verification Code" required size="small"
                        validationMessage={model.code.errorMessage}
                        validationState={model.code.erred ? "error" : "none"}>
                        <Input disabled={loading} onChange={onCodeChange}
                            placeholder="Verification Code"
                            maxLength={6}
                            value={model.code.value}
                            contentAfter={<Button onClick={() => sendEmailVerificationCode({
                                email: email!,
                                reason: "REGISTRATION",
                                setRootMessage,
                                setResendingCode,
                                targetRoute: "/auth/register"
                            })} appearance="subtle" size="small">
                                {resendingCode ? <Spinner size="extra-tiny" /> : "Request"}
                            </Button>}
                            name="code" />
                    </Field>
                </div>
                <div className="field">
                    <Button type="submit" className="submit-button" appearance="primary" disabled={loading}>
                        {loading ? <Spinner size="extra-tiny" /> : "Register"}
                    </Button>
                </div>
                <div className="field">
                    <Button appearance="subtle" className="submit-button" onClick={() => navigate("/auth")}>
                        Change Email
                    </Button>
                </div>
            </form >
        </>)
}

function validateModel(model: IRegisterPageModel, setModel: React.Dispatch<React.SetStateAction<IRegisterPageModel>>,
    setRootError?: React.Dispatch<React.SetStateAction<string | undefined>>): boolean {
    const _model = { ...model }

    if (!model.name.value)
        _model.name.erred = true

    if (!model.surname.value)
        _model.surname.erred = true

    if (!model.password.value)
        _model.password.erred = true

    else if (!FormValidators.validatePassword(model.password.value))
        _model.password.errorMessage = "Password is too weak."

    if (!model.code.value)
        _model.code.erred = true

    else if (_model.code.value!.length !== 6)
        _model.code.errorMessage = "Please check your code."

    if (_model.name.erred || _model.surname.erred || _model.password.erred || _model.code.erred) {
        setModel(_model)
        setRootError?.("Please ensure all fields are correctly filled.")
        return false
    }
    return true
}