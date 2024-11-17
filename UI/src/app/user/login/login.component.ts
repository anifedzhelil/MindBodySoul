import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { LoginRequest } from 'src/app/models/user/login-request.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm | undefined;

  constructor(private authService: AuthService) {}

  submitHandler(): void {
    if (!this.loginForm) return;


    if (this.loginForm.invalid) {
      return;
    }

    const value: { email: string; password: string } = this.loginForm.value;
    this.authService.login(value)
    .subscribe({
      next: (response) => {
        console.log(response);
      }
    });
  }
}
