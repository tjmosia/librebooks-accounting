import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { WxFormGroup, WxInputGroup, WxButton, WxButtonGroup } from '../../ui/core';
import { intents } from '../../ui/core/contants';
import { AuthLayoutService } from '../auth-layout-service';
import { HttpClient } from '@angular/common/http';
import { IUser } from '../../core/identity';
import { ServerService } from '../../providers/server-service';
import { IdentityService } from '../../providers/identity-service';
import { IIdentity } from '../../core/identity/types';
import { AjaxError } from 'rxjs/ajax';
import { IApiError, IApiResult } from '../../core/transactions';
import { StatusCodes } from 'http-status-codes';

@Component({
  selector: 'app-login',
  imports: [WxFormGroup, WxInputGroup, WxButton, WxButtonGroup],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  model = signal<IFormFieldProps>({ value: '' });
  router = inject(Router);
  route = inject(ActivatedRoute);
  authLayout = inject(AuthLayoutService);
  http = inject(HttpClient);
  server = inject(ServerService);
  identity = inject(IdentityService);

  ngOnInit(): void {
    if (!this.authLayout.getEmail()) this.router.navigate(['/auth']).then()
    this.authLayout.setFormTitle("Login")
    this.authLayout.setFormMessage("Login with your password.")
  }

  handleSubmit(event: Event) {
    event.preventDefault();

    if(!this.isValidModel())
      return

    this.http.post<IApiResult<IIdentity>>(this.server.getApiLink('/auth/login'), {
      email: this.authLayout.getEmail(),
      password: this.model().value,
    }, {withCredentials: true}).subscribe({
      next: data => {
        console.log(data);
        if(data.succeeded){
          this.identity.signIn(data.model);
          sessionStorage.removeItem('AUTH_EMAIL');
          if (data.model.company){
            void this.router.navigate(['']);
          }
          else{
            void this.router.navigate(['/company-wizard']);
          }
        }else {
          if (data.errors.length > 0) {
            const _model = { ...this.model() };
            data.errors.forEach((error) => {
              if (error.code === 'Password') _model.error = error.description;
            })
            if (_model.error) this.model.set(_model);
          }
        }
      },
      error: (err: AjaxError) => {
        if(err.status === StatusCodes.NOT_FOUND){
          sessionStorage.removeItem('AUTH_EMAIL');
          void this.router.navigate(['/auth']);
        }

        if(err.status == StatusCodes.BAD_REQUEST){
          const {errors} = err.response as IApiResult
          if(errors && errors.length > 0){
            const _model =  {...this.model()}
            errors.forEach(error => {
              if(error.code === "Password")
                _model.error = error.description
            })
            if(_model.error)
              this.model.set(_model)
          }
        }
      }
    })
  }

  handleInputChange({ value }: { value: string }) {
    this.model.set({
      value: value.replace(/\s/g, ''),
    });
  }

  isValidModel() {
    const _model = this.model();

    if(_model.error)
      return false

    if (!_model.value) {
      _model.error = 'Password is required.';
    }

    if (_model.error) {
      this.model.set(_model);
      return false
    }

    return true;
  }

  getValidationStateClass() {
    if (this.model().error) return intents.error;
    else return intents.none;
  }

  handleResetPasswordClick()
  {

  }
}
