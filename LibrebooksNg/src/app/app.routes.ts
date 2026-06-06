import { Routes } from '@angular/router';
import { App } from './app';
import { Dashboard } from './app/dashboard/dashboard';
import { authGuard } from './guards/auth-guard';
import { Auth } from './auth/auth';
import { Username } from './auth/username/username';
import { AuthLayoutService } from './auth/auth-layout-service';

export const routes: Routes = [
    {
        path: "",
        pathMatch: "full",
        redirectTo: "app"
    },
    {
        path: "app",
        component: App,
        children: [
            {
                path: "",
                canActivate: [authGuard],
                component: Dashboard
            }
        ]
    },
    {
        path: "auth",
        component: Auth,
        providers: [AuthLayoutService],
        children: [
            {
                path: "",
                component: Username
            },
            {
                path: "login",
                loadComponent: async () => (await import("./auth/login/login")).Login
            },
            {
                path: "register",
                loadComponent: async () => (await import("./auth/register/register")).Register
            },
            {
                path: "reset-password",
                loadComponent: async () => (await import("./auth/reset-password/reset-password")).ResetPassword
            }
        ]
    }
];
