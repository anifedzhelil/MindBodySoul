import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/models/user/register-request.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { matchPasswordsValidator } from 'src/app/shared/validators/password/match-paswords-validator';
import { appPasswordValidator } from 'src/app/shared/validators/password/password-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  form = this.fb.group({
    userName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    passGroup: this.fb.group(
      {
        password: ['', [Validators.required, appPasswordValidator()]],
        repeatPassword: ['', [Validators.required]],
      },
      { validators: [matchPasswordsValidator('password', 'repeatPassword')] }
    ),
  });

  errorMessage: string | null = null;

  get userName() {
    return this.form.get('userName');
  }

  get userNameErrors() {
    return this.userName?.errors;
  }

  get email() {
    return this.form.get('email');
  }

  get emailErrors() {
    return this.email?.errors;
  }

  get passGroup() {
    return this.form.get('passGroup');
  }

  get password() {
    return this.passGroup?.get('password');
  }

  get passwordErrors() {
    return this.passGroup?.get('password')?.errors;
  }

  get repeatPasswordErrors() {
    return this.passGroup?.get('repeatPassword')?.errors;
  }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  register(): void {
    console.log(this.form);
    if (this.form.invalid) {
      return;
    }

    const registerRequest: RegisterRequest = {
      email: this.email?.value || '',
      password: this.password?.value || '',
      userName: this.userName?.value || '',
    };

    this.authService.register(registerRequest).subscribe({
      next: (data) => {
        this.router.navigateByUrl('/login');
      },
      error: (error: any) => {
        this.errorMessage = error;
        console.log(error);
      },
    });
  }
}
