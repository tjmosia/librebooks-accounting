import { Component, inject, OnInit, signal } from '@angular/core';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { AuthLayoutService } from '../auth-layout-service';
import { Router } from '@angular/router';
import { WxButton, WxFormGroup, WxInputGroup } from "../../ui/core";
import { intents } from '../../ui/core/contants';

interface IResetPasswordModel {
  [key: string]: IFormFieldProps<string>
  password: IFormFieldProps<string>
  code: IFormFieldProps<string>
}

const initial_model: IResetPasswordModel = {
  password: { value: "" },
  code: { value: "" }
}

@Component({
  selector: 'app-reset-password',
  imports: [WxFormGroup, WxInputGroup, WxButton],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.scss',
})
export class ResetPassword implements OnInit {
  model = signal(initial_model)
  authLayout = inject(AuthLayoutService)
  router = inject(Router)

  ngOnInit(): void {
    this.authLayout.setFormTitle("Reset your password")
    this.authLayout.setFormMessage("Enter the verification code sent to your email and your new password.")
  }

  getModelValidationClass(field: IFormFieldProps<string>) {
    if (field.error)
      return intents.error;
    else
      return intents.none
  }

  handleInputChange(event: Event) {
    const { value, name } = event.target as HTMLInputElement

    this.model.update(model => ({
      ...model,
      [name]: {
        value: value.replaceAll(" ", ""),
      }
    }))
  }

  handleSubmit(event: Event) {
    event.preventDefault()
  }

  isValidModel() {
    const _model = this.model()
    let isValid = true

    if (!_model.code.value) {
      _model.code.error = "Verification code is required."
      isValid = false
    }

    if (!_model.password.value) {
      _model.password.error = "Password is required."
      isValid = false
    }

    if (isValid && !formValidators.isValidPassword(_model.password.value)) {
      _model.password.error = "Password doesn't meet requirements."
      isValid = false
    }

    if (!isValid) {
      this.model.set(_model)
    }

    return isValid
  }

  goToAuth() {
    this.authLayout.setEmail("")
    this.router.navigate(["auth"])
  }
}
