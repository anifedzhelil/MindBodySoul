import { Component, OnDestroy, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnDestroy {
  @ViewChild('loginForm') loginForm: NgForm | undefined;
  errorMessage: string| undefined="";
  private subscription: Subscription | undefined;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}


  submitHandler(): void {
    if (!this.loginForm) return;

    if (this.loginForm.invalid) {
      this.errorMessage = "Въведете коректен имейл адрес и парола!";
      return;
    }

    const value: { email: string; password: string } = this.loginForm.value;

    this.subscription = this.authService.login(value).subscribe({
      next: () => {
        // Навигация към началната страница след успешен вход
        this.router.navigateByUrl('/');
      },
      error: () => {
        this.errorMessage = 'Имейл адресът или паролата са грешни!';
      },
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}