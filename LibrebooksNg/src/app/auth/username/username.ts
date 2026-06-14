import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthLayoutService } from '../auth-layout-service';
import { WxFormGroup, WxInputGroup, WxButton } from '../../ui/core';
import { Title } from '@angular/platform-browser';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { intents } from '../../ui/core/contants';
import { HttpClient } from '@angular/common/http';
import { FindUserDto } from '../auth-dtos';
import { AjaxError, AjaxResponse } from 'rxjs/ajax';
import { Router } from '@angular/router';

@Component({
  selector: 'app-username',
  imports: [WxFormGroup, WxInputGroup, WxButton],
  templateUrl: './username.html',
  styleUrl: './username.scss',
})
export class Username implements OnInit {
  readonly authLayout = inject(AuthLayoutService)
  readonly http = inject(HttpClient)
  readonly router = inject(Router)
  model = signal<IFormFieldProps<string>>({ value: "" })

  constructor(private titleService: Title) {
    this.titleService.setTitle("Sign in or Sign Up")
  }

  handleChange({ value }: { value: string }) {
    this.model.set({ value: value })
  }

  ngOnInit(): void {
    this.authLayout.setFormTitle("Sign in or Sign Up")
    this.authLayout.setFormMessage("Enter your username to continue")
    this.model.set({
      value: this.authLayout.getEmail(false)
    })
  }

  getValidationStateClass() {
    return this.model().error ? intents.error : intents.none;
  }

  isValidModel() {
    if (!this.model().value) {
      this.model.set({ ...this.model(), error: "Email is required." })
      return false
    }
    if (!formValidators.isValidEmail(this.model().value)) {
      this.model.set({ ...this.model(), error: "Email is not valid." })
      return false
    }
    return true
  }

  handleSubmit(event: Event) {
    event.preventDefault()
    if (!this.isValidModel())
      return
    this.authLayout.setEmail(this.model().value)
    this.http.get<AjaxResponse<FindUserDto>>(`https://localhost:5262/auth?email=${this.model().value}`)
      .subscribe({
        next: (response: AjaxResponse<FindUserDto>) => {
          const user = response.response
          this.authLayout.setUser(user)
          this.router.navigate(['/auth', 'login'])
        },
        error: (error: AjaxError) => {
          if (error.status == 404)
            this.router.navigate(['/auth', 'register'])
        }
      })
  }
}
