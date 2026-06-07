import { useContext } from "react";
import { IdentityContext } from "../contexts/identity-context.ts";
import type { ICompany } from "../core/companies/company-type.ts";

export function useCompanyService() {
    const { getClaimsPrincipal, setClaimsPrincipal } = useContext(IdentityContext);
    const claimsPrincipal = getClaimsPrincipal()
    const company = claimsPrincipal?.company
    return {
        getCompany: () => company,
        setCompany: (company: ICompany | null) => {
            if (claimsPrincipal) setClaimsPrincipal({
                ...claimsPrincipal,
                company
            })
        }
    }
}