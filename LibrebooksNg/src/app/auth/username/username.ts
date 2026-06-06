import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthLayoutService } from '../auth-layout-service';
import { WxFormGroup, WxInputGroup, WxButton, WxSpinner } from '../../widgets/core';
import { Title } from '@angular/platform-browser';
import { formValidators, IFormFieldProps } from '../../core/forms';
import { intents } from '../../widgets/core/contants';
import { WxProgressBar } from "../../widgets/core/wx-progress-bar/wx-progress-bar";
import { HttpClient } from '@angular/common/http';
import { FindUserDto } from '../auth-dtos';
import { AjaxError, AjaxResponse } from 'rxjs/ajax';
import { catchError, map, of } from 'rxjs';
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

  handleChange({ data, event }: { data: string, event: Event }) {
    this.model.set({ value: data })
  }

  ngOnInit(): void {
    this.authLayout.formTitle.set("Sign in or Sign Up")
    this.authLayout.formMessage.set("Enter your username to continue")
  }

  getValidationState() {
    return this.model().error ? intents.error : null;
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
    if (!this.isValidModel()) return
    this.http.get<AjaxResponse<FindUserDto>>(`https://localhost:5262/auth?email=${this.model().value}`)
      .subscribe({
        next: (response: AjaxResponse<FindUserDto>) => {
          const user = response.response
          this.authLayout.user.set(user)
          this.router.navigate(['/auth', 'login'])
        },
        error: (error: AjaxError) => {
          if (error.status == 404)
            this.router.navigate(['/auth', 'register'], {
              queryParams: {
                email: this.model().value
              }
            })
        }
      })
  }
}
