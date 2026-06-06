import { Injectable, signal } from '@angular/core';
import { IUser } from '../core/identity/types';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private user = signal<IUser | null>(null);
  constructor() {

  }

  getUser() {
    return this.user()
  }

  isSignedIn() {
    return this.getUser() != null
  }

  signIn(user: IUser) {
    this.user.set(user)
  }
}
