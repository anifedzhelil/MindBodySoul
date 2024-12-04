import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function appPasswordValidator(): ValidatorFn {
  const regExp = /^(?=.*[a-z])(?=.*[A-Z]).{6,}$/;

  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null;
    }

    return regExp.test(value) ? null : { appPasswordValidator: true };
  };
}
