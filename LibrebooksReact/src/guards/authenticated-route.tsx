import { useIdentityService } from "../hooks";
import { useNavigate, Outlet } from "react-router";
import { useEffect } from "react";

export function AuthenticatedRoute() {
    const { isLoggedIn, confirmServerLogin, getUser } = useIdentityService();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isLoggedIn()) {
            if (getUser() === undefined) {
                confirmServerLogin({
                    error: (error) => {
                        if (error.status == 401 || error.status == 0)
                            navigate("/auth")
                    }
                })
            } else {
                navigate("/auth");
            }
        }
    }, [isLoggedIn]);

    return isLoggedIn() ? <Outlet /> : <div>Loading. Please wait...</div>;
}