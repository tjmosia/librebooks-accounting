import { createContext, type Dispatch, type SetStateAction } from "react";
import type { IClaimsPrincipal } from "../core/identity";

interface IIdentityContext {
    getClaimsPrincipal: () => IClaimsPrincipal | null | undefined,
    setClaimsPrincipal: Dispatch<SetStateAction<IClaimsPrincipal | null | undefined>>
}

const IdentityContext = createContext<IIdentityContext>({
    getClaimsPrincipal: () => undefined,
    setClaimsPrincipal: () => { },
})

export {
    IdentityContext,
    type IIdentityContext
};
