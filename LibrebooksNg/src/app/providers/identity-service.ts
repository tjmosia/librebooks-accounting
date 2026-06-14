import { inject, Injectable, signal } from '@angular/core';
import { IClaim, ICompany, IIdentity, IUser } from '../core/identity/types';
import { HttpClient } from '@angular/common/http';
import { IApiResult } from '../core/transactions';
import { ServerService } from './server-service';
import { catchError, map, Observable, of, take } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  private identity = signal<IIdentity | null | undefined>(undefined);
  http = inject(HttpClient);
  server = inject(ServerService);
  router = inject(Router);

  confirmLoginAsync()
  {
    return this.http
      .post<IIdentity>(this.server.getApiLink('/auth/confirm-login'), null, {
        withCredentials: true,
      })
      .pipe(
        take(1),
        map((res) => {
          console.log(res);
          if (res) {
            this.identity.set(res ?? null);
            return true;
          }
          return false;
        }),
        catchError((err) => {
          console.log(err)
          return of(false);
        }),
      );
  }

  getUser() {
    if(this.identity() === undefined)
      return undefined;
    if(this.identity() === null)
      return null;
    return this.identity()?.user
  }

  isSignedIn() {
    return this.getUser() != null
  }

  signOut() {
    return this.identity.set(null)
  }

  setCompany(company: ICompany) {
    if(!this.isSignedIn()) return
    this.identity.update(prev => ({
      ...prev!,
      company
    }))
  }

  setUser(user: IUser) {
    if(!this.isSignedIn()) return
    this.identity.update( prev =>({
      ...prev!,
      user
    }))
  }

  signIn(identity: IIdentity) {
    this.identity.set(identity);
  }

  getCompany() {
    if(!this.isSignedIn()) return null
    return this.identity()!.company
  }
}
