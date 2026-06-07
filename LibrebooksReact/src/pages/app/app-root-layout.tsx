import { Outlet, useLocation, useNavigate } from "react-router"
import { useCompanyService } from "../../hooks"
import { useEffect } from "react"
import { Navbar } from "./shared/navbar/navbar"

export function AppRootLayout() {
    const { getCompany } = useCompanyService()
    const location = useLocation()
    const navigate = useNavigate()

    useEffect(() => {
        if (location.pathname == "/app") {
            if (!getCompany())
                navigate("/app/company-wizard")
            else
                navigate("/app/dashboard")
        }
    }, [location.pathname])

    return (
        <div className="app-root">
            {location.pathname == "/app" ? <span>Loading Session</span> : (
                <>
                    <Navbar />
                    <div className="app-content">
                        <Outlet />
                    </div>
                </>
            )}
        </div>
    )
}