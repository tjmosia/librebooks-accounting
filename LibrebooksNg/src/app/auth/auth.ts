import { Component, inject } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { AuthLayoutService } from './auth-layout-service';

@Component({
  selector: 'app-auth',
  imports: [RouterOutlet],
  templateUrl: './auth.html',
  styleUrl: './auth.scss',
})
export class Auth {
  readonly authLayout = inject(AuthLayoutService)
}
