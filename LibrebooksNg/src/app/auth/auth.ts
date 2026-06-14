import { Component, inject } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { AuthLayoutService } from './auth-layout-service';
import { WxSection } from '../ui/core/wx-section/wx-section';
import { WxAlert } from '../ui/core/wx-alert/wx-alert';

@Component({
  selector: 'app-auth',
  imports: [RouterOutlet, WxSection, WxAlert],
  templateUrl: './auth.html',
  styleUrl: './auth.scss',
})
export class Auth {
  readonly authLayout = inject(AuthLayoutService)
}
