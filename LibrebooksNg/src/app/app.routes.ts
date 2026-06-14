import { Routes } from '@angular/router';
import { App } from './app';
import { Dashboard } from './dashboard/dashboard';
import { authorizeGuard } from './guards/authorize-guard';
import { Auth } from './auth/auth';
import { Username } from './auth/username/username';
import { AuthLayoutService } from './auth/auth-layout-service';
import { inject } from '@angular/core';
import { IdentityService } from './providers/identity-service';
import { CompaniesList, CreateCompany } from './wizards';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: () => {
      const identity = inject(IdentityService);
      return identity.getCompany() ? '/dashboard' : '/wizards/companies';
    },
  },
  {
    path: 'wizards',
    canActivate: [authorizeGuard()],
    canActivateChild: [authorizeGuard()],
    loadComponent: async () => (await import('./layouts')).WizardsLayout,
    children: [
      {
        path: 'company',
        component: CreateCompany,
      },
      {
        path: 'companies',
        component: CompaniesList,
      },
    ],
  },
  {
    path: 'dashboard',
    canActivate: [authorizeGuard({ requireCompany: true })],
    canActivateChild: [authorizeGuard({ requireCompany: true })],
    loadComponent: async () => (await import('./layouts')).DashboardLayout,
    children: [
      {
        path: '',
        loadComponent: async () => (await import('./dashboard/dashboard')).Dashboard,
      },
    ],
  },
  {
    path: 'auth',
    component: Auth,
    providers: [AuthLayoutService],
    children: [
      {
        path: '',
        component: Username,
      },
      {
        path: 'login',
        loadComponent: async () => (await import('./auth/login/login')).Login,
      },
      {
        path: 'register',
        loadComponent: async () => (await import('./auth/register/register')).Register,
      },
      {
        path: 'reset-password',
        loadComponent: async () =>
          (await import('./auth/reset-password/reset-password')).ResetPassword,
      },
    ],
  },
];
