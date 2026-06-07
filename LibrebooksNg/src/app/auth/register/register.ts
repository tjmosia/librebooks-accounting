import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthLayoutService } from '../auth-layout-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { WxButton, WxFormGroup, WxInputGroup } from '../../widgets/core';
import { formValidators, IFormFieldProps } from '../../core/forms';

interface IRegisterModel{
  [key: string]: IFormFieldProps<string>
  name: IFormFieldProps<string>
  surname: IFormFieldProps<string>
  password: IFormFieldProps<string>
  confirmPassword: IFormFieldProps<string>
}

const initial_model: IRegisterModel = {
  name: {value: ""},
  surname: {value: ""},
  password: {value: ""},
  confirmPassword: {value:""}
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
  email = signal<string>("");
  model = signal<IRegisterModel>(initial_model)

  constructor(private titleService: Title) {
    this.titleService.setTitle("Sign up")
  }

  ngOnInit(): void {
    this.email.set(this.route.snapshot.queryParams['email'] ?? "")

    if (!this.email()) {
      this.router.navigate(['auth'])
      return
    }

    this.authLayout.setFormTitle("Create your account")
    this.authLayout.setFormMessage("Enter your information to create an account")
  }

  modelHasErrors(model: IRegisterModel){
    const str =  model.name.error || model.surname.error || model.password.error || model.confirmPassword.error

    if(str === undefined)
      return false
    return true
  }

  handleInputChange(event: Event){
    const {name, value} = event.target as HTMLInputElement
    this.model.set({
      ...this.model(),
      [name]: {
        value: value.replaceAll(" ","")
      }
    })
  }

  getModelValidationClass(field: IFormFieldProps<string>){
    if(field.error)
      return "error"
    else
      return null
  }

  goToAuth() {
    this.router.navigate(["auth"])
  }

  isValidModel(){
    const _model = this.model()

    if(!_model.name.value)
      _model.name.error = "Name is required."

    if(!_model.surname.value)
      _model.surname.error = "Surname is required."

    if(!_model.password.value)
      _model.password.error = "Password is required."

    if(!_model.confirmPassword.error)
      _model.confirmPassword.error = "Confirm password is required."

    if(this.modelHasErrors(_model)){
      this.model.set(_model)
      return false
    }

    if(_model.password.value !== _model.confirmPassword.value)
      _model.confirmPassword.error = "Passwords do not match."

    if(!_model.confirmPassword.error && !formValidators.isValidPassword(_model.password.value))
      _model.password.error = "Your password doesn't meet requirements."

    if(_model.password.error || _model.confirmPassword.error)
    {
      this.model.set({
        ..._model,
        password: {
          value: _model.password.value,
          error: "Your password doesn't meet requirements."
        }
      })
      return false
    }
    return true
  }
}
