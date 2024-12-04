import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginRequest } from 'src/app/models/user/login-request.model';
import { LoginResponse } from 'src/app/models/user/login-response.model';
import { RegisterRequest } from 'src/app/models/user/register-request.model';
import { User } from 'src/app/models/user/user.module';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${environment.apiBaseUrl}/api/auth/login`,
      request
    );
  }

  register(request: RegisterRequest): Observable<any> {
    return this.http.post<any>(
      `${environment.apiBaseUrl}/api/auth/register`,
      request
    );
  }

  setUser(user: User): void {
    this.$user.next(user);
    localStorage.setItem('user-username', user.username);
    localStorage.setItem('user-roles', user.roles.join(','));
  }

  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const username = localStorage.getItem('user-username');
    const roles = localStorage.getItem('user-roles');

    if (username && roles) {
      const user: User = { username: username, roles: roles?.split(',') };
      return user;
    }

    return undefined;
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
