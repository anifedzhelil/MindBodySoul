import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'src/app/services/auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);

  const authService = inject(AuthService);

  const router = inject(Router);
  const user = authService.getUser();

  let token = cookieService.get('Authorization');

  if (token && user) {
    token = token.replace('Bearer ', '');
    const decodenToken: any = jwtDecode(token);
    
    //Check if token is expired
    const expirationDate = decodenToken.exp * 1000;
    const currentTime = new Date().getTime();
    
    if (expirationDate < currentTime) {
      authService.logout();
      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      });
    } else {
      //Token is still valid
      if (user.roles.includes('Writer')) {
        return true;
      } else {
        return false;
      }
    }
  } else {
    authService.logout();
    return router.createUrlTree(['/login'], {
      queryParams: { returnUrl: state.url },
    });
  }
};
