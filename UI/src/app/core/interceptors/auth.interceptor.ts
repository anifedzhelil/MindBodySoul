import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private cookieService: CookieService) {}

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

      return next.handle(authRequest);
    }

    return next.handle(request);
  }

  private isAuthRequired(request: HttpRequest<unknown>) {
    return request.urlWithParams.includes('addAuth=true');
  }
}
