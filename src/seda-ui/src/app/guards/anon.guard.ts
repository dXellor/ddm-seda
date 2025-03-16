import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const anonGuard: CanActivateFn = (route, state) => {
  const allowed = inject(AuthService).user() === undefined;
  if (!allowed) {
    inject(Router).navigateByUrl('/');
  }

  return allowed;
};
