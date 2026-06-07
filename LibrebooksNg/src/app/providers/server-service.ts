import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ServerService {
  apiBaseLink = "https://localhost:5262"
  getApiLink(uri: string) {
    return this.apiBaseLink.concat(uri)
  }
}
