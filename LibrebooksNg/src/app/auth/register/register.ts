import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthLayoutService } from '../auth-layout-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { WxButton, WxFormGroup, WxInputGroup } from '../../ui/core';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { HttpClient } from '@angular/common/http';
import { ServerService } from '../../providers/server-service';
import { AjaxError } from 'rxjs/ajax';
import { StatusCodes } from 'http-status-codes';
import { intents } from '../../ui/core/contants';
import { IApiResult } from '../../core/transactions';
import { IIdentity } from '../../core/identity';

interface IRegisterModel {
  [key: string]: IFormFieldProps<string>
  code: IFormFieldProps<string>
  name: IFormFieldProps<string>
  surname: IFormFieldProps<string>
  password: IFormFieldProps<string>
  confirmPassword: IFormFieldProps<string>
}

const initial_model: IRegisterModel = {
  code: { value: "" },
  name: { value: "" },
  surname: { value: "" },
  password: { value: "" },
  confirmPassword: { value: "" }
}

@Component({
  selector: 'app-register',
  imports: [WxInputGroup, WxFormGroup, WxButton],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register implements OnInit {
  authLayout = inject(AuthLayoutService)
  router = inject(Router)
  route = inject(ActivatedRoute)
  model = signal<IRegisterModel>(initial_model)
  http = inject(HttpClient)
  server = inject(ServerService)

  constructor(private titleService: Title) {
    this.titleService.setTitle("Sign up")
  }

  ngOnInit(): void {
    if (!this.authLayout.getEmail()) {
      this.router.navigate(['auth'])
      return
    }
    this.authLayout.setFormTitle("Create your account")
    this.authLayout.setFormMessage("Enter your information to create an account")
  }

  handleSubmit(event: Event) {
    event.preventDefault()
    if (!this.isValidModel())
      return

    this.http.post<IApiResult<IIdentity>>(this.server.getApiLink("/auth/register"), {
      email: this.authLayout.getEmail(),
      name: this.model().name.value,
      surname: this.model().surname.value,
      password: this.model().password.value,
      code: this.model().code.value,
    }, {withCredentials: true}).subscribe({
      next: (res) => {
        if(res.succeeded){
          void this.router.navigate(['/']);
          return
        }

        res.errors.forEach((err) => {
          const key = err.code.toLowerCase();

          if(key === "email"){
            void this.router.navigate(['/auth']);
            return
          }
          const _model = {...this.model()}
          switch (key){
            case "email":
            {
              void this.router.navigate(['/auth']);
              break
            }
            case "password": {
              _model.password.error = err.description;
            } break
            case "code": {

            }
          }
        });
      },
      error: (error: AjaxError) => {
        if (error.status === StatusCodes.BAD_REQUEST) {
          const {errors} = error.response as IApiResult<IIdentity>
          const _model = {...this.model()}
          errors.forEach(
            (err) =>{
              const key = err.code.toLowerCase()
              _model[key] = {
                ..._model[key],
                error: err.description,
              };
            }
          );
        }
      }
    })
  }

  modelHasErrors(model: IRegisterModel) {
    const str = model.name.error || model.surname.error || model.password.error || model.confirmPassword.error
    if (str === undefined)
      return false
    return true
  }

  handleInputChange(event: Event) {
    const { name, value } = event.target as HTMLInputElement
    this.model.set({
      ...this.model(),
      [name]: {
        value: value.replaceAll(" ", "")
      }
    })
  }

  getModelValidationClass(field: IFormFieldProps<string>) {
    if (field.error)
      return intents.error
    else
      return intents.none
  }

  isValidModel() {
    const _model = this.model()

    if (!_model.name.value)
      _model.name.error = "Name is required."

    if (!_model.surname.value)
      _model.surname.error = "Surname is required."

    if (!_model.password.value)
      _model.password.error = "Password is required."

    if (!_model.confirmPassword.value)
      _model.confirmPassword.error = "Confirm password is required."

    if (this.modelHasErrors(_model)) {
      this.model.set(_model)
      return false
    }
    //
    // if (_model.password.value !== _model.confirmPassword.value)
    //   _model.confirmPassword.error = "Passwords do not match."
    //
    // if (!_model.confirmPassword.error && !formValidators.isValidPassword(_model.password.value))
    //   _model.password.error = "Your password doesn't meet requirements."
    //
    // if (_model.password.error || _model.confirmPassword.error) {
    //   this.model.set({
    //     ..._model
    //   })
    //   return false
    // }
    return true
  }
}
