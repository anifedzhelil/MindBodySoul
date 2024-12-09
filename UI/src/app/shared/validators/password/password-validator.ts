import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function appPasswordValidator(): ValidatorFn {
  const regExp =  /^(?=.*[a-zA-Zа-яА-Я])(?=.*\d).{6,}$/;
  const passwordRegExp = /^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d).{6,}$/;


  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null;
    }

    return regExp.test(value) ? 
    null : { appPasswordValidator: true };
  };
}
