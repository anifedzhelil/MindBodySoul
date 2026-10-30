import { Directive, forwardRef } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appUrlHandlePattern]',
  standalone: false,
  providers: [{
    provide: NG_VALIDATORS,
    useExisting: forwardRef(() => UrlHandlePatternDirective),
    multi: true,
  }],
})
export class UrlHandlePatternDirective implements Validator {
  private readonly urlHandlePattern = /^[a-z0-9]+(-[a-z0-9]+)*$/;

  validate(control: AbstractControl): ValidationErrors | null {
    const value = control.value ?? '';
    if (value === '') {
      return null; // empty is handled by the parent's required attribute
    }
    return this.urlHandlePattern.test(value) ? null : { pattern: true };
  }
}