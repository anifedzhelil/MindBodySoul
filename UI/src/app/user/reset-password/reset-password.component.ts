import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { Validators } from '@angular/forms'; 
import { matchPasswordsValidator } from 'src/app/shared/validators/password/match-paswords-validator';
import { appPasswordValidator } from 'src/app/shared/validators/password/password-validator';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ResetPasswordRequest } from 'src/app/models/user/reset-password-request';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
  standalone: false,
})
export class ResetPasswordComponent {
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  passwordIsVisible = false;
  confirmPasswordIsVisible = false;
  errorMessage: string | undefined = '';
  successMessage: string | undefined = '';
  token: string | null = null;
  email: string | null = null;

  form = this.fb.group({
    passGroup: this.fb.group(
      {
        newPassword: ['', [Validators.required, appPasswordValidator()]],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validators: [matchPasswordsValidator('newPassword', 'confirmPassword')],
      },
    ),
  });

  get passGroup() {
    return this.form.get('passGroup');
  }

  get newPassword() {
    return this.passGroup?.get('newPassword');
  }

  get confirmPassword() {
    return this.passGroup?.get('confirmPassword');
  }

  get passwordErrors() {
    return this.newPassword?.errors;
  }

  get confirmPasswordErrors() {
    return this.confirmPassword?.errors;
  }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.token = params['token'];
      this.email = params['email'];
    });
  }

  submitHandler(): void {
    if (!this.form.valid) return;

    const resetPasswordRequest: ResetPasswordRequest = {
      token: this.token!,
      email: this.email!,
      newPassword: this.newPassword?.value || '',
      confirmPassword: this.confirmPassword?.value || '',
    };

    this.authService.resetPassword(resetPasswordRequest).subscribe({
      next: () => {
        this.successMessage = 'Паролата е нулирана успешно!';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 3000);
      },
      error: () => {
        this.errorMessage =
          'Грешка при нулиране на паролата. Моля, опитайте отново.';
      },
    });
  }

  togglePasswordVisibility(): void {
    this.passwordIsVisible = !this.passwordIsVisible;
  }

  toggleConfirmPasswordVisibility(): void {
    this.confirmPasswordIsVisible = !this.confirmPasswordIsVisible;
  }
}
