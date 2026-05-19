import { NgForm } from '@angular/forms';
import { Component, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css',
  standalone: false
})
export class ForgotPasswordComponent {
  @ViewChild('loginForm') loginForm: NgForm | undefined;
  showSuccess: boolean = false;

  constructor( private authService: AuthService) {}

  submitHandler(): void {
    if (!this.loginForm) return;

    this.authService.forgotPassword(this.loginForm.value.email).subscribe({
      next: () => {
        this.showSuccess = true;
      },
      error: (error) => {
        console.error('Error occurred:', error);
        this.showSuccess = false;
      },
    });



  
  }
}
