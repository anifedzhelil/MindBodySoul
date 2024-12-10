import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest } from 'src/app/models/user/login-request.model';
import { RegisterRequest } from 'src/app/models/user/register-request.model';
import { User } from 'src/app/models/user/user.module';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user$$ = new BehaviorSubject<User | undefined>(
    this.getUserFromLocalStorage()
  );
  user$ = this.user$$.asObservable();

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<User> {
    return this.http
      .post<User>(`${environment.apiBaseUrl}/api/auth/login`, request)
      .pipe(
        tap((response) => {
          const user: User = {
            userName: response.userName,
            roles: response.roles,
            userId: response.userId,
            token: response.token,
          };
          console.log("log", user);
          this.setUser(user);
        })
      );
  }

  setUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.user$$.next(user);
  }

  getUser(): User | undefined {
    return this.user$$.value;
  }

  register(request: RegisterRequest): Observable<any> {
    return this.http.post<any>(
      `${environment.apiBaseUrl}/api/auth/register`,
      request
    );
  }

  logout(): void {
    localStorage.removeItem('user');
    this.user$$.next(undefined);
  }
  
  private getUserFromLocalStorage(): User | undefined {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : undefined;
  }
}
