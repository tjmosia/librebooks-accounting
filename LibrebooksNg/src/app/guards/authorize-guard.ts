import { CanActivateFn, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { inject } from '@angular/core';
import { IdentityService } from '../providers/identity-service';
import { map, of, take } from 'rxjs';
import { IClaim } from '../core/identity';

export function authorizeGuard(props?: IAuthorizeRequirements): CanActivateFn {
  return () => {
    const identityService = inject(IdentityService);
    const router = inject(Router);

    const {roles, claims, requireCompany} = {
      claims: props?.claims ?? [],
      roles: props?.roles ?? [],
      requireCompany: props?.requireCompany ?? false
    };

    const navigationEntries = performance.getEntriesByType(
      'navigation',
    ) as PerformanceNavigationTiming[];

    const isRefresh =
      navigationEntries.length > 0 &&
      (navigationEntries.length == 1 || navigationEntries[0].type === 'reload');

    console.log(isRefresh);
    if (isRefresh) {
      return identityService.confirmLoginAsync().pipe(
        take(1),
        map((signedIn) => {
          console.log(signedIn);
          if (!signedIn){
            void router.navigate(['/auth']);
          }
          else if (requireCompany && !identityService.getCompany()) {
            void router.navigate(['/wizards', 'companies']);
            return false
          }
          return signedIn;
        }),
      );
    } else {
      if (identityService.isSignedIn()) return true;
      else void router.navigate(['auth']);
      return false;
    }
  };
}

interface IAuthorizeRequirements{
  roles?: string[];
  claims?: IClaim[];
  requireCompany?: boolean;
}
