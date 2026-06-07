import { Button, Card, Checkbox, Field, Input, Label, Subtitle1, Subtitle2, Textarea, Title3 } from "@fluentui/react-components"
import { useEffect, useState, type ChangeEvent, type FC, type SubmitEvent } from "react"
import type { IFormField } from "../../../core/forms"
import './company-wizard.scss'

export const CompanyWizard: FC = () => {
    const [model, setModel] = useState<ICompanyWizardModel>(companyWizardModel)
    const [physicalAsPostal, setPhysicalAsPostal] = useState(false)
    const [vatRegistered, setVatRegistered] = useState(false)

    function isValidModel() {
        return true
    }

    function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
        event.preventDefault()
        if (!isValidModel()) {
            return
        }
    }

    function formatPhoneNumber(phone: string) {
        return [...phone.replaceAll("-", "")]
            .map((num, i) => {
                if (i === 3 || i === 6)
                    return "-" + num
                return num;
            }).join("")
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { value, name } = event.target as HTMLInputElement | HTMLTextAreaElement
        let val = value
        if (name === "phoneNumber")
            val = formatPhoneNumber(val.replaceAll(/[^0-9]/g, "").substring(0, 10))
        if (name === "vatNumber")
            val = val.replaceAll(/[^0-9]/g, "").substring(0, 10)
        if (name === "emailAddress")
            val = val.replaceAll(/\s+/g, "")
        setModel(prev => ({
            ...prev,
            [name]: { value: val }
        }))
    }

    function handleSameAsPhysicalChange(event: ChangeEvent<HTMLInputElement>) {
        const { checked } = event.target as HTMLInputElement
        setPhysicalAsPostal(checked)
        setModel(prev => ({
            ...prev,
            postalAddress: checked ? { ...prev.physicalAddress } : { ...prev.postalAddress }
        }))
    }

    useEffect(() => {
        if (physicalAsPostal) {
            setModel(prev => ({
                ...prev,
                postalAddress: { ...prev.physicalAddress }
            }))
        }
    }, [model.physicalAddress.value])

    useEffect(() => {
        if (!vatRegistered && model.vatNumber.value)
            setModel(prev => ({
                ...prev,
                vatNumber: { value: "" }
            }))
    }, [vatRegistered])

    return <>
        <title>New Company Form</title>
        <div className="company-wizard">
            <div className="company-wizard__form-container">
                <Card appearance="subtle">
                    <Title3 align="start">New Business profile</Title3>
                    <form onSubmit={handleSubmit}>
                        <div className="form-grid">
                            <div className="grid-row">
                                <div className="grid-column"><Label required htmlFor="legalName">Registered Name:</Label></div>
                                <div className="grid-column">
                                    <Input
                                        className="fullWidth"
                                        placeholder="e.g. Librebooks (Pty) Ltd"
                                        name="legalName" id="legalName" size="small"
                                        value={model.legalName.value}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className="grid-row">
                                <div className="grid-column"><Label required htmlFor="tradingName">Trading Name:</Label></div>
                                <div className="grid-column">
                                    <Input
                                        className="fullWidth"
                                        placeholder="e.g. Librebooks Accounting"
                                        name="tradingName" id="tradingName" size="small"
                                        value={model.tradingName.value}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className="grid-row">
                                <div className="grid-column"><Label required htmlFor="regNumber">Registration Number:</Label></div>
                                <div className="grid-column">
                                    <Input
                                        className="fullWidth"
                                        placeholder={`${new Date().getFullYear()}/123456/07`}
                                        name="regNumber" id="regNumber" size="small"
                                        value={model.regNumber.value}
                                        onChange={handleInputChange}
                                    />
                                    <Checkbox label="My business is VAT registered." name="vatRegistered" onChange={(event) => setVatRegistered(event.target.checked)} />
                                </div>
                            </div>
                            {vatRegistered ?
                                < div className="grid-row">
                                    <div className="grid-column">
                                        <Label htmlFor="vatNumber">VAT Number:</Label>
                                    </div>
                                    <div className="grid-column">
                                        <Input
                                            className="fullWidth"
                                            name="vatNumber" id="vatNumber" size="small"
                                            value={model.vatNumber.value}
                                            onChange={handleInputChange}
                                        />
                                    </div>
                                </div> : null
                            }
                            <div className="grid-row">
                                <div className="grid-column">
                                    <Label required htmlFor="physicalAddress">Physical Address:</Label>
                                </div>
                                <div className="grid-column">
                                    <Field required
                                        validationState={model.physicalAddress.erred ? "error" : undefined}
                                        validationMessage={model.physicalAddress.errorMessage}>
                                        <Textarea
                                            resize="vertical"
                                            rows={5}
                                            placeholder={`Building Details \nStreet Address \nSuburb \nCity/Town \nPostal Code`}
                                            name="physicalAddress" id="physicalAddress" size="small"
                                            value={model.physicalAddress.value}
                                            onChange={handleInputChange}
                                        />
                                    </Field>
                                    <Checkbox label="Use as postal address."
                                        name="sameAsPhysical"
                                        checked={physicalAsPostal}
                                        onChange={handleSameAsPhysicalChange} />
                                </div>
                            </div>
                            <div className="grid-row">
                                <div className="grid-column"><Label required htmlFor="postalAddress">Postal Address:</Label></div>
                                <div className="grid-column">
                                    <Field required
                                        validationState={model.postalAddress.erred ? "error" : undefined}
                                        validationMessage={model.postalAddress.errorMessage}>
                                        <Textarea
                                            rows={4}
                                            resize="vertical"
                                            disabled={physicalAsPostal}
                                            placeholder={`Mail Box Number \nArea/Suburb \nCity/Town \nPostal Code`}
                                            name="postalAddress" id="postalAddress" size="small"
                                            value={model.postalAddress.value}
                                            onChange={handleInputChange}
                                        />
                                    </Field>
                                </div>
                            </div>
                            <div className="grid-row">
                                <div className="grid-column">
                                    <Label required htmlFor="phoneNumber">Phone Number:</Label>
                                </div>
                                <div className="grid-column">
                                    <Input
                                        id="phoneNumber"
                                        required
                                        placeholder="e.g. 073-774-8891"
                                        className="fullWidth"
                                        name="phoneNumber" size="small"
                                        value={model.phoneNumber.value}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                            <div className="grid-row">
                                <div className="grid-column">
                                    <Label required htmlFor="emailAddress">Email Address:</Label>
                                </div>
                                <div className="grid-column">
                                    <Input
                                        placeholder="e.g. john@librebooks.com"
                                        className="fullWidth"
                                        id="emailAddress"
                                        name="emailAddress" size="small"
                                        value={model.emailAddress.value}
                                        onChange={handleInputChange}
                                    />
                                </div>
                            </div>
                        </div>
                        <Button style={{ width: "100%" }} type="submit" appearance="primary">
                            Submit
                        </Button>
                    </form>
                </Card>
            </div >
        </div >
    </>
}

interface ICompanyWizardModel {
    [key: string]: IFormField
    sector: IFormField
    regNumber: IFormField
    physicalAddress: IFormField
    postalAddress: IFormField
    vatNumber: IFormField
    tradingName: IFormField
    legalName: IFormField
    faxNumber: IFormField
    emailAddress: IFormField
    phoneNumber: IFormField
}

const companyWizardModel: ICompanyWizardModel = {
    sector: { value: '' },
    regNumber: { value: '' },
    physicalAddress: { value: '' },
    postalAddress: { value: '' },
    vatNumber: { value: '' },
    tradingName: { value: '' },
    legalName: { value: '' },
    faxNumber: { value: '' },
    emailAddress: { value: '' },
    phoneNumber: { value: '' }
}
