import { Directive, OnChanges, SimpleChanges } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator, ValidatorFn } from '@angular/forms';
import { appPasswordValidator } from './password-validator';

@Directive({
    selector: '[appPasswordValidator]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: PasswordValidatorDirective,
            multi: true,
        },
    ],
    standalone: false
})
export class PasswordValidatorDirective implements Validator {
  private validator = appPasswordValidator();

  validate(control: AbstractControl): ValidationErrors | null {
    return this.validator(control);
  }
}