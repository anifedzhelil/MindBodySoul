import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  @ViewChild('loginForm') loginForm: NgForm | undefined;

  submitHandler(): void {
    debugger;
    if (!this.loginForm) return;

    const form = this.loginForm;

    // form.reset();

    if (form.invalid) {
      return;
    }

    form.setValue({
      email: '',
      password: '',
    });
    const value: { email: string; password: string } = form.value;
  }
}
