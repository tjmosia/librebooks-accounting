import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { IdentityService } from '../providers/identity-service';

export const authGuard: CanActivateFn = (route, state) => {
  const identityService = inject(IdentityService);
  const router = inject(Router)

  if (identityService.isSignedIn())
    return true;
  else
    router.navigate(['auth'])
  return false
};
