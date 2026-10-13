import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ErrorNotificationService } from 'src/app/services/error-notification/error-notification.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private cookieService: CookieService,
    private router: Router,
    private authService: AuthService,
    private errorService: ErrorNotificationService
  ) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (this.isAuthRequired(request)) {
      const authRequest = request.clone({
        setHeaders: {
          Authorization: this.cookieService.get('Authorization'),
        },
      });

      return next.handle(authRequest).pipe(
        catchError((error) => {
          if (error instanceof HttpErrorResponse && ((error.status === 401) || (error.status === 403))) {
            // handle unauthorized requests here
            if (error.status === 401) {
              this.authService.logout();
              this.router.navigate(['/login']);
            }
            else if(error.status === 403)
            {
              this.errorService.show('Нямате право да извършите това действие!');
            }
          }

          return throwError(() => error);
        })
      );
    }

    return next.handle(request);
  }

  private isAuthRequired(request: HttpRequest<unknown>) {
    return request.urlWithParams.includes('addAuth=true');
  }
}
