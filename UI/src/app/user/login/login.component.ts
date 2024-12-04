import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm | undefined;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {}

  submitHandler(): void {
    if (!this.loginForm) return;

    if (this.loginForm.invalid) {
      return;
    }

    const value: { email: string; password: string } = this.loginForm.value;
    this.authService.login(value).subscribe({
      next: (response) => {
        this.cookieService.set(
          'Authorization',
          `Bearer ${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict'
        );
 
        this.authService.setUser({
          username: response.userName,
          roles: response.roles
        })

        this.router.navigateByUrl('/');
      },
    });
  }


}
