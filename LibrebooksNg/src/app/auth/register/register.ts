import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthLayoutService } from '../auth-layout-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { WxButton, WxFormGroup, WxInputGroup } from '../../widgets/core';

@Component({
  selector: 'app-register',
  imports: [WxInputGroup, WxFormGroup, WxButton],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register implements OnInit {
  authLayout = inject(AuthLayoutService)
  router = inject(Router)
  route = inject(ActivatedRoute)
  email = signal<string>("");

  constructor(private titleService: Title) {
    this.titleService.setTitle("Sign up")
  }

  ngOnInit(): void {
    this.email.set(this.route.snapshot.queryParams['email'] ?? "")

    if (!this.email()) {
      this.router.navigate(['auth'])
      return
    }

    this.authLayout.formTitle.set("Create your account")
    this.authLayout.formMessage.set("Enter your information to create an account")
  }

  goToAuth() {
    this.router.navigate(["auth"])
  }
}
