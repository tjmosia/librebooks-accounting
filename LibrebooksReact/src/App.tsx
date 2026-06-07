import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import { useState } from "react";
import { RouterProvider, createBrowserRouter } from 'react-router';
import { IdentityContext, type IIdentityContext } from "./contexts/identity-context.ts";
import type { IClaimsPrincipal } from "./core/identity";
import { routes } from "./routes.ts";
import './styles/App.scss';
import { lightTheme } from './strings/theme.ts';

const router = createBrowserRouter(routes)

function App() {
  const [claimsPrincipal, setClaimsPrincipal] = useState<IClaimsPrincipal | null | undefined>(undefined)

  const identityContext: IIdentityContext = {
    getClaimsPrincipal: () => claimsPrincipal,
    setClaimsPrincipal: setClaimsPrincipal
  }

  return (
    <IdentityContext.Provider value={identityContext}>
      <FluentProvider theme={lightTheme}>
        <RouterProvider router={router} />
      </FluentProvider>
    </IdentityContext.Provider>
  )
}

export default App
