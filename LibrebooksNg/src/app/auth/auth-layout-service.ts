import { Injectable, Type, inject } from '@angular/core';
import { Auth } from './auth';
import { signal } from '@angular/core';
import { FindUserDto } from './auth-dtos';

@Injectable({
  providedIn: null
})
export class AuthLayoutService {
  private formTitle = signal<string>("Login or Register")
  private formMessage = signal<string>("")
  private email = signal("")
  private user = signal<FindUserDto | null>(null)
  private loading = signal(false)

  constructor() {
    this.email.set(sessionStorage.getItem("AUTH_EMAIL") ?? "")
  }

  getFormTitle() {
    return this.formTitle()
  }

  getFormMessage() {
    return this.formMessage()
  }

  isLoading() {
    return this.loading()
  }

  getEmail(persistValue = true) {
    const val = this.email()
    if (!persistValue) {
      try {
        sessionStorage.removeItem("AUTH_EMAIL")
      } catch { }
      this.setEmail("")
    }
    return val
  }

  getUser(persistValue = true) {
    const val = this.user()
    if (!persistValue) {
      try {
        sessionStorage.removeItem("AUTH_USER")
      } catch { }
      this.setUser(null)
    }
    return val
  }

  setFormTitle(title: string) {
    this.formTitle.set(title)
  }

  setFormMessage(message: string) {
    this.formMessage.set(message)
  }

  setEmail(email: string = "") {
    sessionStorage.setItem("AUTH_EMAIL", email)
    this.email.set(email)
  }

  setUser(user: FindUserDto | null = null) {
    this.user.set(user)
  }

  setLoading(state: boolean = true) {
    this.loading.set(state)
  }
}
