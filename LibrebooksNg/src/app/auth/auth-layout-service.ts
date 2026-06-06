import { Injectable, Type, inject } from '@angular/core';
import { Auth } from './auth';
import { signal } from '@angular/core';
import { FindUserDto } from './auth-dtos';

@Injectable({
  providedIn: null
})
export class AuthLayoutService {
  formTitle = signal<string | undefined>(undefined)
  formMessage = signal<string | undefined>(undefined)
  email = signal<string>("")
  user = signal<FindUserDto | null>(null)

  setTitle(title: string | undefined) {
    this.formTitle.set(title)
  }

  setMessage(message: string | undefined) {
    this.formMessage.set(message)
  }

  setEmail(email: string = "") {
    this.email.set(email)
  }

  setUser(user: FindUserDto | null = null) {
    this.user.set(user)
  }
}
