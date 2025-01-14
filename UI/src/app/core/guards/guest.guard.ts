import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'src/app/services/auth/auth.service';

export const guestGuard: CanActivateFn = (route, state) => {
   const cookieService = inject(CookieService);
 
   const authService = inject(AuthService);
 
   const router = inject(Router);
   const user = authService.getUser();
 
   let token = cookieService.get('Authorization');
 
   if (token && user) {
     return router.createUrlTree(['/']);
  
   } else {
    return true;
   }
  };
 