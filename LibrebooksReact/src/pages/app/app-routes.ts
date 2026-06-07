import type { RouteObject } from "react-router";
import { AppRootLayout } from "./app-root-layout.tsx";
import { Dashboard } from "./dashboard/dashboard.tsx";
import { CompanyWizard } from "./company-wizard/company-wizard.tsx";

export const appRoutes: RouteObject = {
    path: "app",
    Component: AppRootLayout,
    children: [
        {
            path: "dashboard",
            Component: Dashboard
        },
        {
            path: "company-wizard",
            Component: CompanyWizard
        }
    ]
}